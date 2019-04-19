//
// Parser for the QPC/VPC file format. github.com/quiverteam/qpc-parser
//

module QPC.Parser

open FParsec
open System



let test parser str =
    match run parser str with
    | Success(res, _, _) -> printfn "%A" res
    | Failure(msg, _, _) -> printfn "%s" msg


// A condition that must be met for something, e.g:
//
// $Include "foo" [$VAR]
//
// foo is included if $VAR is defined. Useful for system specific declarations
type Condition = AndCond of (Condition * Condition)
               // One or more of  the conditions is true
               | OrCond of (Condition * Condition)
               // Some condition is false
               | NotCond of Condition
               // Some var is defined
               | DefCond of string
               // Always true, used if empty or no conditions provided.
               | EmptyCond


type Syntax = Statement of (string * Condition)
            | Block     of (string * Syntax)

// let ws1 = many1 <| anyOf " \t"

let paddedStr s = spaces >>. stringReturn s s .>> spaces

// Newlines **must** end in an NL char, an arbitrary number of CRs is allowed
// before, but there must be an NL at the end. Might break support on very,
// very old macs, but I'm not worried about that.
// let nlPadded = many (anyOf " \t\r") .>> pstring "\n"

let variable = pstring "$" >>. regex "([a-zA-Z0-9_\-]+)" .>> spaces

// Condition parsers. They parse conditions in square brackets. e.g:
// [$foo && $bar || $baz]
//

let cond, condRef = createParserForwardedToRef<Condition, unit>()

let varCond = variable |>> DefCond


let infixStr s T = pstring s .>> spaces >>% (fun x y -> T(x, y))
let infixCond s T = chainl1 varCond (infixStr s T)

let andCond = infixCond "&&" AndCond

condRef := andCond <|> varCond



let condition = spaces >>. between (pstring "[") (pstring "]") cond

let statement = variable .>>. condition |>> Statement 

let testVar s = test variable s
let testStatement s = test statement s
