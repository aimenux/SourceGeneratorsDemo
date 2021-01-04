using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.CSharp;
using System.IO;
using System.Linq;
using System.Text;

namespace Lib.Example5
{
    [Generator]
    public class BuilderGenerator : AbstractSourceGenerator
    {
        private const string AttributeName = "AutoBuilder";

        private string FullAttributeName => $"{Namespace}.{AttributeName}Attribute";

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
                var attributeSymbol = compilation.GetTypeByMetadataName(FullAttributeName);
                var candidateClasses = ((SyntaxReceiver)context.SyntaxReceiver).CandidateClasses;
                foreach (var candidateClass in candidateClasses)
                {
                    var semanticModel = compilation.GetSemanticModel(candidateClass.SyntaxTree);
                    var declaredSymbol = semanticModel.GetDeclaredSymbol(candidateClass);
                    if (declaredSymbol.HasAttribute(attributeSymbol))
                    {
                        sourceCode = GenerateSourceCode(declaredSymbol);
                        sourceText = SourceText.From(sourceCode, Encoding);
                        context.AddSource($"{declaredSymbol.Name}Builder.cs", sourceText);
                    }
                }
            }
        }

        public override void Initialize(GeneratorInitializationContext context)
        {
            base.Initialize(context);
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        private string GetSourceCodeFromFile(AdditionalText file, GeneratorExecutionContext context)
        {
            var content = file.GetText(context.CancellationToken).ToString();
            return content
                .Replace("{0}", Namespace)
                .Replace("{1}", AttributeName);
        }

        private string GenerateSourceCode(INamedTypeSymbol symbol)
        {
            var sb = new StringBuilder();
            var symbolName = symbol.Name;
            var symbolNamespace = symbol.ContainingNamespace.Name;
            var symbolParameters = symbol.GenerateParameters();
            var symbolInitializedParameters = symbol.InitializeParameters();
            sb.AppendLine("using System;");
            sb.AppendLine($"namespace {symbolNamespace} {{");
            sb.AppendLine($"public class {symbolName}Builder {{");
            sb.AppendLine($"public {symbolName} Build ({symbolParameters}) {{");
            sb.AppendLine($"var obj = new {symbolName}();");
            sb.AppendLine($"{symbolInitializedParameters}");
            sb.AppendLine("return obj;}}}");
            return sb.ToString();
        }
    }
}
