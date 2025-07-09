namespace QuantitiesDotNet;

public class UnitMetadataTest
{
    [Fact]
    public void UnitMetadata_ctor()
    {
        var unit = new UnitMetadata<double>(60, "Minutes", "min");

        Assert.Equal(60, unit.Scale);
        Assert.Equal("Minutes", unit.MajorName);
        Assert.Equal("min", unit.UnitSymbol);
    }
}
