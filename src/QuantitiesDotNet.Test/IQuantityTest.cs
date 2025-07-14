namespace QuantitiesDotNet;

public class IQuantityTest
{
    [Fact]
    public void MetadataThrowsNotSupported()
    {
        Assert.Throws<NotSupportedException>(static () =>
        {
            static void getMetadata<T>() where T : IQuantity
            {
                _ = T.Metadata;
            }

            getMetadata<IQuantity>();
        });
    }
}
