using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of luminous intensity.
/// This type can be re-interpret-casted into <see cref="double"/> as [cd] scale.
/// </summary>
[Quantity(L: 0, M: 0, T: 0, I: 0, Th: 0, N: 0, J: 1)]
[QuantityUnit("Candela", "cd", 1.0, None, exportsShorthandSymbol: true)]
public readonly partial struct QLuminousIntensity : IQuantity<QLuminousIntensity>
{
}