using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace QuantitiesDotNet.Generators;

public sealed record QuantityDef(
    string TypeName,
    bool IsRefLike,
    Dimension Dimension,
    ImmutableArray<UnitSymbol> UnitSymbols,
    ImmutableArray<QuantityOperation> Equations)
{
    public override int GetHashCode() => TypeName.GetHashCode();

    public bool Equals(QuantityDef? other) =>
        Equals(this, other);

    public static bool Equals(QuantityDef? x, QuantityDef? y) =>
        (x, y) switch
        {
            (null, null) => true,
            (null, _) => false,
            (_, null) => false,
            _ => x.TypeName == y.TypeName
                && x.IsRefLike == y.IsRefLike
                && x.Dimension.Equals(y.Dimension)
                && x.UnitSymbols.SequenceEqual(y.UnitSymbols)
                && x.Equations.SequenceEqual(y.Equations),
        };
}

public record QuantityDef_(int L, int M, int T, int I, int Th, int N, int J)
{
    public static QuantityDef_ GetQuantityDef(AttributeData attr)
        => new(
            (int)attr.ConstructorArguments[0].Value!,
            (int)attr.ConstructorArguments[1].Value!,
            (int)attr.ConstructorArguments[2].Value!,
            (int)attr.ConstructorArguments[3].Value!,
            (int)attr.ConstructorArguments[4].Value!,
            (int)attr.ConstructorArguments[5].Value!,
            (int)attr.ConstructorArguments[6].Value!);
}


public record Dimension(int L, int M, int T, int I, int Th, int N, int J)
{
    public static Dimension GetDimension(AttributeData attr)
        => new(
            (int)attr.ConstructorArguments[0].Value!,
            (int)attr.ConstructorArguments[1].Value!,
            (int)attr.ConstructorArguments[2].Value!,
            (int)attr.ConstructorArguments[3].Value!,
            (int)attr.ConstructorArguments[4].Value!,
            (int)attr.ConstructorArguments[5].Value!,
            (int)attr.ConstructorArguments[6].Value!);
}