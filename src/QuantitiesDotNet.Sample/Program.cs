// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;
using QuantitiesDotNet;
using QuantitiesDotNet.Samples;

var samples = new IUsageSample[]
{
    // new BasicUsage(),
    // new Formatting(),
    // new Parsing(),
    new ReinterpretCast(),
    // new Generics(),
    // new UnitShorthandsUsage(),
    // new QuantityInfos(),
};

var rawValueGeneric = 1.234m;
var x = QuantitiesDotNet.Generic.QSpeed<decimal>.FromMetrePerSecond(rawValueGeneric);
Console.WriteLine(x);
// Console.WriteLine(Unsafe.As<decimal, QuantitiesDotNet.Generic.QSpeed<decimal>>(ref rawValueGeneric));  // 1.234m/s
foreach (var sample in samples)
{
    Console.WriteLine($"【{sample.SampleName}】");
    sample.Execute(Console.Out);
    Console.WriteLine();
}