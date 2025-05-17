using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of frequency.
/// This type can be re-interpret-casted into <see cref="double"/> as [Hz] scale.
/// </summary>
[Quantity(L: 0, M: 0, T: -1, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("Hertz", "Hz", 1.0, None | Kilo | Mega | Giga | Tera, exportsShorthandSymbol: true)]
[QuantityOperation(typeof(QTime), typeof(QFrequency), typeof(QDimensionless))]
public readonly partial struct QFrequency : IQuantity<QFrequency>
{
}