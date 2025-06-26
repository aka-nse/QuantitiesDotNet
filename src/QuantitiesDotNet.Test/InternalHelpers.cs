namespace QuantitiesDotNet;

internal static class InternalHelpers
{
    // This method is used to prevent the compiler from optimizing away the value.
    // It is useful in tests to ensure that the value is actually used.
    public static void NoUse<T>(T value)
        => _ = value;
}
