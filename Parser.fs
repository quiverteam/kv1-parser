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


type Syntax = Statement of (string * string * Condition)
            | Block     of (string * Syntax)

// let ws1 = many1 <| anyOf " \t"

let str_ws s = pstring s >>. spaces

// Newlines **must** end in an NL char, an arbitrary number of CRs is allowed
// before, but there must be an NL at the end. Might break support on very,
// very old macs, but I'm not worried about that.
// let nlPadded = many (anyOf " \t\r") .>> pstring "\n"

let variable = spaces >>. pstring "$" >>. regex "([a-zA-Z0-9_\-]+)" .>> spaces

let stringlit =
    spaces >>. between (pstring "\"") (pstring "\"")
        (manyChars (noneOf "\"\r\n"))

// Condition parsers. They parse conditions in square brackets. e.g:
// [$foo && $bar || $baz]

let opp = new OperatorPrecedenceParser<Condition, unit, unit>()
let cond = opp.ExpressionParser

let varCond = variable |>> DefCond

let term = attempt varCond <|> between (str_ws "(") (str_ws ")") cond
opp.TermParser <- term

type Asc = Associativity

let addInfix a T = opp.AddOperator(InfixOperator(a, spaces, 1, Asc.Left, fun x y -> T(x, y)))

addInfix "&&" AndCond
addInfix "||" OrCond

let condition = spaces >>. between (pstring "[") (pstring "]") cond

// Comments

let lineComment = pstring "//" >>. restOfLine true
let blockComment =
    between
        (pstring "/*")
        (pstring "*/")
        (charsTillString "*/" false System.Int32.MaxValue)

let comment = lineComment <|> blockComment |> skipMany

// Syntax rules

let conditionalStatement =
    variable .>>. stringlit .>>.? condition
    |> attempt
    |>> fun ((x, y), z) -> Statement(x, y, z)

let unconditionalStatement =
    variable .>>. stringlit
    |>> fun (x, y) -> Statement(x, y, EmptyCond)

let statement =
        conditionalStatement
    <|> unconditionalStatement

let testVar       s = test variable s
let testStatement s = test statement s
let testString    s = test stringlit s
let testComment   s = test comment s
let testCond      s = test condition s
