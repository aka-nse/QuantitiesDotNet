using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;

namespace QuantitiesDotNet.Generators;

public record QuantityOperation(
    string MultiplicantType,
    string MultiplierType,
    string ProductType)
{
    private static readonly Regex _TypeNameToSymbolMatcher = new(@"^Q(\w+)$");
    public static string TargetTypeToSymbol(string targetTypeName)
        => _TypeNameToSymbolMatcher.Replace(targetTypeName, "$1UnitSymbol");

    public string MultiplicantSymbol => _MultiplicantSymbol ??= TargetTypeToSymbol(MultiplicantType);
    private string? _MultiplicantSymbol;

    public string MultiplierSymbol => _MultiplierSymbol ??= TargetTypeToSymbol(MultiplierType);
    private string? _MultiplierSymbol;

    public string ProductSymbol => _ProductSymbol ??= TargetTypeToSymbol(ProductType);
    private string? _ProductSymbol;

    private static class QuantityOperationAttributeFields
    {
        public const int MultiplicantType = 0;
        public const int MultiplierType = 1;
        public const int ProductType = 2;
    }

    public QuantityOperation(AttributeData attr)
        : this(
              GetMultiplicantType(attr),
              GetMultiplierType(attr),
              GetProductType(attr))
    {
    }

    public static ImmutableArray<QuantityOperation> GetOperations(IEnumerable<AttributeData> enumerable) =>
        [.. enumerable.Select(attr => new QuantityOperation(attr))];

    private static string GetMultiplicantType(AttributeData attr)
        => (attr.ConstructorArguments[QuantityOperationAttributeFields.MultiplicantType].Value as INamedTypeSymbol)
            ?.Name
            ?? throw new InvalidOperationException();

    private static string GetMultiplierType(AttributeData attr)
        => (attr.ConstructorArguments[QuantityOperationAttributeFields.MultiplierType].Value as INamedTypeSymbol)
            ?.Name
            ?? throw new InvalidOperationException();

    private static string GetProductType(AttributeData attr)
        => (attr.ConstructorArguments[QuantityOperationAttributeFields.ProductType].Value as INamedTypeSymbol)
            ?.Name
            ?? throw new InvalidOperationException();

}
