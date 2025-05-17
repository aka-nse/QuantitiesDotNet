using System.Text;

namespace QuantitiesDotNet;

public partial record QuantityMetadata
{

    public string Name { get; }
    public DimensionMetadata Dimension { get; }

    internal QuantityMetadata(string Name, int L, int M, int T, int I, int Th, int N, int J)
    {
        this.Name = Name;
        Dimension = new(L, M, T, I, Th, N, J);
    }
}