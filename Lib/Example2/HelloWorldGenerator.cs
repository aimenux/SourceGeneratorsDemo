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
            var files = context.AdditionalFiles.Where(x => x.Path.Contains(Namespace));
            foreach (var file in files)
            {
                var name = Path.GetFileNameWithoutExtension(file.Path);
                var sourceCode = GetSourceCodeFromFile(file, context);
                var sourceText = SourceText.From(sourceCode, Encoding.UTF8);
                context.AddSource($"{name}Generated", sourceText);
            }
        }

        private string GetSourceCodeFromFile(AdditionalText file, GeneratorExecutionContext context)
        {
            var content = file.GetText(context.CancellationToken).ToString();
            return content
                .Replace("{0}", Namespace)
                .Replace("{1}", Environment.UserName)
                .Replace("{2}", Dns.GetHostName())
                .Replace("{3}", DateTime.Now.ToString("F"));
        }
    }
}
