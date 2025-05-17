using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of luminous flux.
/// This type can be re-interpret-casted into <see cref="double"/> as [lm] scale.
/// </summary>
[Quantity(L: 0, M: 0, T: 0, I: 0, Th: 0, N: 0, J: 1)]
[QuantityUnit("Lumen", "lm", 1.0, None)]
[QuantityOperation(typeof(QSolidAngle), typeof(QLuminousIntensity), typeof(QLuminousFlux))]
public readonly partial struct QLuminousFlux : IQuantity<QLuminousFlux>
{
}