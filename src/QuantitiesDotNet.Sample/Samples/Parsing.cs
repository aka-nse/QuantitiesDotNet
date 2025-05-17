using System.Globalization;

namespace QuantitiesDotNet.Samples;

internal class Parsing : IUsageSample
{
    public string SampleName => "string parsing";

    public void Execute(TextWriter stdout)
    {
        // all results: 1.234m/s
        Core(stdout, "1.234m/s", CultureInfo.InvariantCulture);
        Core(stdout, "4.4424km/h", CultureInfo.InvariantCulture, "0.000");
        Core(stdout, "1,234m/s", CultureInfo.GetCultureInfo("fr-FR"));
        Core(stdout, "1.234 m/s", CultureInfo.InvariantCulture);
        Core(stdout, "1.234[m/s]", CultureInfo.InvariantCulture);
        Core(stdout, "1.234 m/s", CultureInfo.InvariantCulture);
        Core(stdout, "1.234", CultureInfo.InvariantCulture);
    }

    private static void Core(TextWriter stdout, string expression, CultureInfo cultureInfo, string? format = null)
    {
        try
        {
            stdout.Write($"{expression} -> ");
            var formatted = format is { }
                ? QSpeed.Parse(expression, cultureInfo).ToString(format, cultureInfo)
                : QSpeed.Parse(expression, cultureInfo).ToString();
            stdout.Write(formatted);
        }
        catch (FormatException)
        {
            stdout.Write("You cannot omit unit.");
        }
        stdout.WriteLine();
    }
}