using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of electric conductivity.
/// This type can be re-interpret-casted into <see cref="double"/> as [S/m] scale.
/// </summary>
[Quantity(L: -3, M: -1, T: 3, I: 2, Th: 0, N: 0, J: 0)]
[QuantityUnit("SiemensPerMetre", "S/m", 1.0, None | Milli | Micro | Nano | Pico)]
[QuantityOperation(typeof(QLength), typeof(QElectricConductivity), typeof(QElectricConductance))]
public readonly partial struct QElectricConductivity : IQuantity<QElectricConductivity>
{
}