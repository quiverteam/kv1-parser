open System

open QPC.Parser

[<EntryPoint>]
let main argv =
    testVar "$132"
    testStatement """ $Include "foo/bar/baz" [$POSIX && $OSX64] """
    testStatement """ $Include "foo/bar/baz" """
    testCond "[$FOO && $BAZ || $BAR]"
    testCond "[$FOO && $BAR && $BAZ]"
    testString """ "This is a string" """
    testComment "// This is a comment \n/* Block Comment */"
    testKey """ "hello world" """
    testKey "Nice"
    0
