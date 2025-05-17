namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of torque.
/// This type can be re-interpret-casted into <see cref="double"/> as [N*m] scale.
/// </summary>
[Quantity(L: 2, M: 1, T: -2, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("NewtonMetre", "N*m", 1.0)]
public readonly partial struct QTorque : IQuantity<QTorque>
{
}