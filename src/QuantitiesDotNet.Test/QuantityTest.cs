namespace QuantitiesDotNet;

public partial class QuantityTest
{
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
        Assert.Equal(dimensions.L, metadata.Dimension.L);
        Assert.Equal(dimensions.M, metadata.Dimension.M);
        Assert.Equal(dimensions.T, metadata.Dimension.T);
        Assert.Equal(dimensions.I, metadata.Dimension.I);
        Assert.Equal(dimensions.Th, metadata.Dimension.Th);
        Assert.Equal(dimensions.N, metadata.Dimension.N);
        Assert.Equal(dimensions.J, metadata.Dimension.J);
    }

    #endregion Metadata
}
