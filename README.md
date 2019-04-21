# QPC Parser

An F# implementation of a parser for the QPC/VPC file format. Should
be built as a library and used as the parser frontend for other generators.

## Dependencies

- FParsec via Nuget

## Status

Done
- statements
- variables
- strings
- conditions
    - $var
    - &&
    - ||
    - ()

In Progress
- blocks
    - $Configuration { ... }
