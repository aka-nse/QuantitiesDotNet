namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of thermodynamic temperature.
/// This type can be re-interpret-casted into <see cref="double"/> as [K] scale.
/// </summary>
[Quantity(L: 0, M: 0, T: 0, I: 0, Th: 1, N: 0, J: 0)]
[QuantityUnit("Kelvin", "K", 1.0, exportsShorthandSymbol: true)]
public readonly partial struct QTemperature : IQuantity<QTemperature>
{
}