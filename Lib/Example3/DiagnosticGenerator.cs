using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.IO;
using System.Linq;

namespace Lib.Example3
{
    [Generator]
    public class DiagnosticGenerator : AbstractSourceGenerator
    {
        private const string AttributeName = "AutoDiagnostic";

        public override void Execute(GeneratorExecutionContext context)
        {
            var files = context.AdditionalFiles.Where(x => x.Path.Contains(Namespace));
            foreach (var file in files)
            {
                var name = Path.GetFileNameWithoutExtension(file.Path);
                var sourceCode = GetSourceCodeFromFile(file, context);
                var sourceText = SourceText.From(sourceCode, Encoding);
                context.AddSource($"{name}Generated", sourceText);
                var compilation = context.CreateCompilation(sourceText);
                var matchings = compilation.FilterCompilation(AttributeName);
                if (matchings.Count > 0)
                {
                    var diagnostic = BuildDiagnostic();
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }

        private string GetSourceCodeFromFile(AdditionalText file, GeneratorExecutionContext context)
        {
            var content = file.GetText(context.CancellationToken).ToString();
            return content
                .Replace("{0}", Namespace)
                .Replace("{1}", AttributeName);
        }

        private Diagnostic BuildDiagnostic(DiagnosticSeverity severity = DiagnosticSeverity.Warning)
        {
            var upperSeverity = severity.ToString().ToUpper();
            var lowerSeverity = severity.ToString().ToLower();

            var id = $"AUTO-{upperSeverity}001";
            var message = $"This is {lowerSeverity}.";

            var diagnosticDescriptor = new DiagnosticDescriptor(
                id,
                AttributeName,
                message,
                Namespace,
                severity,
                true);

            return Diagnostic.Create(diagnosticDescriptor, Location.None);
        }
    }
}
