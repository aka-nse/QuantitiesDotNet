using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of amount of substance.
/// This type can be re-interpret-casted into <see cref="double"/> as [mol] scale.
/// </summary>
[Quantity(L: 0, M: 0, T: 0, I: 0, Th: 0, N: 1, J: 0)]
[QuantityUnit("Mole", "mol", 1.0, None | Milli | Kilo, exportsShorthandSymbol: true)]
public readonly partial struct QSubstanceAmount : IQuantity<QSubstanceAmount>
{
}