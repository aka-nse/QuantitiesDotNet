using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of electric indactance.
/// This type can be re-interpret-casted into <see cref="double"/> as [H] scale.
/// </summary>
[Quantity(L: 2, M: 1, T: -2, I: -2, Th: 0, N: 0, J: 0)]
[QuantityUnit("Henry", "H", 1.0, None | Milli | Micro | Nano | Pico)]
[QuantityOperation(typeof(QElectricIndactance), typeof(QElectricCurrent), typeof(QMagneticFlux))]
public readonly partial struct QElectricIndactance : IQuantity<QElectricIndactance>
{
}