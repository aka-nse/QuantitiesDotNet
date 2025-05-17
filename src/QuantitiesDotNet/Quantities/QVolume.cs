using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of volume.
/// This type can be re-interpret-casted into <see cref="double"/> as [m^3] scale.
/// </summary>
[Quantity(L: 3, M: 0, T: 0, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("CubicMetre", "m^3", 1.0, None | Centi, 3)]
[QuantityUnit("Litre", "L", 1e-3, None, 3)]
[QuantityOperation(typeof(QLength), typeof(QArea), typeof(QVolume))]
public readonly partial struct QVolume : IQuantity<QVolume>
{
}