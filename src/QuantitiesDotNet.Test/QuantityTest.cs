using System.Numerics;

namespace QuantitiesDotNet;

public partial class QuantityTest
{
    public static partial TheoryData<object, object> ScaleTestCases();

    [Theory]
    [MemberData(nameof(ScaleTestCases))]
    public void Scale<T>(T viaQuantity, T expected)
        where T : INumber<T>
        => Assert.Equal(expected, viaQuantity);
}
