using Xunit.Sdk;

namespace QuantitiesDotNet;

public class QuantityFormatInfoTest
{
    public static TheoryData<string, QuantityFormatInfo?> TryCompileTestCases()
        => new()
        {
            // success
            { "", new("", "", "", false) },
            { "0", new("0", "", "", false) },
            { "E", new("E", "", "", false) },
            { "&", new("", "", "", false) },
            { "0&", new("0", "", "", false) },
            { "E&", new("E", "", "", false) },
            { "&m/s", new("", "", "m/s", false) },
            { "&[m/s]", new("", "", "m/s", true) },
            { "E&m/s", new("E", "", "m/s", false) },
            { "E&[m/s]", new("E", "", "m/s", true) },
            { "E3&m/s", new("E3", "", "m/s", false) },
            { "E3&[m/s]", new("E3", "", "m/s", true) },
            { "0.00&m/s", new("0.00", "", "m/s", false) },
            { "0.00&[m/s]", new("0.00", "", "m/s", true) },
            { "0.00& m/s", new("0.00", " ", "m/s", false) },
            { "0.00& [m/s]", new("0.00", " ", "m/s", true) },
            { "0.00\\&a& [m/s]", new("0.00&a", " ", "m/s", true) },
            { "0.00\\&a& [m/s\\[\\]]", new("0.00&a", " ", "m/s[]", true) },

            // failure
            { "&[m/s", null },
            { "&m/s]", null },
            { "0.00&[m/s", null },
            { "0.00&m/s]", null },
            { "0.00&[m/s]]", null },
            { "0.00&[[m/s]", null },
            { "0.00& [", null },
            { "0.00& ]", null },
            { "0.00&[", null },
            { "0.00&]", null },
            { "0.00&[ ]", null }
        };

    [Theory]
    [MemberData(nameof(TryCompileTestCases))]
    public void TryCompile(string format, QuantityFormatInfo? expectedResult)
    {
        var actualSucceeded = QuantityFormatInfo.TryCompile(format, out var actualResult);
        if (expectedResult is { })
        {
            Assert.True(actualSucceeded);
            Assert.Equal(expectedResult, actualResult);
        }
        else
        {
            Assert.False(actualSucceeded);
        }
    }


    public static TheoryData<QuantityFormatInfo, int, string, string, bool, int, string> TryFormatTestCases()
        => new()
        {
            // success
            { new("", "", "", false), 256, "123", "m/s", true, 6, "123m/s" },
            { new("", " ", "", false), 256, "123", "m/s", true, 7, "123 m/s" },
            { new("", "", "", true), 256, "123", "m/s", true, 8, "123[m/s]" },
            { new("", " ", "", true), 256, "123", "m/s", true, 9, "123 [m/s]" },

            // failure
            { new("", "", "", false), 1, "123", "m/s", false, default, "" },
            { new("", " ", "", false), 1, "123", "m/s", false, default, "" },
            { new("", "", "", true), 1, "123", "m/s", false, default, "" },
            { new("", " ", "", true), 1, "123", "m/s", false, default, "" },
        };


    [Theory]
    [MemberData(nameof(TryFormatTestCases))]
    public void TryFormat(QuantityFormatInfo info, int destSize, string number, string unit, bool expectedSucceeded, int expectedCharsWritten, string expectedDestination)
    {
        var dest = (stackalloc char[destSize]);
        var actualSucceeded = info.TryFormat(number, unit, dest, out var actualCharsWritten);
        Assert.Equal(expectedSucceeded, actualSucceeded);
        if (expectedSucceeded)
        {
            Assert.Equal(expectedCharsWritten, actualCharsWritten);
            Assert.Equal(expectedDestination, dest.Slice(0, expectedCharsWritten));
        }
    }
}