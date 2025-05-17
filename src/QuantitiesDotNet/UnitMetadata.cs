namespace QuantitiesDotNet;

public record UnitMetadata<T>(
    T Scale,
    string MajorName,
    string UnitSymbol)
#if NET7_0_OR_GREATER
    where T : notnull, INumber<T>
#else
    where T : notnull
#endif
{
}
