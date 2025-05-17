using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of magnetic flux density.
/// This type can be re-interpret-casted into <see cref="double"/> as [T] scale.
/// </summary>
[Quantity(L: 0, M: 1, T: -2, I: -1, Th: 0, N: 0, J: 0)]
[QuantityUnit("Tesla", "T", 1.0, None | Milli | Micro)]
[QuantityOperation(typeof(QArea), typeof(QMagneticFluxDensity), typeof(QMagneticFlux))]
public readonly partial struct QMagneticFluxDensity : IQuantity<QMagneticFluxDensity>
{
}