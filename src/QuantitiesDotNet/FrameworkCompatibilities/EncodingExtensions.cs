#pragma warning disable IDE0130
namespace QuantitiesDotNet;

internal static class EncodingExtensions
{
    extension(Encoding encoding)
    {
#if !(NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER)
        public string GetString(ReadOnlySpan<byte> bytes)
        {
            unsafe
            {
                fixed (byte* p = bytes)
                {
                    return encoding.GetString(p, bytes.Length);
                }
            }
        }

        public int GetChars(ReadOnlySpan<byte> bytes, Span<char> chars)
        {
            unsafe
            {
                fixed (byte* bytesPtr = bytes)
                fixed (char* charsPtr = chars)
                {
                    return encoding.GetChars(bytesPtr, bytes.Length, charsPtr, chars.Length);
                }
            }
        }

        public int GetBytes(ReadOnlySpan<char> chars, Span<byte> bytes)
        {
            unsafe
            {
                fixed (byte* bytesPtr = bytes)
                fixed (char* charsPtr = chars)
                {
                    return encoding.GetBytes(charsPtr, chars.Length, bytesPtr, bytes.Length);
                }
            }
        }
#endif

    }
}
