using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of electric resistance.
/// This type can be re-interpret-casted into <see cref="double"/> as [Ω] scale.
/// </summary>
[Quantity(L: 2, M: 1, T: -3, I: -2, Th: 0, N: 0, J: 0)]
[QuantityUnit("Ohm", "Ω", 1.0, None | Milli | Kilo | Mega | Giga)]
[QuantityOperation(typeof(QElectricCurrent), typeof(QElectricResistance), typeof(QElectricVoltage))]
public readonly partial struct QElectricResistance : IQuantity<QElectricResistance>
{
}