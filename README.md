﻿![.NET 5.0](https://github.com/aimenux/SourceGeneratorsDemo/workflows/.NET%205.0/badge.svg)
# SourceGeneratorsDemo
```
Playing with source generators (net 5.0 feature)
```

> In this demo, i m building some [source generators](https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/). 
> - `Example1.HelloWorldGenerator` : a basic source generator saying hello, the source code is retrieved from an internal string field.
> - `Example2.HelloWorldGenerator` : a basic source generator saying hello, the source code is retrieved from an external textual file.
> - `Example3.DiagnosticGenerator` : a source generator displaying diagnostics like infos, warnings or errors at compilation time.
> - `Example4.IsPrimeGenerator` : a source generator adding a dummy extension method to check if a number is prime or not.
> - `Example5.BuilderGenerator` : a source generator adding a dummy builder to public classes decorated with some attribute.
> - `Example6.ToStringGenerator` : a source generator adding ToString method to partial classes decorated with some attribute.
>
> :gear: In order to debug source generators, set `isDebuggingEnabled` to `true` in debug mode.

**`Tools`** : vs19, net 5.0