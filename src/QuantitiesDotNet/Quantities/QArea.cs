using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of area.
/// This type can be re-interpret-casted into <see cref="double"/> as [m^2] scale.
/// </summary>
[Quantity(L: 2, M: 0, T: 0, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("Metre2", "m^2", 1.0, None | Centi | Milli | Micro | Nano | Kilo, powerOfPrefix: 2)]
[QuantityUnit("Hectare", "ha", 1.0e+4)]
[QuantityOperation(typeof(QLength), typeof(QLength), typeof(QArea))]
public readonly partial struct QArea : IQuantity<QArea>
{
}