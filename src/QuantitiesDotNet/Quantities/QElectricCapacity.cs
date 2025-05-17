using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of electric capacity.
/// This type can be re-interpret-casted into <see cref="double"/> as [F] scale.
/// </summary>
[Quantity(L: -2, M: -1, T: 4, I: 2, Th: 0, N: 0, J: 0)]
[QuantityUnit("Farad", "F", 1.0, None | Milli | Micro | Nano | Pico)]
[QuantityOperation(typeof(QElectricCapacity), typeof(QElectricVoltage), typeof(QElectricCharge))]
public readonly partial struct QElectricCapacity : IQuantity<QElectricCapacity>
{
}