using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of heat capacity.
/// This type can be re-interpret-casted into <see cref="double"/> as [J/K] scale.
/// </summary>
[Quantity(L: 2, M: 1, T: -2, I: 0, Th: -1, N: 0, J: 0)]
[QuantityUnit("JoulePerKelvin", "J/K", 1.0, None | Milli | Micro | Nano | Kilo | Mega | Giga)]
[QuantityOperation(typeof(QTemperature), typeof(QHeatCapacity), typeof(QEnergy))]
public readonly partial struct QHeatCapacity : IQuantity<QHeatCapacity>
{
}