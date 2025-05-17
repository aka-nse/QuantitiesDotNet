using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of acceleration.
/// This type can be re-interpret-casted into <see cref="double"/> as [m/s^2] scale.
/// </summary>
[Quantity(L: 1, M: 0, T: -2, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("MetrePerSquareSecond", "m/s^2", 1.0, None | Milli | Kilo)]
[QuantityOperation(typeof(QTime), typeof(QAcceleration), typeof(QSpeed))]
public readonly partial struct QAcceleration : IQuantity<QAcceleration>
{
}