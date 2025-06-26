using System.Runtime.CompilerServices;

namespace QuantitiesDotNet;

public partial class QuantityTest
{
    public static partial TheoryData<object> TypeArgumentProvider_NonGeneric();
    public static partial TheoryData<object> TypeArgumentProvider_Generic();

    #region Scale

    public static partial TheoryData<object, object> ScaleTestCases_NonGeneric();

    [Theory]
    [MemberData(nameof(ScaleTestCases_NonGeneric))]
    public void Scale_NonGeneric<T>(T expected, T viaQuantity)
        where T : IQuantity<T, double>
        => Assert.Equal(expected.RawValue, viaQuantity.RawValue, 1e-10);


    public static partial TheoryData<object, object> ScaleTestCases_Generic();

    [Theory]
    [MemberData(nameof(ScaleTestCases_Generic))]
    public void Scale_Generic<T>(T expected, T viaQuantity)
        where T : IQuantity<T, decimal>
        => Assert.Equal((double)expected.RawValue, (double)viaQuantity.RawValue, 1e-10);

    #endregion Scale

    #region Metadata
    
    public static partial TheoryData<IQuantity, (int L, int M, int T, int I, int Th, int N, int J)> MetadataTestCases();

    [Theory]
    [MemberData(nameof(MetadataTestCases))]
    public void Metadata<TQuantity>(TQuantity value, (int L, int M, int T, int I, int Th, int N, int J) dimensions)
        where TQuantity : IQuantity
    {
        var metadata = value.MetadataInstance;
        Assert.NotNull(metadata);
        Assert.Equal(TQuantity.Metadata, metadata);
        Assert.Equal(dimensions.L, metadata.Dimension.L);
        Assert.Equal(dimensions.M, metadata.Dimension.M);
        Assert.Equal(dimensions.T, metadata.Dimension.T);
        Assert.Equal(dimensions.I, metadata.Dimension.I);
        Assert.Equal(dimensions.Th, metadata.Dimension.Th);
        Assert.Equal(dimensions.N, metadata.Dimension.N);
        Assert.Equal(dimensions.J, metadata.Dimension.J);
    }

    #endregion Metadata

    #region Compare
#pragma warning disable CS1718

    [Theory]
    [MemberData(nameof(TypeArgumentProvider_NonGeneric))]
    public void Compare_NonGeneric<T>(T placeHolder)
        where T : struct, IQuantity<T, double>
    {
        InternalHelpers.NoUse(placeHolder);
        var _0 = Unsafe.BitCast<double, T>(0.0);
        var _1 = Unsafe.BitCast<double, T>(1.0);

        Assert.Equal(_1, _1);
        Assert.True (_1.Equals(_1));
        Assert.True (_1.Equals((object)_1));
        Assert.True (T.Equals(_1, _1));
        Assert.Equal(0, T.Compare(_1, _1));
        Assert.Equal(0, _1.CompareTo(_1));
        Assert.True (_1 == _1);
        Assert.False(_1 != _1);
        Assert.True (_1 >= _1);
        Assert.False(_1 >  _1);
        Assert.True (_1 <= _1);
        Assert.False(_1 <  _1);

        Assert.NotEqual(_0, _1);
        Assert.False(_0.Equals(_1));
        Assert.False(_0.Equals((object)_1));
        Assert.False(T.Equals(_0, _1));
        Assert.True(0 > T.Compare(_0, _1));
        Assert.True(0 > _0.CompareTo(_1));
        Assert.False(_0 == _1);
        Assert.True (_0 != _1);
        Assert.False(_0 >= _1);
        Assert.False(_0 >  _1);
        Assert.True (_0 <= _1);
        Assert.True (_0 <  _1);

        Assert.NotEqual(_1, _0);
        Assert.False(_1.Equals(_0));
        Assert.False(_1.Equals((object)_0));
        Assert.False(T.Equals(_1, _0));
        Assert.True(0 < T.Compare(_1, _0));
        Assert.True(0 < _1.CompareTo(_0));
        Assert.False(_1 == _0);
        Assert.True (_1 != _0);
        Assert.True (_1 >= _0);
        Assert.True (_1 >  _0);
        Assert.False(_1 <= _0);
        Assert.False(_1 <  _0);

        Assert.False(_0.Equals(new object()));
    }

    [Theory]
    [MemberData(nameof(TypeArgumentProvider_Generic))]
    public void Compare_Generic<T>(T placeHolder)
        where T : struct, IQuantity<T, decimal>
    {
        InternalHelpers.NoUse(placeHolder);
        var _0 = Unsafe.BitCast<decimal, T>(0.0m);
        var _1 = Unsafe.BitCast<decimal, T>(1.0m);

        Assert.Equal(_1, _1);
        Assert.True (_1.Equals(_1));
        Assert.True (_1.Equals((object)_1));
        Assert.True (T.Equals(_1, _1));
        Assert.Equal(0, T.Compare(_1, _1));
        Assert.Equal(0, _1.CompareTo(_1));
        Assert.True (_1 == _1);
        Assert.False(_1 != _1);
        Assert.True (_1 >= _1);
        Assert.False(_1 >  _1);
        Assert.True (_1 <= _1);
        Assert.False(_1 <  _1);

        Assert.NotEqual(_0, _1);
        Assert.False(_0.Equals(_1));
        Assert.False(_0.Equals((object)_1));
        Assert.False(T.Equals(_0, _1));
        Assert.True(0 > T.Compare(_0, _1));
        Assert.True(0 > _0.CompareTo(_1));
        Assert.False(_0 == _1);
        Assert.True (_0 != _1);
        Assert.False(_0 >= _1);
        Assert.False(_0 >  _1);
        Assert.True (_0 <= _1);
        Assert.True (_0 <  _1);

        Assert.NotEqual(_1, _0);
        Assert.False(_1.Equals(_0));
        Assert.False(_1.Equals((object)_0));
        Assert.False(T.Equals(_1, _0));
        Assert.True(0 < T.Compare(_1, _0));
        Assert.True(0 < _1.CompareTo(_0));
        Assert.False(_1 == _0);
        Assert.True (_1 != _0);
        Assert.True (_1 >= _0);
        Assert.True (_1 >  _0);
        Assert.False(_1 <= _0);
        Assert.False(_1 <  _0);

        Assert.False(_0.Equals(new object()));
    }

#pragma warning restore CS1718
    #endregion
}
