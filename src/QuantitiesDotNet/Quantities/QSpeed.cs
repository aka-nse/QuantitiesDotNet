using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of velocity.
/// This type can be re-interpret-casted into <see cref="double"/> as [m/s] scale.
/// </summary>
[Quantity(L: 1, M: 0, T: -1, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("MetrePerSecond", "m/s", 1.0, None | Milli | Micro | Nano | Kilo)]
[QuantityUnit("MetrePerMinute", "m/min", 1.0 / 60)]
[QuantityUnit("KilometrePerHour", "km/h", 1.0e+3 / 3600)]
[QuantityOperation(typeof(QTime), typeof(QSpeed), typeof(QLength))]
public readonly partial struct QSpeed : IQuantity<QSpeed>
{
}