using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of energy.
/// This type can be re-interpret-casted into <see cref="double"/> as [J] scale.
/// </summary>
[Quantity(L: 2, M: 1, T: -2, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("Joule", "J", 1.0, None | Milli | Kilo | Mega | Giga, exportsShorthandSymbol: true)]
[QuantityUnit("ElectronVolt", "eV", 1.602176634e-19, None | Milli | Kilo | Mega | Giga)]
public readonly partial struct QEnergy : IQuantity<QEnergy>
{
}