namespace QuantitiesDotNet;
using Generic;

partial class QuantityTest
{
    public static partial TheoryData<object, object> ScaleTestCases()
    {
        var data = new TheoryData<object, object>();
        data.Add(new QAcceleration(1.0).MetrePerSquareSecond, 1.0);
        data.Add(new QAcceleration<decimal>(1.0m).MetrePerSquareSecond, 1.0m);
        return data;
    }
}
