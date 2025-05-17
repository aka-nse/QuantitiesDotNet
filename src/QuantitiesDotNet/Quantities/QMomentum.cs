using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of momentum or impulse.
/// This type can be re-interpret-casted into <see cref="double"/> as [kg*m/s] scale.
/// </summary>
[Quantity(L: 1, M: 1, T: -1, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("GramMetrePerSecond", "g*m/s", 1.0e-3, None | Milli | Micro | Kilo)]
[QuantityUnit("NewtonSecond", "N*s", 1, None | Milli | Kilo)]
[QuantityOperation(typeof(QForce), typeof(QTime), typeof(QMomentum))]
[QuantityOperation(typeof(QMass), typeof(QSpeed), typeof(QMomentum))]
public readonly partial struct QMomentum : IQuantity<QMomentum>
{
}