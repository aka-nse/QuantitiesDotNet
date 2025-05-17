namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of dimensionless.
/// This type can be re-interpret-casted into <see cref="double"/> as [1] scale.
/// </summary>
[Quantity(L: 0, M: 0, T: 0, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("Raw", "1", 1)]
public readonly partial struct QDimensionless : IQuantity
{
}