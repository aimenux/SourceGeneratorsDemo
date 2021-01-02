![.NET 5.0](https://github.com/aimenux/SourceGeneratorsDemo/workflows/.NET%205.0/badge.svg)
# SourceGeneratorsDemo
```
Playing with source generators
```

> In this demo, i m building some [source generators](https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/). 
> - `Example1.HelloWorldGenerator` : a basic source generator saying hello, source code is retrieved from an internal string field
> - `Example2.HelloWorldGenerator` : a basic source generator saying hello, source code is retrieved from an external textual file
>
>> In order to debug source generators, we can set `isDebuggingEnabled` to `true` and launch solution compilation in debug mode.

**`Tools`** : vs19, net 5.0