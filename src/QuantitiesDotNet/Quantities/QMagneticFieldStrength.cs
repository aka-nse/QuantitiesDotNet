using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of magnetic field strength.
/// This type can be re-interpret-casted into <see cref="double"/> as [A/m] scale.
/// </summary>
[Quantity(L: -1, M: 0, T: 0, I: 1, Th: 0, N: 0, J: 0)]
[QuantityUnit("AmparePerMetre", "A/m", 1.0, None | Milli | Kilo)]
[QuantityOperation(typeof(QLength), typeof(QMagneticFieldStrength), typeof(QElectricCurrent))]
public readonly partial struct QMagneticFieldStrength : IQuantity<QMagneticFieldStrength>
{
}