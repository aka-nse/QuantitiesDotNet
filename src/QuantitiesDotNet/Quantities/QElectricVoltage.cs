using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of voltage.
/// This type can be re-interpret-casted into <see cref="double"/> as [V] scale.
/// </summary>
[Quantity(L: 2, M: 1, T: -3, I: -1, Th: 0, N: 0, J: 0)]
[QuantityUnit("Volt", "V", 1.0, None | Milli | Micro | Kilo | Mega, exportsShorthandSymbol: true)]
public readonly partial struct QElectricVoltage : IQuantity<QElectricVoltage>
{
}