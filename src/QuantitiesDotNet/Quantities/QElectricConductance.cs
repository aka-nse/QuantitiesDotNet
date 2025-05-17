using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of electric conductance.
/// This type can be re-interpret-casted into <see cref="double"/> as [S] scale.
/// </summary>
[Quantity(L: -2, M: -1, T: 3, I: 2, Th: 0, N: 0, J: 0)]
[QuantityUnit("Siemens", "S", 1.0, None | Milli | Micro | Nano)]
[QuantityOperation(typeof(QElectricResistance), typeof(QElectricConductance), typeof(QDimensionless))]
public readonly partial struct QElectricConductance : IQuantity<QElectricConductance>
{
}