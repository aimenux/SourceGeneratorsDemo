using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Net;
using System.Text;

namespace Lib.Example1
{
    [Generator]
    public class HelloWorldGenerator : AbstractSourceGenerator
    {
        private const string Namespace = "Example1.HelloWorldGenerated";

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

            context.AddSource(Namespace, SourceText.From(sourceCode, Encoding.UTF8));
        }
    }
}
