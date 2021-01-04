using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Net;

namespace Lib.Example1
{
    [Generator]
    public class HelloWorldGenerator : AbstractSourceGenerator
    {
        public override void Execute(GeneratorExecutionContext context)
        {
            var sourceCode =
$@"
using System;
namespace {Namespace}
{{
    public static class HelloWorld
    {{
        public static void SayHello()
        {{
            Console.WriteLine(""Hello from generated code"");
            Console.WriteLine(""Compiled by: {Environment.UserName}"");
            Console.WriteLine(""Compiled on: {Dns.GetHostName()}"");
            Console.WriteLine(""Compiled at: {DateTime.Now:F}"");
        }}
    }}
}}";
            var sourceText = SourceText.From(sourceCode, Encoding);
            context.AddSource($"{Namespace}Generated", sourceText);
        }
    }
}
