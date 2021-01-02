using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Net;
using System.Text;

namespace Lib
{
    [Generator]
    public class HelloWorldGenerator : ISourceGenerator
    {
        public const string Namespace = "HelloWorldGenerated";

        public void Execute(GeneratorExecutionContext context)
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
            Console.WriteLine(""Compiled on: {Dns.GetHostName()}"");
            Console.WriteLine(""Compiled at: {DateTime.Now:F}"");
        }}
    }}
}}";

            context.AddSource(Namespace, SourceText.From(sourceCode, Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
