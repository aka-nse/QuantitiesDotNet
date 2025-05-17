using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace QuantitiesDotNet.Generators;

[Generator(LanguageNames.CSharp)]
public partial class Generator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var quantityAttributeSymbol = context
            .CompilationProvider
            .GetMetadata("QuantitiesDotNet.QuantityAttribute");
        var quantityUnitAttributeSymbol = context
            .CompilationProvider
            .GetMetadata("QuantitiesDotNet.QuantityUnitAttribute");
        var quantityOperationAttributeSymbol = context
            .CompilationProvider
            .GetMetadata("QuantitiesDotNet.QuantityOperationAttribute");
        var attributedFiles = context.SyntaxProvider
            .FindAttributedMembers<StructDeclarationSyntax, INamedTypeSymbol>(quantityAttributeSymbol);

        var source = attributedFiles
            .Combine(quantityAttributeSymbol
                .Combine(quantityUnitAttributeSymbol
                    .Combine(quantityOperationAttributeSymbol)));
        context.RegisterSourceOutput(source, GenerateUnitTypeImplements);
    }


    private void GenerateUnitTypeImplements(
        SourceProductionContext context,
        (AttributedMemberInfo<INamedTypeSymbol> info,
        (INamedTypeSymbol qAttr, (INamedTypeSymbol unitAttr, INamedTypeSymbol opAttr))) tpl)
    {
        var canceller = context.CancellationToken;
        canceller.ThrowIfCancellationRequested();

        var (info, (qAttr, (qUnitAttr, qOpAttr))) = tpl;
        var attributes = info.TargetSymbol.GetAttributes();
        var qDef = attributes
            .Single(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, qAttr));
        var unitDefs = attributes
            .Where(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, qUnitAttr));
        var operationDefs = attributes
            .Where(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, qOpAttr));

        var generator = new QuantityImplementBuilder(
            info.TargetSymbol.Name,
            info.TargetSymbol.IsRefLikeType,
            QuantityDef.GetQuantityDef(qDef),
            [.. unitDefs.SelectMany(UnitSymbolDef.GetUnitSymbols)],
            [.. operationDefs.Select(static attr => new UnitOperationDef(attr))]);
        var sb = new StringBuilder();
        generator.Generate(sb, context.CancellationToken);
        context.AddSource(
            $"{info.TargetSymbol.Name}.g.cs",
            sb.ToString());
    }
}