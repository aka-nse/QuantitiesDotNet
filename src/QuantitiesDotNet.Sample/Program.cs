// See https://aka.ms/new-console-template for more information
using QuantitiesDotNet;
using QuantitiesDotNet.Samples;


Console.WriteLine(QLength.FromMetre(1).Millimetre);

var samples = new IUsageSample[]
{
    new BasicUsage(),
    new Formatting(),
    new Parsing(),
    new ReinterpretCast(),
    new Generics(),
    new UnitShorthandsUsage(),
    new QuantityInfos(),
};

foreach (var sample in samples)
{
    Console.WriteLine($"【{sample.SampleName}】");
    sample.Execute(Console.Out);
    Console.WriteLine();
}