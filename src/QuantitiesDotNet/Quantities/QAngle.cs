using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of angle.
/// This type can be re-interpret-casted into <see cref="double"/> as [rad] scale.
/// </summary>
[Quantity(L: 0, M: 0, T: 0, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("Radian", "rad", 1.0)]
[QuantityUnit("Degree", "deg", 2 * Math.PI / 360, None | Milli)]
public readonly partial struct QAngle : IQuantity<QAngle>
{
}