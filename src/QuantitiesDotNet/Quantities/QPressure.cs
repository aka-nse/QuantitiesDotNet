using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of pressure.
/// This type can be re-interpret-casted into <see cref="double"/> as [Pa] scale.
/// </summary>
[Quantity(L: -1, M: 1, T: -2, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("Pascal", "Pa", 1.0, None | Milli | Micro | Nano | Hecto | Kilo | Mega | Giga, exportsShorthandSymbol: true)]
[QuantityOperation(typeof(QArea), typeof(QPressure), typeof(QForce))]
public readonly partial struct QPressure : IQuantity<QPressure>
{
}