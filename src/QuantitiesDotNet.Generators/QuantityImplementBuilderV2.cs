using SourceGeneratorToolkit;
namespace QuantitiesDotNet.Generators;


internal abstract class QuantityImplementBuilderBase(
    string typeNameBase,
    string tValue,
    bool isRefLikeType,
    QuantityDef quantityDef,
    IList<UnitSymbolDef> unitSymbols,
    IList<UnitOperationDef> unitOperations)
{
    protected static readonly SourceStringHandler Empty = new(0, 0);

    public static (QuantityImplementBuilderBase NonGeneric, QuantityImplementBuilderBase Generic) Create(
        string typeNameBase,
        bool isRefLikeType,
        QuantityDef quantityDef,
        IList<UnitSymbolDef> unitSymbols,
        IList<UnitOperationDef> unitOperations)
    {
        var nonGeneric = new NonGenericQuantityImplementBuilder(
            typeNameBase,
            isRefLikeType,
            quantityDef,
            unitSymbols,
            unitOperations);
        var generic = new GenericQuantityImplementBuilder(
            typeNameBase,
            isRefLikeType,
            quantityDef,
            unitSymbols,
            unitOperations);
        return (nonGeneric, generic);
    }

    public string TypeNameBase => typeNameBase;
    public string TValue => tValue;
    public bool IsRefLikeType => isRefLikeType;
    public QuantityDef QuantityDef => quantityDef;
    public IList<UnitSymbolDef> UnitSymbols => unitSymbols;
    public IList<UnitOperationDef> UnitOperations => unitOperations;

    public UnitSymbolDef PrimaryUnit => _primaryUnit ??= GetPrimaryUnit();
    private UnitSymbolDef? _primaryUnit;
    private UnitSymbolDef GetPrimaryUnit() => UnitSymbols.FirstOrDefault() ?? new UnitSymbolDef("RawValue", "", 1, false);

    public abstract string TypeName { get; }
    public abstract string DocTypeName { get; }

    public abstract string UnitScaleFormat { get; }
    public abstract string OneValue { get; }

    public void Generate(SourceBuilderSlim sb)
    {
        GenerateTypeInit(sb);
        sb.AppendLine("""
        {
        """);
        GenerateMetadata(sb);
        GenerateBasicTypeShape(sb);
        GenerateUnitDefinitionsShape(sb);
        sb.AppendLine();
        GenerateSelfOperatorsShape(sb);
        sb.AppendLine("""
        }
        
        #region unit operations

        """);
        foreach (var op in UnitOperations)
        {
            GenerateExternalOperator(sb, op);
        }
        sb.AppendLine("""

        #endregion unit operations

        """);
        GenerateUnitShorthand(sb);
        sb.AppendLine("""

        """);

    }

    protected abstract void GenerateTypeInit(SourceBuilderSlim sb);

    protected abstract void GenerateMetadata(SourceBuilderSlim sb);

    private void GenerateBasicTypeShape(SourceBuilderSlim sb)
    {
        sb.AppendLine($$"""
            /// <summary> Gets the raw value of <see href="{{DocTypeName}}" />. </summary>
            public {{TValue}} RawValue => _rawValue;
            private readonly {{TValue}} _rawValue;
        
            internal {{TypeNameBase}}({{TValue}} rawValue)
                => _rawValue = rawValue;

        """);

        if (IsRefLikeType)
        {
            return;
        }
        sb.AppendLine($$"""
            #region basic type implements
        
            /// <inheritdoc />
            public int CompareTo(object? obj)
                => obj is {{TypeName}} other
                ? Compare(this, other)
                : throw new ArgumentException(nameof(obj));

            /// <inheritdoc />
            public int CompareTo({{TypeName}} other) => Compare(this, other);

            /// <inheritdoc />
            public bool Equals({{TypeName}} other) => Equals(this, other);

            /// <summary>
            /// Tries to parse a string into a value.
            /// </summary>
            /// <param name="s"></param>
            /// <param name="provider"></param>
            /// <param name="result"></param>
            /// <returns></returns>
            public static bool TryParse(string? s, IFormatProvider? provider, out {{TypeName}} result)
            {
                if(!QuantityParseInfo.TryCompile(s, out var info))
                {
                    result = default;
                    return false;
                }
                var (succeeded, value) = info.UnitSelector switch {
                    {{UnitSymbols
                            .Select(unit => $"\"{unit.ShortName}\" => ({TValue}.TryParse(info.Number, NumberStyles.Any, provider, out var x), From{unit.MajorName}(x!)),")
                            .PreserveIndent()}}
                    _ => (false, default({{TypeName}})),
                };
                result = value;
                return succeeded;
            }

            /// <summary>
            /// Parses a string into a value.
            /// </summary>
            /// <param name="s"></param>
            /// <param name="provider"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentNullException"></exception>
            /// <exception cref="FormatException"></exception>
            public static {{TypeName}} Parse(string? s, IFormatProvider? provider)
            {
                if(s is null)
                    throw new ArgumentNullException(nameof(s));
                if(!TryParse(s, provider, out var result))
                    throw new FormatException();
                return result;
            }

            /// <inheritdoc />
            public override string ToString()
                => ToString(null, CultureInfo.CurrentCulture);

            /// <summary>
            /// Formats the value of the current instance using the specified format.
            /// </summary>
            /// <param name="format">
            /// The format to use. -or- A null reference (Nothing in Visual Basic) to use the
            /// default format defined for the type of the System.IFormattable implementation.
            /// </param>
            /// <returns>
            /// The value of the current instance in the specified format.
            /// </returns>
            public string ToString(string? format)
                => ToString(format, CultureInfo.CurrentCulture);

            private (QuantityFormatInfo info, string number, string unit) GetFormatInfo(string? format, IFormatProvider? formatProvider)
            {
                if(!QuantityFormatInfo.TryCompile(format, out var info))
                {
                    throw new FormatException();
                }

                var (value, unit) = info.UnitSelector switch {
                    {{UnitSymbols
                            .Select(unit => $"\"{unit.ShortName}\" => ({unit.MajorName}, \"{unit.ShortName}\"),")
                            .PreserveIndent()}}
                    "" => ({{PrimaryUnit.MajorName}}, "{{PrimaryUnit.ShortName}}"),
                    _ => throw new FormatException(),
                };
                var number = string.Format(formatProvider, "{0:" + info.NumberFormat + "}", value);
                return (info, number, unit);
            }

            /// <inheritdoc />
            public string ToString(string? format, IFormatProvider? formatProvider)
            {
                var (info, number, unit) = GetFormatInfo(format, formatProvider);
                return info.Format(number, unit);
            }

            /// <summary>
            /// Tries to format the value of the current instance into the provided span of characters.
            /// </summary>
            /// <param name="destination">The span in which to write this instance's value formatted as a span of characters.</param>
            /// <param name="charsWritten">When this method returns, contains the number of characters that were written in <paramref name="destination"/>.</param>
            /// <param name="format">A span containing the characters that represent a standard or custom format string that defines the acceptable format for <paramref name="destination"/>.</param>
            /// <param name="formatProvider">An optional object that supplies culture-specific formatting information for <paramref name="destination"/>.</param>
            /// <returns><c>true</c> if the formatting was successful; otherwise, <c>false</c>.</returns>
            public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? formatProvider)
            {
                var (info, number, unit) = GetFormatInfo(format.ToString(), formatProvider);
                return info.TryFormat(destination, out charsWritten, number, unit);
            }

            /// <inheritdoc />
            public override int GetHashCode()
                => _rawValue.GetHashCode();

            /// <inheritdoc />
            public override bool Equals([NotNullWhen(true)] object? obj)
                => obj is {{TypeName}} other && Equals(this, other);

            #endregion  // basic type implements

        """);
    }

    private void GenerateUnitDefinitionsShape(SourceBuilderSlim sb)
    {
        var unitInfo = $"UnitMetadata<{TValue}>";
        sb.AppendLine($$"""
            #region unit definition implements

            /// <summary> The unit informations dictionary which is keyed by unit symbols. </summary>
            public static readonly ImmutableDictionary<string, {{unitInfo}}> UnitsBySymbol = GetUnitsBySymbol();
            private static ImmutableDictionary<string, {{unitInfo}}> GetUnitsBySymbol()
            {
                var builder = ImmutableDictionary.CreateBuilder<string, {{unitInfo}}>();
                {{UnitSymbols
                        .Select(unit => $"builder.Add(\"{unit.ShortName}\", {unit.MajorName}Info);")
                        .PreserveIndent()}}
                return builder.ToImmutable();
            }

        """);
        foreach (var unit in UnitSymbols)
        {
            sb.AppendLine($$"""
            #region {{unit.MajorName}}

            private static readonly {{TValue}} _{{unit.MajorName}}Scale = {{string.Format(UnitScaleFormat, unit.Scale)}};

            /// <summary> The information for [{{unit.ShortName}}]. </summary>
            public static readonly {{unitInfo}} {{unit.MajorName}}Info = new (_{{unit.MajorName}}Scale, "{{unit.MajorName}}", "{{unit.ShortName}}");

            /// <summary>
            /// Creates a new <see href="{{DocTypeName}}" /> instance by interpreting the given real value in the scale of [{{unit.ShortName}}].
            /// </summary>
            /// <param name="Second"></param>
            /// <returns></returns>
            public static {{TypeName}} From{{unit.MajorName}}({{TValue}} {{unit.MajorName}})
                => new ({{unit.MajorName}} * _{{unit.MajorName}}Scale);

            /// <summary> Gets the value of this instance in [{{unit.ShortName}}] scale. </summary>
            public {{TValue}} {{unit.MajorName}} => _rawValue / _{{unit.MajorName}}Scale;

            #endregion {{unit.MajorName}}

        """);
        }
        sb.AppendLine($$"""
            #endregion unit definition implements
        """);
    }

    private void GenerateSelfOperatorsShape(SourceBuilderSlim sb)
    {
        sb.AppendLine($$"""
            #region arithmetic operator implements

            /** <inheritdoc /> */ public static {{TypeName}} AdditiveIdentity       => default;
            /** <inheritdoc /> */ public static {{TValue}}   MultiplicativeIdentity => {{OneValue}};
            /** <inheritdoc /> */ public static {{TypeName}} operator +({{TypeName}} value) => value;
            /** <inheritdoc /> */ public static {{TypeName}} operator -({{TypeName}} value) => new(-value._rawValue);
            /** <inheritdoc /> */ public static {{TypeName}} operator +({{TypeName}} x, {{TypeName}} y) => new (x._rawValue + y._rawValue);
            /** <inheritdoc /> */ public static {{TypeName}} operator -({{TypeName}} x, {{TypeName}} y) => new (x._rawValue - y._rawValue);
            /** <inheritdoc /> */ public static {{TypeName}} operator %({{TypeName}} x, {{TypeName}} y) => new(x._rawValue % y._rawValue);
            /** <inheritdoc /> */ public static {{TypeName}} operator *({{TValue}}   x, {{TypeName}} y) => new (x * y._rawValue);
            /** <inheritdoc /> */ public static {{TypeName}} operator *({{TypeName}} x, {{TValue}}   y) => new (x._rawValue * y);
            /** <inheritdoc /> */ public static {{TValue}}   operator /({{TypeName}} x, {{TypeName}} y) => x._rawValue / y._rawValue;
            /** <inheritdoc /> */ public static {{TypeName}} operator /({{TypeName}} x, {{TValue}}   y) => new(x._rawValue / y);

            #endregion arithmetic operator implements

        """);
        if(IsRefLikeType)
        {
            return;
        }
        sb.AppendLine($$"""
            #region comparison operator implements

            /// <summary> Determines whether the 2 values are same or not. </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public static bool Equals(in {{TypeName}} x, in {{TypeName}} y) => x._rawValue == y._rawValue;

            /// <summary> Determines which value is greater than another. </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public static int Compare(in {{TypeName}} x, in {{TypeName}} y)
            {
                if (x._rawValue == y._rawValue) { return 0; }
                return x._rawValue < y._rawValue ? -1 : 1;
            }

            /** <inheritdoc /> */ public static bool operator ==({{TypeName}} x, {{TypeName}} y) => Equals(x, y);
            /** <inheritdoc /> */ public static bool operator !=({{TypeName}} x, {{TypeName}} y) => !Equals(x, y);
            /** <inheritdoc /> */ public static bool operator < ({{TypeName}} x, {{TypeName}} y) => Compare(x, y) <  0;
            /** <inheritdoc /> */ public static bool operator > ({{TypeName}} x, {{TypeName}} y) => Compare(x, y) >  0;
            /** <inheritdoc /> */ public static bool operator <=({{TypeName}} x, {{TypeName}} y) => Compare(x, y) <= 0;
            /** <inheritdoc /> */ public static bool operator >=({{TypeName}} x, {{TypeName}} y) => Compare(x, y) >= 0;

            #endregion comparison operator implements

        """);
    }

    private void GenerateExternalOperator(SourceBuilderSlim sb, UnitOperationDef op)
    {
        var product = GetRelativeType(op.ProductType);
        var multiplicant = GetRelativeType(op.MultiplicantType);
        var multiplier = GetRelativeType(op.MultiplierType);
        var divisonOperator1If = !IsRefLikeType
            ? (SourceStringHandler)$$"""

                #if NET7_0_OR_GREATER
                    : IDivisionOperators<{{product}}, {{multiplicant}}, {{multiplier}}>
                #endif
                """
            : Empty;
        var multiplyOperator1If = !IsRefLikeType
            ? (SourceStringHandler)$$"""

                #if NET7_0_OR_GREATER
                    : IMultiplyOperators<{{multiplicant}}, {{multiplier}}, {{product}}>
                #endif
                """
            : Empty;
        var divisionOperator2If = !IsRefLikeType
            ? (SourceStringHandler)$$"""

                #if NET7_0_OR_GREATER
                    : IDivisionOperators<{{product}}, {{multiplier}}, {{multiplicant}}>
                #endif
                """
            : Empty;
        var multiplyOperator2If = !IsRefLikeType
            ? (SourceStringHandler)$$"""

                #if NET7_0_OR_GREATER
                    : IMultiplyOperators<{{multiplier}}, {{multiplicant}}, {{product}}>
                #endif
                """
            : Empty;

        sb.AppendLine($$"""
        partial struct {{product}}{{divisonOperator1If}}
        {
            /// <inheritdoc />
            public static {{multiplier}} operator /({{product}} x, {{multiplicant}} y) => new(x.RawValue / y.RawValue);
        }

        partial struct {{multiplicant}}{{multiplyOperator1If}}
        {
            /// <inheritdoc />
            public static {{product}} operator *({{multiplicant}} x, {{multiplier}} y) => new(x.RawValue * y.RawValue);
        }
        
        """);
        if(multiplicant == multiplier)
        {
            return;
        }
        sb.AppendLine($$"""
        partial struct {{product}}{{divisionOperator2If}}
        {
            /// <inheritdoc />
            public static {{multiplicant}} operator /({{product}} x, {{multiplier}} y) => new(x.RawValue / y.RawValue);
        }

        partial struct {{multiplier}}{{multiplyOperator2If}}
        {
            /// <inheritdoc />
            public static {{product}} operator *({{multiplier}} x, {{multiplicant}} y) => new(x.RawValue * y.RawValue);
        }

        """);
        }

    private void GenerateUnitShorthand(SourceBuilderSlim sb)
    {
        var unitShorthands = GetRelativeType("UnitShorthands");
        sb.AppendLine($$"""
            partial class {{unitShorthands}}
            {
                {{
                    UnitSymbols
                        .Where(static x => x.ExportsShorthandSymbol)
                        .Select(x => (SourceStringHandler)$$"""
                        /// <summary> A symbol for <see cref="{{DocTypeName}}" />. </summary>
                        [CLSCompliant(false)]
                        public static readonly {{TypeName}} {{x.ShortName}} = new({{string.Format(UnitScaleFormat, x.Scale)}});
                        
                        """)
                        .PreserveIndent()
                }}
            }

            """);
    }

    protected abstract string GetRelativeType(string typeNameBase);
}


internal sealed class NonGenericQuantityImplementBuilder(
    string typeNameBase,
    bool isRefLikeType,
    QuantityDef quantityDef,
    IList<UnitSymbolDef> unitSymbols,
    IList<UnitOperationDef> unitOperations)
    : QuantityImplementBuilderBase(typeNameBase, "double", isRefLikeType, quantityDef, unitSymbols, unitOperations)
{
    public override string TypeName => TypeNameBase;
    public override string DocTypeName => TypeNameBase;
    public override string UnitScaleFormat => "{0}";
    public override string OneValue => "1.0";

    protected override void GenerateTypeInit(SourceBuilderSlim sb)
    {
        var quantityIf = !IsRefLikeType
                        ? (SourceStringHandler)$$"""
                
                    : IQuantity<{{TypeName}}, {{TValue}}>
                #if NET7_0_OR_GREATER
                    , IMultiplyOperators<{{TypeName}}, {{TValue}}, {{TypeName}}>
                    , IDivisionOperators<{{TypeName}}, {{TValue}}, {{TypeName}}>
                    , IDivisionOperators<{{TypeName}}, {{TypeName}}, {{TValue}}>
                #endif
                """
            : Empty;
        sb.AppendLine($$"""
        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = sizeof(double))]
        public partial struct {{TypeName}}{{quantityIf}}
        """);
    }

    protected override void GenerateMetadata(SourceBuilderSlim sb)
    {
        sb.AppendLine($$"""
            // for reflection of ref struct, explicitly named backing field is provided.
            internal static readonly QuantityMetadata _Metadata = new(
                "{{TypeNameBase.Substring(1)}}",
                L : {{QuantityDef.L}},
                M : {{QuantityDef.M}},
                T : {{QuantityDef.T}},
                I : {{QuantityDef.I}},
                Th: {{QuantityDef.Th}},
                N : {{QuantityDef.N}},
                J : {{QuantityDef.J}});
    
            /// <summary> Gets quantity metadata instance for <see cref="{{DocTypeName}}" />. </summary>
            public static QuantityMetadata Metadata => _Metadata;

            /// <summary> Gets quantity metadata instance for <see cref="{{DocTypeName}}" />. </summary>
            public QuantityMetadata MetadataInstance => Metadata;

        """);
    }

    protected override string GetRelativeType(string typeNameBase)
        => typeNameBase;
}


internal sealed class GenericQuantityImplementBuilder(
    string typeNameBase,
    bool isRefLikeType,
    QuantityDef quantityDef,
    IList<UnitSymbolDef> unitSymbols,
    IList<UnitOperationDef> unitOperations)
    : QuantityImplementBuilderBase(typeNameBase, "T", isRefLikeType, quantityDef, unitSymbols, unitOperations)
{
    public override string TypeName => $"{TypeNameBase}<{TValue}>";
    public override string DocTypeName => TypeNameBase;
    public override string UnitScaleFormat => "T.CreateSaturating({0})";
    public override string OneValue => "T.One";

    protected override void GenerateTypeInit(SourceBuilderSlim sb)
    {
        var quantityIf = !IsRefLikeType
            ? (SourceStringHandler)$$"""
                
                    : IQuantity<{{TypeName}}, {{TValue}}>
                    , IMultiplyOperators<{{TypeName}}, {{TValue}}, {{TypeName}}>
                    , IDivisionOperators<{{TypeName}}, {{TValue}}, {{TypeName}}>
                    , IDivisionOperators<{{TypeName}}, {{TypeName}}, {{TValue}}>
                """
            : Empty;
        sb.AppendLine($$"""
        public partial struct {{TypeName}}{{quantityIf}}
            where T : INumber<T>
        """);
    }

    protected override void GenerateMetadata(SourceBuilderSlim sb)
    {
        sb.AppendLine($$"""
            /// <summary> Gets quantity metadata instance for <see cref="{{DocTypeName}}" />. </summary>
            public static QuantityMetadata Metadata => {{TypeNameBase}}.Metadata;

            /// <summary> Gets quantity metadata instance for <see cref="{{DocTypeName}}" />. </summary>
            public QuantityMetadata MetadataInstance => {{TypeNameBase}}.Metadata;

        """);
    }

    protected override string GetRelativeType(string typeNameBase)
        => $"{typeNameBase}<{TValue}>";
}

