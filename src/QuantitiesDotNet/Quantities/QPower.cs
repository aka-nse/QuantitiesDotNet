using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of power.
/// This type can be re-interpret-casted into <see cref="double"/> as [W] scale.
/// </summary>
[Quantity(L: 2, M: 1, T: -3, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("Watt", "W", 1.0, None | Milli | Micro | Nano | Pico | Femto | Kilo | Mega | Giga | Tera | Peta, exportsShorthandSymbol: true)]
[QuantityOperation(typeof(QTime), typeof(QPower), typeof(QEnergy))]
[QuantityOperation(typeof(QElectricCurrent), typeof(QElectricVoltage), typeof(QPower))]
public readonly partial struct QPower : IQuantity<QPower>
{
}