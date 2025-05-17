using Microsoft.CodeAnalysis;

namespace QuantitiesDotNet.Generators;

public record QuantityDef(int L, int M, int T, int I, int Th, int N, int J)
{
    public static QuantityDef GetQuantityDef(AttributeData attr)
        => new(
            (int)attr.ConstructorArguments[0].Value!,
            (int)attr.ConstructorArguments[1].Value!,
            (int)attr.ConstructorArguments[2].Value!,
            (int)attr.ConstructorArguments[3].Value!,
            (int)attr.ConstructorArguments[4].Value!,
            (int)attr.ConstructorArguments[5].Value!,
            (int)attr.ConstructorArguments[6].Value!);
}
