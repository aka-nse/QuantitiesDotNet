namespace QuantitiesDotNet;

public class CustomTypeConversionTest
{
    [Fact]
    public void TimeSpanToQTime()
    {
        var timeSpan = TimeSpan.FromSeconds(1.234);
        var qTime = (QTime)timeSpan;
        Assert.IsType<QTime>(qTime);
        Assert.Equal(timeSpan.TotalSeconds, qTime.Second);
    }

    [Fact]
    public void QTimeToTimeSpan()
    {
        var qTime = QTime.FromSecond(1.234);
        var timeSpan = (TimeSpan)qTime;
        Assert.IsType<TimeSpan>(timeSpan);
        Assert.Equal(qTime.Second, timeSpan.TotalSeconds);
    }
}
