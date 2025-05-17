namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of density.
/// This type can be re-interpret-casted into <see cref="double"/> as [kg/m^3] scale.
/// </summary>
[Quantity(L: -3, M: 1, T: 0, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("KilogramPerCubicMetre", "kg/m^3", 1.0)]
[QuantityUnit("GramPerCubicCentimetre", "g/cm^3", 1.0e-3)]
[QuantityOperation(typeof(QVolume), typeof(QDensity), typeof(QMass))]
public readonly partial struct QDensity : IQuantity<QDensity>
{
}