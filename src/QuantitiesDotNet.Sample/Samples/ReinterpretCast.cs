using System.Runtime.CompilerServices;

namespace QuantitiesDotNet.Samples;

internal class ReinterpretCast : IUsageSample
{
    public string SampleName => "reinterpret casting";

    public void Execute(TextWriter stdout)
    {
        stdout.WriteLine(Unsafe.BitCast<double, QSpeed>(1.234));  // 1.234m/s

#if NET7_0_OR_GREATER
        stdout.WriteLine(Unsafe.BitCast<decimal, Generic.QSpeed<decimal>>(1.234m));  // 1.234m/s
#endif
    }
}