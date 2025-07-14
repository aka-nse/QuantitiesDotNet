namespace QuantitiesDotNet;

public class AttributeTest
{
    [Fact]
    public void QuantityAttribute_ctor()
    {
        var l = 1;
        var m = 2;
        var t = 3;
        var i = 4;
        var th = 5;
        var n = 6;
        var j = 7;
        var attribute = new QuantityAttribute(l, m, t, i, th, n, j);
        Assert.Equal(l, attribute.L);
        Assert.Equal(m, attribute.M);
        Assert.Equal(t, attribute.T);
        Assert.Equal(i, attribute.I);
        Assert.Equal(th, attribute.Th);
        Assert.Equal(n, attribute.N);
        Assert.Equal(j, attribute.J);
    }

    [Fact]
    public void QuantityOperationAttribute_ctor()
    {
        var attribute = new QuantityOperationAttribute(typeof(QSpeed), typeof(QTime), typeof(QLength));
        Assert.Equal(typeof(QSpeed), attribute.MultiplicantType);
        Assert.Equal(typeof(QTime), attribute.MultiplierType);
        Assert.Equal(typeof(QLength), attribute.ProductType);
    }

    [Fact]
    public void QuantityUnitAttribute_ctor()
    {
        var name = "meter";
        var unit = "m";
        var scale = 1.0;
        var prefix = UnitPrefix.None;
        var powerOfPrefix = 1;
        var exportsShorthandSymbol = true;
        var attribute = new QuantityUnitAttribute(name, unit, scale, prefix, powerOfPrefix, exportsShorthandSymbol);
        Assert.Equal(name, attribute.Name);
        Assert.Equal(unit, attribute.Unit);
        Assert.Equal(scale, attribute.Scale);
        Assert.Equal(prefix, attribute.Prefix);
        Assert.Equal(powerOfPrefix, attribute.PowerOfPrefix);
        Assert.Equal(exportsShorthandSymbol, attribute.ExportsShorthandSymbol);
    }
}
