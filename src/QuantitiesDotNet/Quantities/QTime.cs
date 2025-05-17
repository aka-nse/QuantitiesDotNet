using static QuantitiesDotNet.UnitPrefix;

namespace QuantitiesDotNet;

/// <summary>
/// Represents a value of time.
/// This type can be re-interpret-casted into <see cref="double"/> as [s] scale.
/// </summary>
[Quantity(L: 0, M: 0, T: 1, I: 0, Th: 0, N: 0, J: 0)]
[QuantityUnit("Second", "s", 1.0, None | Milli | Micro | Nano | Pico | Femto, exportsShorthandSymbol: true)]
[QuantityUnit("Minute", "min", 60.0, exportsShorthandSymbol: true)]
[QuantityUnit("Hour", "h", 3600.0, exportsShorthandSymbol: true)]
public readonly partial struct QTime : IQuantity<QTime>
{
    /// <summary>
    /// Converts from <see cref="QTime"/> to <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="time"></param>
    public static explicit operator TimeSpan(QTime time)
        => TimeSpan.FromSeconds(time.Second);

    /// <summary>
    /// Converts from <see cref="TimeSpan"/> to <see cref="QTime"/>.
    /// </summary>
    /// <param name="time"></param>
    public static explicit operator QTime(TimeSpan time)
        => FromSecond(time.TotalSeconds);
}