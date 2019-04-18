open FParsec
open System

open QPC.Parser

[<EntryPoint>]
let main argv =
    test pfloat "0.132"

    0
