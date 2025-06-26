namespace QuantitiesDotNet;

public class DimensionMetadataTest
{
    public static TheoryData<(int L, int M, int T, int I, int Th, int N, int J), string> ToStringTestCases()
        => new()
        {
            { (+0, +0, +0, +0, +0, +0, +0), "1" },

            { (+1, +0, +0, +0, +0, +0, +0), "L" },
            { (+0, +1, +0, +0, +0, +0, +0), "M" },
            { (+0, +0, +1, +0, +0, +0, +0), "T" },
            { (+0, +0, +0, +1, +0, +0, +0), "I" },
            { (+0, +0, +0, +0, +1, +0, +0), "Th" },
            { (+0, +0, +0, +0, +0, +1, +0), "N" },
            { (+0, +0, +0, +0, +0, +0, +1), "J" },

            { (+1, +2, -1, +0, +0, +0, +0), "L M^2 T^-1" },
            { (+0, +1, +2, -1, +0, +0, +0), "M T^2 I^-1" },
            { (+0, +0, +1, +2, -1, +0, +0), "T I^2 Th^-1" },
            { (+0, +0, +0, +1, +2, -1, +0), "I Th^2 N^-1" },
            { (+0, +0, +0, +0, +1, +2, -1), "Th N^2 J^-1" },
            { (-1, +0, +0, +0, +0, +1, +2), "L^-1 N J^2" },
            { (+2, -1, +0, +0, +0, +0, +1), "L^2 M^-1 J" },
        };

    [Theory]
    [MemberData(nameof(ToStringTestCases))]
    public void ToStringFormat((int L, int M, int T, int I, int Th, int N, int J) dim, string expected)
    {
        var dimension = new DimensionMetadata(dim.L, dim.M, dim.T, dim.I, dim.Th, dim.N, dim.J);
        Assert.Equal(expected, dimension.ToString());
    }
}
