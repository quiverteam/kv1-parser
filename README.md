# KeyValues Parser

An F# implementation of a parser for the KeyValues file format. Used in QPC
to parse qpc and qgc files.

## Dependencies

- FParsec via Nuget

## Status

### Done

- statements
- variables
- strings
- conditions
  - $var
  - &&
  - ||
  - ()

###In Progress

- blocks
  - $Configuration { ... }
