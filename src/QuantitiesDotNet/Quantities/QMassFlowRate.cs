using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of mass flow rate.
/// This type can be re-interpret-casted into <see cref="double"/> as [kg/s] scale.
/// </summary>
[Quantity(L: 0, M: 1, T: -1, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("GramPerSecond", "g/s", 1e-3, None | Milli | Kilo)]
[QuantityOperation(typeof(QMassFlowRate), typeof(QTime), typeof(QMass))]
public readonly partial struct QMassFlowRate : IQuantity<QMassFlowRate>
{
}