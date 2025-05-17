using static QuantitiesDotNet.UnitPrefix;
namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of spatial frequency.
/// This type can be re-interpret-casted into <see cref="double"/> as [m^-1] scale.
/// </summary>
[Quantity(L: -1, M: 0, T: 0, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("ReciprocalMetre", "m^-1", 1.0, None | Centi | Milli | Micro | Nano | Kilo, -1)]
[QuantityOperation(typeof(QSpatialFrequency), typeof(QLength), typeof(QDimensionless))]
public readonly partial struct QSpatialFrequency : IQuantity<QSpatialFrequency>
{
}