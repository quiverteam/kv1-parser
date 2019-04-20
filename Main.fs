open System

open QPC.Parser

[<EntryPoint>]
let main argv =
    testVar "$132"
    testStatement """ $Include "foo/bar/baz" [$POSIX && $OSX64] """
    // This fails:
    testStatement """ $Include "foo/bar/baz" [$POSIX || $OSX] """
    testStatement """ $Include "foo/bar/baz" """
    testString """ "This is a string" """
    testComment "// This is a comment \n/* Block Comment */"
    0
