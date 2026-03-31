namespace QuantitiesDotNet;

public interface IQuantity
    : IFormattable
#if NET7_0_OR_GREATER
    , ISpanFormattable
    , IUtf8SpanFormattable
#endif
{
#if NET7_0_OR_GREATER
    public static virtual QuantityMetadata Metadata
        => throw new NotSupportedException("This type does not support Metadata property. Use MetadataInstance instead.");
#endif
    public QuantityMetadata MetadataInstance { get; }

}

public interface IQuantity<TSelf, T>
    : IQuantity
    , IComparable<TSelf>
    , IEquatable<TSelf>
#if NET7_0_OR_GREATER
    , IParsable<TSelf>
    , ISpanParsable<TSelf>
    , IUtf8SpanParsable<TSelf>
    , IComparisonOperators<TSelf, TSelf, bool>
    , IAdditionOperators<TSelf, TSelf, TSelf>
    , ISubtractionOperators<TSelf, TSelf, TSelf>
    , IModulusOperators<TSelf, TSelf, TSelf>
    , IAdditiveIdentity<TSelf, TSelf>
    , IMultiplicativeIdentity<TSelf, T>
    , IUnaryPlusOperators<TSelf, TSelf>
    , IUnaryNegationOperators<TSelf, TSelf>
    where T : notnull, INumber<T>
#else
    where T : notnull
#endif
    where TSelf : IQuantity<TSelf, T>
{
    public T RawValue { get; }

#if NET7_0_OR_GREATER

    /// <summary> Determines whether the 2 values are same or not. </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    /// <remarks>
    /// The implementation must be <c>x.RawValue == y.RawValue</c>.
    /// The behaviour for edge cases conforms to the implementation of <typeparamref name="T"/>.
    /// </remarks>
    public static abstract bool Equals(TSelf x, TSelf y);

    /// <summary> Determines which value is greater than another. </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    /// <remarks>
    /// The implementation must be <c>x.RawValue == y.RawValue ? 0 : (x.RawValue &lt; y.RawValue ? -1 : 1)</c>.
    /// The behaviour for edge cases conforms to the implementation of <typeparamref name="T"/>.
    /// </remarks>
    public static abstract int Compare(TSelf x, TSelf y);

    /// <inheritdoc cref="object.GetHashCode"/>
    /// <remarks>
    /// The implementation must be:
    /// <code>
    /// (x.RawValue &lt; y.RawValue, x.RawValue &gt; y.RawValue) switch
    /// {
    ///     (true, false) => -1,
    ///     (false, true) => +1,
    ///     _ => 0,
    /// }
    /// </code>
    /// The behaviour for edge cases conforms to the implementation of <typeparamref name="T"/>.
    /// </remarks>
    public int GetHashCode();

#endif
}
