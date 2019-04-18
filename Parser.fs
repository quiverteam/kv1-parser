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
