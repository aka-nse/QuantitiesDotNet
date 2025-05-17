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


public record DimensionMetadata
{
    public int L { get; }
    public int M { get; }
    public int T { get; }
    public int I { get; }
    public int Th { get; }
    public int N { get; }
    public int J { get; }

    internal DimensionMetadata(int L, int M, int T, int I, int Th, int N, int J)
    {
        this.L = L;
        this.M = M;
        this.T = T;
        this.I = I;
        this.Th = Th;
        this.N = N;
        this.J = J;
    }

    public override string ToString()
    {
        static void setup(StringBuilder sb, string symbol, int value, ref string space)
        {
            if (value == 0)
            {
                return;
            }

            if (value == 1)
            {
                sb.Append($"{space}{symbol}");
            }
            else
            {
                sb.Append($"{space}{symbol}^{value}");
            }
            space = " ";
        }

        var space = "";
        var sb = new StringBuilder();
        setup(sb, nameof(L), L, ref space);
        setup(sb, nameof(M), M, ref space);
        setup(sb, nameof(T), T, ref space);
        setup(sb, nameof(I), I, ref space);
        setup(sb, nameof(Th), Th, ref space);
        setup(sb, nameof(N), N, ref space);
        setup(sb, nameof(J), J, ref space);
        return sb.Length > 0 ? sb.ToString() : "1";
    }
}