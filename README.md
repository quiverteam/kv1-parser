# KeyValues Parser

An F# implementation of a parser for the KeyValues file format. Used in QPC
to parse qpc and qgc files.

## Dependencies

- FParsec via Nuget

## Status

The full kv1 spec parses. Here's a stress test of what it can parse:

```pl
$Configuration
{
    $Include "windows.h" [$WINDOWS]
    $Include "posix.h" [$OSX || ($LINUX && $POSIX)]
}
```

Which parses successfully to this F# representation of the data:

```fs
Statement
  ("$Configuration",
   BlockValue
     [Statement ("$Include", StringValue "windows.h", DefCond "$WINDOWS");
      Statement
        ("$Include", StringValue "posix.h",
         OrCond (DefCond "$OSX", AndCond (DefCond "$LINUX", DefCond "$POSIX")))],
   EmptyCond)
```
