using Microsoft.CodeAnalysis;

namespace QuantitiesDotNet.Generators;

internal static class InternalHelpers
{
    private static SymbolEqualityComparer Comparer => SymbolEqualityComparer.Default;

    public static IEnumerable<AttributeData> GetAttributes(
        this GeneratorAttributeSyntaxContext context,
        string attributeFullName)
    {
        var attrType = context.SemanticModel.Compilation.GetTypeByMetadataName(attributeFullName);
        return context
            .TargetSymbol
            .GetAttributes()
            .Where(attr => Comparer.Equals(attr.AttributeClass, attrType));
    }
}