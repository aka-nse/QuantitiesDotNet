using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of length.
/// This type can be re-interpret-casted into <see cref="double"/> as [m] scale.
/// </summary>
[Quantity(L: 1, M: 0, T: 0, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("Metre", "m", 1.0, None | Centi | Milli | Micro | Nano | Kilo, exportsShorthandSymbol: true)]
public readonly partial struct QLength : IQuantity<QLength>
{
}