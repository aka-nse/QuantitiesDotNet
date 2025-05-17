using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of electric current.
/// This type can be re-interpret-casted into <see cref="double"/> as [A] scale.
/// </summary>
[Quantity(L: 0, M: 0, T: 0, I: 1, Th: 0, N: 0, J: 0)]
[QuantityUnit("Ampere", "A", 1.0, None | Milli | Micro | Nano | Pico | Kilo, exportsShorthandSymbol: true)]
public readonly partial struct QElectricCurrent : IQuantity<QElectricCurrent>
{
}