using System.Text;
using Microsoft.CodeAnalysis;

namespace QuantitiesDotNet.Generators;

internal class QuantityImplementBuilder(
    string TargetTypeName,
    bool IsRefLikeType,
    QuantityDef QuantityDef,
    IList<UnitSymbolDef> UnitSymbols,
    IList<UnitOperationDef> UnitOperations
    )
{
    public UnitSymbolDef PrimaryUnit => _primaryUnit ??= GetPrimaryUnit();
    private UnitSymbolDef? _primaryUnit;
    private UnitSymbolDef GetPrimaryUnit() => UnitSymbols.FirstOrDefault() ?? new UnitSymbolDef("RawValue", "", 1, false);


    public void Generate(StringBuilder sb, CancellationToken canceller)
        => Generate(new StringBuilderWrapper(sb, canceller));


    private void Generate(StringBuilderWrapper sb)
    {
        sb.AppendLine($$"""
#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace QuantitiesDotNet
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = sizeof(double))]
    public partial struct {{TargetTypeName}}
"""); if (!IsRefLikeType) { sb.AppendLine($$"""
        : IQuantity<{{TargetTypeName}}, double>
    #if NET7_0_OR_GREATER
        , IMultiplyOperators<{{TargetTypeName}}, double, {{TargetTypeName}}>
        , IDivisionOperators<{{TargetTypeName}}, double, {{TargetTypeName}}>
        , IDivisionOperators<{{TargetTypeName}}, {{TargetTypeName}}, double>
    #endif
"""); }
        sb.AppendLine($$"""
    {
        /// <summary>
        /// Gets quantity metadata instance for <see cref="{{TargetTypeName}}" />.
        /// </summary>
        public static QuantityMetadata Metadata => _Metadata;

        // for reflection of ref struct, explicitly named backing field is provided.
        internal static readonly QuantityMetadata _Metadata = new(
            "{{TargetTypeName.Substring(1)}}",
            L : {{QuantityDef.L}},
            M : {{QuantityDef.M}},
            T : {{QuantityDef.T}},
            I : {{QuantityDef.I}},
            Th: {{QuantityDef.Th}},
            N : {{QuantityDef.N}},
            J : {{QuantityDef.J}});

        /// <summary>
        /// Gets quantity metadata instance for <see cref="{{TargetTypeName}}" />.
        /// </summary>
        public QuantityMetadata MetadataInstance => Metadata;

        private readonly double _RawValue;

        /// <summary>
        /// The raw value of <see href="{{TargetTypeName}}" />.
        /// </summary>
        public double RawValue => _RawValue;

        internal {{TargetTypeName}}(double rawValue)
            => _RawValue = rawValue;

"""); GenerateBasicTypeShape(sb, isGeneric: false); sb.AppendLine($$"""

"""); GenerateUnitDefinitionsShape(sb, isGeneric: false); sb.AppendLine($$"""

"""); GenerateSelfOperatorsShape(sb, isGeneric: false); sb.AppendLine($$"""
    }


    #region unit operations
"""); foreach (var op in UnitOperations) { sb.AppendLine($$"""

    partial struct {{op.ProductType}}
"""); if (!IsRefLikeType) { sb.AppendLine($$"""
    #if NET7_0_OR_GREATER
        : IDivisionOperators<{{op.ProductType}}, {{op.MultiplicantType}}, {{op.MultiplierType}}>
    #endif
"""); } sb.AppendLine($$"""
    {
        /// <inheritdoc />
        public static {{op.MultiplierType}} operator /({{op.ProductType}} x, {{op.MultiplicantType}} y) => new(x.RawValue / y.RawValue);
    }

    partial struct {{op.MultiplicantType}}
"""); if (!IsRefLikeType) { sb.AppendLine($$"""
    #if NET7_0_OR_GREATER
        : IMultiplyOperators<{{op.MultiplicantType}}, {{op.MultiplierType}}, {{op.ProductType}}>
    #endif
"""); } sb.AppendLine($$"""
    {
        /// <inheritdoc />
        public static {{op.ProductType}} operator *({{op.MultiplicantType}} x, {{op.MultiplierType}} y) => new(x.RawValue * y.RawValue);
    }

"""); if (op.MultiplierType != op.MultiplicantType) { sb.AppendLine($$"""
    partial struct {{op.ProductType}}
"""); if (!IsRefLikeType) { sb.AppendLine($$"""
    #if NET7_0_OR_GREATER
        : IDivisionOperators<{{op.ProductType}}, {{op.MultiplierType}}, {{op.MultiplicantType}}>
    #endif
"""); } sb.AppendLine($$"""
    {
        /// <inheritdoc />
        public static {{op.MultiplicantType}} operator /({{op.ProductType}} x, {{op.MultiplierType}} y) => new(x.RawValue / y.RawValue);
    }

    partial struct {{op.MultiplierType}}
"""); if (!IsRefLikeType) { sb.AppendLine($$"""
    #if NET7_0_OR_GREATER
        : IMultiplyOperators<{{op.MultiplierType}}, {{op.MultiplicantType}}, {{op.ProductType}}>
    #endif
"""); } sb.AppendLine($$"""
    {
        /// <inheritdoc />
        public static {{op.ProductType}} operator *({{op.MultiplierType}} x, {{op.MultiplicantType}} y) => new(x.RawValue * y.RawValue);
    }
"""); } }
        sb.AppendLine($$"""
    #endregion

    partial class UnitShorthands
    {
"""); foreach (var unit in UnitSymbols) { sb.AppendLine($$"""

"""); if (unit.ExportsShorthandSymbol) { sb.AppendLine($$"""
            /// <summary> A symbol for <see cref="{{TargetTypeName}}" />. </summary>
            [CLSCompliant(false)]
            public static readonly {{TargetTypeName}} {{unit.ShortName}} = new({{unit.Scale}});

"""); } }
        sb.AppendLine($$"""
    }
}


#if NET7_0_OR_GREATER
namespace QuantitiesDotNet.Generic
{
    public partial struct {{TargetTypeName}}<T>
"""); if (!IsRefLikeType) { sb.AppendLine($$"""
        : IQuantity<{{TargetTypeName}}<T>, T>
        , IMultiplyOperators<{{TargetTypeName}}<T>, T, {{TargetTypeName}}<T>>
        , IDivisionOperators<{{TargetTypeName}}<T>, {{TargetTypeName}}<T>, T>
        , IDivisionOperators<{{TargetTypeName}}<T>, T, {{TargetTypeName}}<T>>
"""); }
        sb.AppendLine($$"""
        where T : INumber<T>
    {
        public static QuantityMetadata Metadata => {{TargetTypeName}}.Metadata;
        public QuantityMetadata MetadataInstance => {{TargetTypeName}}.Metadata;

        private readonly T _RawValue;

        /// <summary>
        /// The raw value of <see href="{{TargetTypeName}}{T}" />.
        /// </summary>
        public T RawValue => _RawValue;

        internal {{TargetTypeName}}(T rawValue)
            => _RawValue = rawValue;

"""); GenerateBasicTypeShape(sb, isGeneric: true); sb.AppendLine($$"""

"""); GenerateUnitDefinitionsShape(sb, isGeneric: true); sb.AppendLine($$"""

"""); GenerateSelfOperatorsShape(sb, isGeneric: true); sb.AppendLine($$"""
    }


    #region unit operations

    /// NOTE: In specific case recursive generic interface causes JIT compile time increasing exponentially.
    /// Therefore interface implementations are comment-outed until .Net runtime is improved.

"""); foreach (var op in UnitOperations) { sb.AppendLine($$"""

    partial struct {{op.ProductType}}<T>
//        : IDivisionOperators<{{op.ProductType}}<T>, {{op.MultiplicantType}}<T>, {{op.MultiplierType}}<T>>
    {
        /// <inheritdoc />
        public static {{op.MultiplierType}}<T> operator /({{op.ProductType}}<T> x, {{op.MultiplicantType}}<T> y) => new(x.RawValue / y.RawValue);
    }

    partial struct {{op.MultiplicantType}}<T>
//        : IMultiplyOperators<{{op.MultiplicantType}}<T>, {{op.MultiplierType}}<T>, {{op.ProductType}}<T>>
    {
        /// <inheritdoc />
        public static {{op.ProductType}}<T> operator *({{op.MultiplicantType}}<T> x, {{op.MultiplierType}}<T> y) => new(x.RawValue * y.RawValue);
    }

"""); if (op.MultiplierType != op.MultiplicantType) { sb.AppendLine($$"""
    partial struct {{op.ProductType}}<T>
//        : IDivisionOperators<{{op.ProductType}}<T>, {{op.MultiplierType}}<T>, {{op.MultiplicantType}}<T>>
    {
        /// <inheritdoc />
        public static {{op.MultiplicantType}}<T> operator /({{op.ProductType}}<T> x, {{op.MultiplierType}}<T> y) => new(x.RawValue / y.RawValue);
    }

    partial struct {{op.MultiplierType}}<T>
//        : IMultiplyOperators<{{op.MultiplierType}}<T>, {{op.MultiplicantType}}<T>, {{op.ProductType}}<T>>
    {
        /// <inheritdoc />
        public static {{op.ProductType}}<T> operator *({{op.MultiplierType}}<T> x, {{op.MultiplicantType}}<T> y) => new(x.RawValue * y.RawValue);
    }
"""); } }
        sb.AppendLine($$"""
    #endregion

    partial class UnitShorthands
    {
"""); foreach (var unit in UnitSymbols) { sb.AppendLine($$"""

"""); if (unit.ExportsShorthandSymbol) { sb.AppendLine($$"""
            /// <summary> A symbol for <see cref="{{TargetTypeName}}" />. </summary>
            [CLSCompliant(false)]
            public static readonly {{TargetTypeName}} {{unit.ShortName}} = new({{unit.Scale}});

"""); } }
        sb.AppendLine($$"""
    }
}
#endif
""");
    }


    private void GenerateBasicTypeShape(StringBuilderWrapper sb, bool isGeneric)
    {
        var targetTypeName = TargetTypeName + (isGeneric ? "<T>" : "");
        var entityTypeName = isGeneric ? "T" : "double";
        if (IsRefLikeType)
        {
            return;
        }
        sb.AppendLine($$"""
        #region basic type implements

        /// <inheritdoc />
        public int CompareTo(object? obj)
            => obj is {{targetTypeName}} other
            ? Compare(this, other)
            : throw new ArgumentException(nameof(obj));

        /// <inheritdoc />
        public int CompareTo({{targetTypeName}} other) => Compare(this, other);

        /// <inheritdoc />
        public bool Equals({{targetTypeName}} other) => Equals(this, other);

        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="provider"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryParse(string? s, IFormatProvider? provider, out {{targetTypeName}} result)
        {
            if(!QuantityParseInfo.TryCompile(s, out var info))
            {
                result = default;
                return false;
            }
            var (succeeded, value) = info.UnitSelector switch {
"""); foreach (var unit in UnitSymbols) { sb.AppendLine($$"""
                "{{unit.ShortName}}" => ({{entityTypeName}}.TryParse(info.Number, NumberStyles.Any, provider, out var x), From{{unit.MajorName}}(x!)),
"""); }
        sb.AppendLine($$"""
                _ => (false, default({{targetTypeName}})),
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
        public static {{targetTypeName}} Parse(string? s, IFormatProvider? provider)
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
"""); foreach (var unit in UnitSymbols) { sb.AppendLine($$"""
                "{{unit.ShortName}}" => ({{unit.MajorName}}, "{{unit.ShortName}}"),
"""); }
        sb.AppendLine($$"""
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

        #if NET6_0_OR_GREATER
        /// <inheritdoc />
        #else
        /// <summary>
        /// Tries to format the value of the current instance into the provided span of characters.
        /// </summary>
        /// <param name="destination">The span in which to write this instance's value formatted as a span of characters.</param>
        /// <param name="charsWritten">When this method returns, contains the number of characters that were written in <paramref name="destination"/>.</param>
        /// <param name="format">A span containing the characters that represent a standard or custom format string that defines the acceptable format for <paramref name="destination"/>.</param>
        /// <param name="formatProvider">An optional object that supplies culture-specific formatting information for <paramref name="destination"/>.</param>
        /// <returns><c>true</c> if the formatting was successful; otherwise, <c>false</c>.</returns>
        #endif
        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? formatProvider)
        {
            var (info, number, unit) = GetFormatInfo(format.ToString(), formatProvider);
            return info.TryFormat(destination, out charsWritten, number, unit);
        }

        /// <inheritdoc />
        public override int GetHashCode()
            => _RawValue.GetHashCode();

        /// <inheritdoc />
        public override bool Equals([NotNullWhen(true)] object? obj)
            => obj is {{targetTypeName}} other && Equals(this, other);

        #endregion  // basic type implements
""");
    }


    private void GenerateUnitDefinitionsShape(StringBuilderWrapper sb, bool isGeneric)
    {
        var targetTypeName = TargetTypeName + (isGeneric ? "<T>" : "");
        var entityTypeName = isGeneric ? "T" : "double";
        var unitInfoTypeName = isGeneric ? "UnitMetadata<T>" : "UnitMetadata<double>";
        var getUnitScaleFormat = isGeneric ? "T.CreateSaturating({0})" : "{0}";
        sb.AppendLine($$"""
        #region unit definition implements

        /// <summary> The unit informations dictionary which is keyed by unit symbols. </summary>
        public static readonly ImmutableDictionary<string, {{unitInfoTypeName}}> UnitsBySymbol = GetUnitsBySymbol();
        private static ImmutableDictionary<string, {{unitInfoTypeName}}> GetUnitsBySymbol()
        {
            var builder = ImmutableDictionary.CreateBuilder<string, {{unitInfoTypeName}}>();
"""); foreach (var unit in UnitSymbols) { sb.AppendLine($$"""
            builder.Add("{{unit.ShortName}}", {{unit.MajorName}}Info);
"""); }
        sb.AppendLine($$"""
            return builder.ToImmutable();
        }

"""); foreach (var unit in UnitSymbols) { sb.AppendLine($$"""
        #region {{unit.MajorName}}

        private static readonly {{entityTypeName}} _{{unit.MajorName}}Scale = {{string.Format(getUnitScaleFormat, unit.Scale)}};

        /// <summary> The information for [{{unit.ShortName}}]. </summary>
        public static readonly {{unitInfoTypeName}} {{unit.MajorName}}Info = new (_{{unit.MajorName}}Scale, "{{unit.MajorName}}", "{{unit.ShortName}}");

        /// <summary>
        /// Creates a new <see href="{{targetTypeName.Replace('<', '{').Replace('>', '}')}}" /> instance by interpreting the given real value in the scale of [{{unit.ShortName}}].
        /// </summary>
        /// <param name="Second"></param>
        /// <returns></returns>
        public static {{targetTypeName}} From{{unit.MajorName}}({{entityTypeName}} {{unit.MajorName}})
            => new ({{unit.MajorName}} * _{{unit.MajorName}}Scale);

        /// <summary>
        /// Gets the value of this instance in [{{unit.ShortName}}] scale.
        /// </summary>
        public {{entityTypeName}} {{unit.MajorName}} => _RawValue / _{{unit.MajorName}}Scale;

        #endregion

"""); }
        sb.AppendLine($$"""
        #endregion  // unit definition implements
""");
    }


    private void GenerateSelfOperatorsShape(StringBuilderWrapper sb, bool isGeneric)
    {
        var targetTypeName = TargetTypeName + (isGeneric ? "<T>" : "");
        var entityTypeName = isGeneric ? "T" : "double";
        var one = isGeneric ? "T.One" : "1.0";
        sb.AppendLine($$"""
        #region operator implements

"""); if (!IsRefLikeType) { sb.AppendLine($$"""

        /// <summary>
        /// Determines whether the 2 values are same or not.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool Equals(in {{targetTypeName}} x, in {{targetTypeName}} y) => x._RawValue == y._RawValue;

        /// <summary>
        /// Determines which value is greater than another.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int Compare(in {{targetTypeName}} x, in {{targetTypeName}} y)
        {
            if (x._RawValue == y._RawValue) { return 0; }
            return x._RawValue < y._RawValue ? -1 : 1;
        }

        /// <inheritdoc />
        public static bool operator ==({{targetTypeName}} x, {{targetTypeName}} y) => Equals(x, y);

        /// <inheritdoc />
        public static bool operator !=({{targetTypeName}} x, {{targetTypeName}} y) => !Equals(x, y);

        /// <inheritdoc />
        public static bool operator <({{targetTypeName}} x, {{targetTypeName}} y) => Compare(x, y) < 0;

        /// <inheritdoc />
        public static bool operator >({{targetTypeName}} x, {{targetTypeName}} y) => Compare(x, y) > 0;

        /// <inheritdoc />
        public static bool operator <=({{targetTypeName}} x, {{targetTypeName}} y) => Compare(x, y) <= 0;

        /// <inheritdoc />
        public static bool operator >=({{targetTypeName}} x, {{targetTypeName}} y) => Compare(x, y) >= 0;

"""); }
        sb.AppendLine($$"""

        /// <inheritdoc />
        public static {{targetTypeName}} operator +({{targetTypeName}} x, {{targetTypeName}} y) => new (x._RawValue + y._RawValue);

        /// <inheritdoc />
        public static {{targetTypeName}} operator-({{targetTypeName}} x, {{targetTypeName}} y) => new (x._RawValue - y._RawValue);

        /// <inheritdoc />
        public static {{targetTypeName}} operator *({{entityTypeName}} x, {{targetTypeName}} y) => new (x * y._RawValue);

        /// <inheritdoc />
        public static {{entityTypeName}} operator /({{targetTypeName}} x, {{targetTypeName}} y) => x._RawValue / y._RawValue;

        /// <inheritdoc />
        public static {{targetTypeName}} operator *({{targetTypeName}} x, {{entityTypeName}} y) => new (x._RawValue * y);

        /// <inheritdoc />
        public static {{targetTypeName}} operator /({{targetTypeName}} x, {{entityTypeName}} y) => new(x._RawValue / y);

        /// <inheritdoc />
        public static {{targetTypeName}} operator %({{targetTypeName}} x, {{targetTypeName}} y) => new(x._RawValue % y._RawValue);

        /// <inheritdoc />
        public static {{targetTypeName}} AdditiveIdentity => default;

        /// <inheritdoc />
        public static {{entityTypeName}} MultiplicativeIdentity => {{one}};

        /// <inheritdoc />
        public static {{targetTypeName}} operator +({{targetTypeName}} value) => value;

        /// <inheritdoc />
        public static {{targetTypeName}} operator -({{targetTypeName}} value) => new(-value._RawValue);

        #endregion  // operator implements
""");
    }


    private class StringBuilderWrapper(StringBuilder sb, CancellationToken token)
    {
        public void AppendLine(string text)
        {
            token.ThrowIfCancellationRequested();
            sb.AppendLine(text);
        }
    }
}