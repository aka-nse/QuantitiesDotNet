using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of electric flux density.
/// This type can be re-interpret-casted into <see cref="double"/> as [C/m^2] scale.
/// </summary>
[Quantity(L: -2, M: 0, T: 1, I: 1, Th: 0, N: 0, J: 0)]
[QuantityUnit("CoulombPerSquareMetre", "C/m^2", 1.0, None | Milli | Micro | Nano | Pico)]
[QuantityOperation(typeof(QArea), typeof(QElectricFluxDensity), typeof(QElectricCharge))]
public readonly partial struct QElectricFluxDensity : IQuantity<QElectricFluxDensity>
{
}