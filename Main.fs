open System

open QPC.Parser

[<EntryPoint>]
let main argv =
    testVar "$132"
    testStatement """$Include "foo/bar/baz" [$POSIX && $OSX64]"""
    testString """ "This is a string" """
    0
