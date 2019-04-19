# QPC Parser

An F# implementation of a parser for the QPC/VPC file format. Should
be built as a library and used as the parser frontend for other generators.

## Dependencies

- FParsec via Nuget

## Status

Done
- statements
- variables
- conditions
    - $var
    - &&
In Progress
- strings
    - $Include "this is a string"
- blocks
    - $Configuration { ... }
