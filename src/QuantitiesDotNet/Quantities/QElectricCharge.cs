using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of electric charge.
/// This type can be re-interpret-casted into <see cref="double"/> as [C] scale.
/// </summary>
[Quantity(L: 0, M: 0, T: 1, I: 1, Th: 0, N: 0, J: 0)]
[QuantityUnit("Coulomb", "C", 1.0, None | Milli | Micro | Nano | Pico, exportsShorthandSymbol: true)]
[QuantityUnit("AmpareHour", "Ah", 3600, None | Milli | Kilo)]
[QuantityOperation(typeof(QElectricCurrent), typeof(QTime), typeof(QElectricCharge))]
public readonly partial struct QElectricCharge : IQuantity<QElectricCharge>
{
}