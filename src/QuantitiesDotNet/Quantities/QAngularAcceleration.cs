using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of angular acceleration.
/// This type can be re-interpret-casted into <see cref="double"/> as [rad/s^2] scale.
/// </summary>
[Quantity(L: 0, M: 0, T: -2, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("RadianPerSquareSecond", "rad/s^2", 1.0)]
[QuantityUnit("DegreePerSquareSecond", "deg/s^2", 2 * Math.PI / 360, None | Milli)]
[QuantityOperation(typeof(QTime), typeof(QAngularAcceleration), typeof(QAngularVelocity))]
public readonly partial struct QAngularAcceleration : IQuantity<QAngularAcceleration>
{
}