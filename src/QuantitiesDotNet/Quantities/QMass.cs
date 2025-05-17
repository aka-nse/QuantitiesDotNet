using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of mass.
/// This type can be re-interpret-casted into <see cref="double"/> as [kg] scale.
/// </summary>
[Quantity(L: 0, M: 1, T: 0, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("Gram", "g", 1.0e-3, None | Milli | Micro | Kilo, exportsShorthandSymbol: true)]
[QuantityUnit("Tonne", "t", 1.0e+3)]
public readonly partial struct QMass : IQuantity<QMass>
{
}