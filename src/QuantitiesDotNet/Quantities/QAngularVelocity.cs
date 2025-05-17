using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of angular velocity.
/// This type can be re-interpret-casted into <see cref="double"/> as [rad/s] scale.
/// </summary>
[Quantity(L: 0, M: 0, T: -1, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("RadianPerSecond", "rad/s", 1.0)]
[QuantityUnit("DegreePerSecond", "deg/s", 2 * Math.PI / 360, None | Milli)]
[QuantityUnit("RevolutionPerMinute", "rpm", 2 * Math.PI / 60)]
[QuantityOperation(typeof(QTime), typeof(QAngularVelocity), typeof(QAngle))]
public readonly partial struct QAngularVelocity : IQuantity<QAngularVelocity>
{
}