---
title: Spec
permalink: /spec
layout: default
---
# KeyValues Spec

This page provides a specification of the KeyValues file format. It is largely based on
Valve's KeyValues documentation with some additions specific to this implementation.

## Keys and Values

As the name suggests KeyValues (referred to as KV from now on) is based on key value pairs.
A can be either a space terminated identifier (e.g: `foo`), or a double quoted string. Values
can be either an ident, string, or a block, which allows nesting.

## Blocks

```pl
block_key {
    "I'm a key in a block" "and I'm it's value"
    Im_another_key "im also a value"
}
```

## Conditions

One thing that makes KV more powerfull than other formants like JSON is the ability to make
parse-time decissions in it. This is especially useful for things like QPC/VPC, where these
decisions can be used to easily make conditional building decisions. Conditions are enclosed
in square brackets and follow a key value pair. They contain some conditions that must be
fulfilled in order for the preceeding KV pair to be included.

```pl
key "included if $WINDOWS is defined" [$WINDOWS]
key "included if $POSIX OR $OSX is defined" [$POSIX || $OSX]
key "included if $FOO AND ($BAR OR $BAZ) are defined" [$FOO && $BAR || $BAZ]
key "included if ($FOO AND $BAR) OR $BAZ are defined" [($FOO && $BAR) || $BAZ]
```

## Comments

C++ and C style comments are both supported. They work as expected. Nesting C style comments
is broken. These work significantly better than Valve's implementation. Now a single slash
will not be interpreted as a comment. How I managed to write something less broken than a
massive company with millions of dollars of resources I don't know but I guess that goes to
show how shit valve is.

```cpp
foo bar // this is a comment
baz /* inline comment */ quux
```
