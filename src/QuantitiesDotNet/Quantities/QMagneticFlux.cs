using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of magnetic flux.
/// This type can be re-interpret-casted into <see cref="double"/> as [Wb] scale.
/// </summary>
[Quantity(L: 2, M: 1, T: -2, I: -1, Th: 0, N: 0, J: 0)]
[QuantityUnit("Weber", "Wb", 1.0, None | Milli | Micro | Nano)]
public readonly partial struct QMagneticFlux : IQuantity<QMagneticFlux>
{
}