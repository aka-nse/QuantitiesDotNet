using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of thermodynamic electric field strength.
/// This type can be re-interpret-casted into <see cref="double"/> as [V/m] scale.
/// </summary>
[Quantity(L: 1, M: 1, T: -3, I: -1, Th: 0, N: 0, J: 0)]
[QuantityUnit("VoltPerMetre", "V/m", 1.0, None | Milli | Micro | Nano | Kilo)]
[QuantityOperation(typeof(QLength), typeof(QElectricFieldStrength), typeof(QElectricVoltage))]
public readonly partial struct QElectricFieldStrength : IQuantity<QElectricFieldStrength>
{
}