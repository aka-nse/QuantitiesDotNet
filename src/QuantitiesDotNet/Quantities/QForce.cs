using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of mechanical force.
/// This type can be re-interpret-casted into <see cref="double"/> as [N] scale.
/// </summary>
[Quantity(L: 1, M: 1, T: -2, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("Newton", "N", 1.0, None | Milli | Kilo | Mega, exportsShorthandSymbol: true)]
[QuantityOperation(typeof(QMass), typeof(QAcceleration), typeof(QForce))]
public readonly partial struct QForce : IQuantity<QForce>
{
}