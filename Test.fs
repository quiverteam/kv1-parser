open System

open KeyValues.Parser

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
    testBlock """ { foo bar } """
    testStatement """
    $Configuration
    {
        $Include "windows.h" [$WINDOWS]
        $Include "posix.h" [$OSX || ($LINUX && $POSIX)]
    }"""
    0
