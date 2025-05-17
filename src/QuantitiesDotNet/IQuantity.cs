namespace QuantitiesDotNet;

public interface IQuantity
    : IFormattable
{
#if NET7_0_OR_GREATER
    public static abstract QuantityMetadata Metadata { get; }
#endif
    public QuantityMetadata MetadataInstance { get; }

}

public interface IQuantity<TSelf, T>
    : IQuantity
    , IComparable<TSelf>
    , IEquatable<TSelf>
#if NET7_0_OR_GREATER
    , ISpanFormattable
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
}
