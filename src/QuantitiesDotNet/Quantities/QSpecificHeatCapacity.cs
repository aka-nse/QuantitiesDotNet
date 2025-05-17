using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of specific heat capacity.
/// This type can be re-interpret-casted into <see cref="double"/> as [J/(Kg*K)] scale.
/// </summary>
[Quantity(L: 2, M: 0, T: -2, I: 0, Th: -1, N: 0, J: 0)]
[QuantityUnit("JoulePerKilogramPerKelvin", "J/(Kg*K)", 1.0, None | Kilo)]
[QuantityOperation(typeof(QMass), typeof(QSpecificHeatCapacity), typeof(QHeatCapacity))]
public readonly partial struct QSpecificHeatCapacity : IQuantity<QSpecificHeatCapacity>
{
}