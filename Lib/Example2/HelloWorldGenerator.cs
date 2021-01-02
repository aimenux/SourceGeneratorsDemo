using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Lib.Example2
{
    [Generator]
    public class HelloWorldGenerator : AbstractSourceGenerator
    {
        public override void Execute(GeneratorExecutionContext context)
        {
            var files = context.AdditionalFiles.Where(at => at.Path.EndsWith(".txt"));
            foreach (var file in files)
            {
                var name = Path.GetFileNameWithoutExtension(file.Path);
                var sourceCode = GetSourceCodeFromFile(file, context);
                var sourceText = SourceText.From(sourceCode, Encoding.UTF8);
                context.AddSource($"{name}Generated", sourceText);
            }
        }

        private static string GetSourceCodeFromFile(AdditionalText file, GeneratorExecutionContext context)
        {
            var content = file.GetText(context.CancellationToken).ToString();

            return content
                .Replace("{0}", Environment.UserName)
                .Replace("{1}", Dns.GetHostName())
                .Replace("{2}", DateTime.Now.ToString("F"));
        }
    }
}
