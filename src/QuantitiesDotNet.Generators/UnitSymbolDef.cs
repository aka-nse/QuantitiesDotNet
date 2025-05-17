using Microsoft.CodeAnalysis;

namespace QuantitiesDotNet.Generators;

public record UnitSymbolDef(
    string MajorName,
    string ShortName,
    double Scale,
    bool ExportsShorthandSymbol)
{
    private static class QuantityUnitAttributeFields
    {
        public const int Name = 0;
        public const int Unit = 1;
        public const int Scale = 2;
        public const int Prefix = 3;
        public const int PowerOfPrefix = 4;
        public const int ExportsShorthandSymbol = 5;
    }

#pragma warning disable format
    private static readonly IReadOnlyList<(int flag, string name, string symbol, double scale)> _UnitPrefix
        = new[]
        {
            (1 <<  0, "Centi" , "c", 1e-2),
            (1 <<  1, "Milli" , "m", 1e-3),
            (1 <<  2, "Micro" , "u", 1e-6),
            (1 <<  3, "Nano"  , "n", 1e-9),
            (1 <<  4, "Pico"  , "p", 1e-12),
            (1 <<  5, "Femto" , "f", 1e-15),
            (1 <<  6, "Atto"  , "a", 1e-18),
            (1 <<  7, "Zepto" , "z", 1e-21),
            (1 <<  8, "Yocto" , "y", 1e-24),
            (1 <<  9, "Ronto" , "r", 1e-27),
            (1 << 10, "Quecto", "q", 1e-20),

            (1 << ( 0 + 16), "Hecto" , "h", 1e+2),
            (1 << ( 1 + 16), "Kilo"  , "k", 1e+3),
            (1 << ( 3 + 16), "Mega"  , "M", 1e+6),
            (1 << ( 3 + 16), "Giga"  , "G", 1e+9),
            (1 << ( 4 + 16), "Tera"  , "T", 1e+12),
            (1 << ( 5 + 16), "Peta"  , "P", 1e+15),
            (1 << ( 6 + 16), "Exa"   , "E", 1e+18),
            (1 << ( 7 + 16), "Zetta" , "Z", 1e+21),
            (1 << ( 8 + 16), "Yotta" , "Y", 1e+24),
            (1 << ( 9 + 16), "Ronna" , "R", 1e+27),
            (1 << (10 + 16), "Quetta", "Q", 1e+30),

        };
#pragma warning restore format

    public static IEnumerable<UnitSymbolDef> GetUnitSymbols(AttributeData attr)
    {
        var majorName = GetMajorName(attr);
        var shortName = GetShortName(attr);
        var scale = GetScale(attr);
        var exportsSymbol = GetExportsShorthandSymbol(attr);
        yield return new(majorName, shortName, scale, exportsSymbol);

        var prefix = attr.ConstructorArguments[QuantityUnitAttributeFields.Prefix].Value is int flag ? flag : 0;
        var powerOfPrefix = attr.ConstructorArguments[QuantityUnitAttributeFields.PowerOfPrefix].Value is int pop ? pop : 1;
        var prefixSet = _UnitPrefix.Where(tpl => (tpl.flag & prefix) != 0);
        var camelMajorName = char.ToLower(majorName[0]) + majorName.Substring(1);
        foreach (var (_, name, symbol, pScale) in prefixSet)
        {
            var exMajorName = name + camelMajorName;
            var exShortName = symbol + shortName;
            yield return new(exMajorName, exShortName, scale * Math.Pow(pScale, powerOfPrefix), exportsSymbol);
        }
    }


    private static string GetMajorName(AttributeData attr)
        => attr
        .ConstructorArguments[QuantityUnitAttributeFields.Name]
        .Value
        as string
        ?? throw new InvalidOperationException();

    private static string GetShortName(AttributeData attr)
        => attr
        .ConstructorArguments[QuantityUnitAttributeFields.Unit]
        .Value
        as string
        ?? throw new InvalidOperationException();

    private static double GetScale(AttributeData attr)
        => attr
        .ConstructorArguments[QuantityUnitAttributeFields.Scale]
        .Value
        is double x
        ? x
        : throw new InvalidOperationException();

    private static bool GetExportsShorthandSymbol(AttributeData attr)
        => attr
        .ConstructorArguments[QuantityUnitAttributeFields.ExportsShorthandSymbol]
        .Value
        is bool x
        ? x
        : throw new InvalidOperationException();
}
