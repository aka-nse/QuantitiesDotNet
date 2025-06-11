using static QuantitiesDotNet.UnitPrefix;
namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of spatial frequency.
/// This type can be re-interpret-casted into <see cref="double"/> as [m^-1] scale.
/// </summary>
[Quantity(L: -1, M: 0, T: 0, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("ReciprocalMetre"     , "m^-1" , 1.0, None, -1)]
[QuantityUnit("ReciprocalCentimetre", "cm^-1", 1e+2, None, -1)]
[QuantityUnit("ReciprocalMillimetre", "mm^-1", 1e+3, None, -1)]
[QuantityUnit("ReciprocalMicrometre", "um^-1", 1e+6, None, -1)]
[QuantityUnit("ReciprocalNanometre" , "nm^-1", 1e+9, None, -1)]
[QuantityUnit("ReciprocalKilometre" , "km^-1", 1e-3, None, -1)]
[QuantityOperation(typeof(QSpatialFrequency), typeof(QLength), typeof(QDimensionless))]
public readonly partial struct QSpatialFrequency : IQuantity<QSpatialFrequency, double>
{
}