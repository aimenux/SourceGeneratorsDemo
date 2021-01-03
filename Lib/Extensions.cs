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
    }
}
