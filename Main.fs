open System

open QPC.Parser

[<EntryPoint>]
let main argv =
    testVar "$132"
    testStatement "$Include [$POSIX && $OSX64]"
    0
