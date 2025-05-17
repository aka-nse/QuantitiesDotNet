using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of luminance.
/// This type can be re-interpret-casted into <see cref="double"/> as [cd/m^2] scale.
/// </summary>
[Quantity(L: -2, M: 0, T: 0, I: 0, Th: 0, N: 0, J: 1)]
[QuantityUnit("CandelaPerSquareMetre", "cd/m^2", 1.0, None)]
[QuantityOperation(typeof(QArea), typeof(QLuminance), typeof(QLuminousIntensity))]
public readonly partial struct QLuminance : IQuantity<QLuminance>
{
}