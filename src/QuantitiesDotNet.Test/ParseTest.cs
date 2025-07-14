using QuantitiesDotNet.Generic;

namespace QuantitiesDotNet;

public class ParseTest
{
#pragma warning disable format
    public static TheoryData<string?, bool, XunitSerializable<QuantityParseInfo?>> TryCompileTestCases()
        => new()
        {
            { null, false, new(null) },
            { "", false, new(null) },
            { " ", false, new(null) },
            { "1.23m/s", true, new(new QuantityParseInfo("1.23", "m/s")) },
            { "1.23 m/s", true, new(new QuantityParseInfo("1.23", "m/s")) },
            { "1.23[m/s]", true, new(new QuantityParseInfo("1.23", "m/s")) },
            { "1.23 [m/s]", true, new(new QuantityParseInfo("1.23", "m/s")) },
            { "1.23 [m/s", false, new(null) },
            { "1.23m^2/s^2", true, new(new QuantityParseInfo("1.23", "m^2/s^2")) },
            { "-1.23kg*m/s^2", true, new(new QuantityParseInfo("-1.23", "kg*m/s^2")) },
            { "+1.23kg*m/s^2", true, new(new QuantityParseInfo("+1.23", "kg*m/s^2")) },
            { "1.23E+3m/s", true, new(new QuantityParseInfo("1.23E+3", "m/s")) },
            { "1.23E-3m/s", true, new(new QuantityParseInfo("1.23E-3", "m/s")) },
            { "1,23m/s", true, new(new QuantityParseInfo("1,23", "m/s")) },
            { "123", false, new(null) },
            { "m/s", false, new(null) },
            { "1.23", false, new(null) },
            { "1.23 [m s^-1]", true, new(new QuantityParseInfo("1.23", "m s^-1")) },
            { "1.23 [m s^-1", false, new(null) },
            { "1.23 [m/s] extra", false, new(null) },
        };


#pragma warning restore format

    [Theory]
    [MemberData(nameof(TryCompileTestCases))]
    public void TryCompile(string? expression, bool expected, XunitSerializable<QuantityParseInfo?> expectedInfo)
    {
        if (expected)
        {
            Assert.True(QuantityParseInfo.TryCompile(expression, out var info));
            Assert.Equal(expectedInfo.Value, info);
        }
        else
        {
            Assert.False(QuantityParseInfo.TryCompile(expression, out _));
        }
    }


#pragma warning disable format
    public static TheoryData<string, QSpeed?> ParseTestCase()
        => new()
        {
            { "1.234m/s"           , QSpeed.FromMetrePerSecond(1.234) },
            { "1.234m/s"           , QSpeed.FromMetrePerSecond(1.234) },
            { "1.234 m/s"          , QSpeed.FromMetrePerSecond(1.234) },
            { "1.234[m/s]"         , QSpeed.FromMetrePerSecond(1.234) },
            { "1.234 [m/s]"        , QSpeed.FromMetrePerSecond(1.234) },
            { "1.234000E+000m/s"   , QSpeed.FromMetrePerSecond(1.234) },
            { "1.234000E+000m/s"   , QSpeed.FromMetrePerSecond(1.234) },
            { "1.234000E+000 m/s"  , QSpeed.FromMetrePerSecond(1.234) },
            { "1.234000E+000[m/s]" , QSpeed.FromMetrePerSecond(1.234) },
            { "1.234000E+000 [m/s]", QSpeed.FromMetrePerSecond(1.234) },
            { "1.234000E+000 [m/s", null },
        };

    public static TheoryData<string, QSpeed<decimal>?> ParseGenericTestCase()
        => new()
        {
            { "1.234m/s"           , QSpeed<decimal>.FromMetrePerSecond(1.234m) },
            { "1.234m/s"           , QSpeed<decimal>.FromMetrePerSecond(1.234m) },
            { "1.234 m/s"          , QSpeed<decimal>.FromMetrePerSecond(1.234m) },
            { "1.234[m/s]"         , QSpeed<decimal>.FromMetrePerSecond(1.234m) },
            { "1.234 [m/s]"        , QSpeed<decimal>.FromMetrePerSecond(1.234m) },
            { "1.234 [m/s" , null },
        };
#pragma warning restore format

    [Theory]
    [MemberData(nameof(ParseTestCase))]
    public void Parse(string expression, QSpeed? expected)
    {
        if (expected is QSpeed expected_)
        {
            Assert.True(QSpeed.TryParse(expression, null, out var actual));
            Assert.Equal(expected_, actual);
            Assert.Equal(expected_, QSpeed.Parse(expression, null));
        }
        else
        {
            Assert.False(QSpeed.TryParse(expression, null, out _));
            Assert.Throws<FormatException>(() => QSpeed.Parse(expression, null));
        }
    }

    [Theory]
    [MemberData(nameof(ParseGenericTestCase))]
    public void ParseGeneric(string expression, QSpeed<decimal>? expected)
    {
        if (expected is QSpeed<decimal> expected_)
        {
            Assert.True(QSpeed<decimal>.TryParse(expression, null, out var actual));
            Assert.Equal(expected_, actual);
            Assert.Equal(expected_, QSpeed<decimal>.Parse(expression, null));
        }
        else
        {
            Assert.False(QSpeed<decimal>.TryParse(expression, null, out _));
            Assert.Throws<FormatException>(() => QSpeed<decimal>.Parse(expression, null));
        }
    }
}