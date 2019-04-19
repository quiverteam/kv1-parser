---
title: Spec
permalink: /spec
layout: default
---
# QPC Spec

This page provides a specification of the QPC file format. It is largely based on
Valve's VPC format, with some additions. We hope for QPC to be backwards compatible
with VPC scripts in the future.

## Variables

Variables in QPC must begin with a dollar sign (`$`) and contain only one or more of
the characters `a-z`, `A-Z`, `0.9`, `_` or `-`.

```perl
$This_is_a_variable
```

## Comments

Comments begin with two slashes and span until the next LF character (`\n`).

```c
// This is a single line comment
```

In addition to the single line comments supported by VPC, QPC also supports multi line
c-style comments:

```c
/**
 * This is a multiline comment
 */
```

## Blocks

In QPC blocks serve as a generic type which can be used for a variety of things.
Blocks are delimited by curly braces (`{ ... }`) and can contain other blocks, strings,
variables, or any other QPC syntax.

```perl
$Foo
{
    "Bar"
}
```

## Conditions

Conditions are delimited by square brackets (`[ ... ]`) and contain logic that dictates
whether the preceding item should be included or not. For example, consider the following
scenario: a certain source file is to be used on windows, and another on posix systems. Let's
assume that `$WINDOWS` and `$POSIX` are defined on windows and posix systems, respectively.

```perl
{
    $File "foo.cpp" [$POSIX]
    $File "bar.cpp" [$WINDOWS]
}
```

The operators `||`, `&&`, and `!` can be used, which function exactly as they would in C.
They all have equal precedense.
