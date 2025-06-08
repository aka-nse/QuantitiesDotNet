namespace QuantitiesDotNet;

public partial class QuantityTest
{
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
}
