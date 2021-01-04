using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;

namespace Lib
{
    public static class Extensions
    {
        public static Compilation CreateCompilation(this GeneratorExecutionContext context, SourceText sourceText)
        {
            var options = (context.Compilation as CSharpCompilation).SyntaxTrees[0].Options as CSharpParseOptions;
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceText, options);
            var compilation = context.Compilation.AddSyntaxTrees(syntaxTree);
            return compilation;
        }

        public static ICollection<AttributeSyntax> FilterCompilation(this Compilation compilation, string attributeName)
        {
            var syntaxNodes = compilation.SyntaxTrees.SelectMany(s => s.GetRoot().DescendantNodes());
            var syntaxAttributes = syntaxNodes
                .Where(x => x.IsKind(SyntaxKind.Attribute))
                .OfType<AttributeSyntax>()
                .Where(x => x.Name.ToString().EndsWith(attributeName))
                .ToList();
            return syntaxAttributes;
        }

        public static bool HasAttribute(this INamedTypeSymbol declaredSymbol, INamedTypeSymbol attributeSymbol)
        {
            var attributes = declaredSymbol
                .GetAttributes()
                .Select(x => x.AttributeClass)
                .Where(x => x?.Equals(attributeSymbol, SymbolEqualityComparer.Default) == true);

            return attributes.Any();
        }

        public static ICollection<IPropertySymbol> GetProperties(this INamedTypeSymbol typeSymbol)
        {
            var properties = new List<IPropertySymbol>();

            foreach (var member in typeSymbol.GetMembers())
            {
                if (member is IPropertySymbol propertySymbol)
                {
                    properties.Add(propertySymbol);
                }
            }

            return properties;
        }

        public static string GenerateParameters(this INamedTypeSymbol symbol)
        {
            var parameters = symbol
                .GetProperties()
                .Select(x => $"{x.Type.Name} {x.Name.ToLower()}")
                .ToList();

            return string.Join(", ", parameters);
        }

        public static string InitializeParameters(this INamedTypeSymbol symbol)
        {
            var parameters = symbol
                .GetProperties()
                .Select(x => $"obj.{x.Name} = {x.Name.ToLower()};")
                .ToList();

            return string.Join("\n", parameters);
        }
    }
}
