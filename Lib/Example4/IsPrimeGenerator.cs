using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.IO;
using System.Linq;

namespace Lib.Example4
{
    [Generator]
    public class IsPrimeGenerator : AbstractSourceGenerator
    {
        public override void Execute(GeneratorExecutionContext context)
        {
            var files = context.AdditionalFiles.Where(x => x.Path.Contains(Namespace));
            foreach (var file in files)
            {
                var name = Path.GetFileNameWithoutExtension(file.Path);
                var sourceCode = GetSourceCodeFromFile(file, context);
                var sourceText = SourceText.From(sourceCode, Encoding);
                context.AddSource($"{name}Generated", sourceText);
            }
        }

        private string GetSourceCodeFromFile(AdditionalText file, GeneratorExecutionContext context)
        {
            var content = file.GetText(context.CancellationToken).ToString();
            return content.Replace("{0}", Namespace);
        }
    }
}
