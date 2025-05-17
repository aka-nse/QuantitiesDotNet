using System.Reflection;

namespace QuantitiesDotNet.Samples;

internal class QuantityInfos : IUsageSample
{
    public string SampleName => "shows quantity infos";

    public void Execute(TextWriter stdout)
    {
        const BindingFlags flags =
            BindingFlags.NonPublic | BindingFlags.Static;

        var types = typeof(QuantityMetadata).Assembly
            .GetTypes()
            .Where(t => t.IsValueType
                && !t.IsGenericType
                && t.GetCustomAttributes().Any(x => x.GetType().Name == "QuantityAttribute"));
        foreach (var type in types)
        {
            // for reflection of ref struct, explicitly named backing field is provided.
            var info = type.GetField(nameof(QDimensionless._Metadata), flags)!.GetValue(null) as QuantityMetadata;

            stdout.WriteLine($"{info?.Name,-24} (Dimension={info?.Dimension})");
        }
    }

}