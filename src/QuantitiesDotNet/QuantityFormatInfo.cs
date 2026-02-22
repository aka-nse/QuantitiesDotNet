using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace QuantitiesDotNet;

public partial record QuantityFormatInfo(
    string NumberFormat,
    string Spacing,
    string UnitSelector,
    bool HasBrackets)
{
    // lang=regex
    private const string _escapeMatcherPattern = @"\\(.)";
    private static readonly Regex _escapeMatcher
#if NET7_0_OR_GREATER
        = GenerateEscapeMatcher();
    [GeneratedRegex(_escapeMatcherPattern)]
    private static partial Regex GenerateEscapeMatcher();
#else
        = new(_escapeMatcherPattern, RegexOptions.Compiled);
#endif

    // lang=regex
    private const string _formatMatcherPattern = @"^(?<number>(?:[^\s&]|\\.)*)(?:&(?<spacing>\s*)(?<open>\[?)(?<unit>(?:[^\s\[\]]|\\.)*)(?<close>\]?))?$";
    private static readonly Regex _formatMatcher
#if NET7_0_OR_GREATER
        = GenerateFormatMatcher();
    [GeneratedRegex(_formatMatcherPattern)]
    private static partial Regex GenerateFormatMatcher();
#else
        = new(_formatMatcherPattern, RegexOptions.Compiled);
#endif

    public static bool TryCompile(
        ReadOnlySpan<char> format,
        [NotNullWhen(true)] out QuantityFormatInfo? info) =>
        // TODO: implement efficiently without allocating a string
        TryCompile(format.ToString(), out info);


    public static bool TryCompile(
        string? format,
        [NotNullWhen(true)] out QuantityFormatInfo? info)
    {
        info = default!;
        format ??= "";
        var match = _formatMatcher.Match(format);
        if (!match.Success)
        {
            return false;
        }
        bool hasBrackets;
        switch ((match.Groups["open"].Value, match.Groups["close"].Value))
        {
        case ("", ""):
            hasBrackets = false;
            break;
        case ("[", "]"):
            hasBrackets = true;
            break;
        default:
            return false;
        }
        info = new(
            _escapeMatcher.Replace(match.Groups["number"].Value, "$1"),
            match.Groups["spacing"].Value,
            _escapeMatcher.Replace(match.Groups["unit"].Value, "$1"),
            hasBrackets);
        return true;
    }


    public string Format(ReadOnlySpan<byte> utf8Number, ReadOnlySpan<byte> utf8Unit)
    {
        // TODO: implement efficiently without allocating a string
        var number = (stackalloc char[Encoding.UTF8.GetMaxCharCount(utf8Number.Length)]);
        var unit = (stackalloc char[Encoding.UTF8.GetMaxCharCount(utf8Unit.Length)]);
        number = number.Slice(0, Encoding.UTF8.GetChars(utf8Number, number));
        unit = unit.Slice(0, Encoding.UTF8.GetChars(utf8Unit, unit));
        return Format(number, unit);
    }


    public string Format(ReadOnlySpan<char> number, ReadOnlySpan<char> unit)
    {
        var buffer = (stackalloc char[number.Length + Spacing.Length + (HasBrackets ? 2 : 0) + unit.Length]);
        Format(buffer, number, unit);
        return buffer.ToString();
    }

    public bool TryFormat(ReadOnlySpan<char> number, ReadOnlySpan<char> unit, Span<byte> utf8Destination, out int bytesWritten)
    {
        // TODO: implement efficiently without allocating a string
        char[]? heapBuffer = null;
        try
        {
            var charBufferSize = Encoding.UTF8.GetMaxCharCount(utf8Destination.Length);
            Span<char> destination = charBufferSize <= 256
                ? stackalloc char[charBufferSize]
                : (heapBuffer = ArrayPool<char>.Shared.Rent(utf8Destination.Length));
            if (!TryFormat(number, unit, destination, out var charsWritten))
            {
                bytesWritten = default;
                return false;
            }
            bytesWritten = Encoding.UTF8.GetBytes(destination.Slice(0, charsWritten), utf8Destination);
            return true;
        }
        finally
        {
            if(heapBuffer is { })
            {
                ArrayPool<char>.Shared.Return(heapBuffer);
            }
        }
    }

    public bool TryFormat(ReadOnlySpan<char> number, ReadOnlySpan<char> unit, Span<char> destination, out int charsWritten)
    {
        var length = number.Length + Spacing.Length + (HasBrackets ? 2 : 0) + unit.Length;
        if (destination.Length < length)
        {
            charsWritten = default;
            return false;
        }
        charsWritten = Format(destination, number, unit);
        return true;
    }

    private int Format(Span<char> destination, ReadOnlySpan<char> number, ReadOnlySpan<char> unit)
    {
        var cursor = 0;
        number.CopyTo(destination.Slice(cursor));
        cursor += number.Length;
        Spacing.AsSpan().CopyTo(destination.Slice(cursor));
        cursor += Spacing.Length;
        if (HasBrackets)
        {
            destination[cursor] = '[';
            ++cursor;
        }
        unit.CopyTo(destination.Slice(cursor));
        cursor += unit.Length;
        if (HasBrackets)
        {
            destination[cursor] = ']';
            ++cursor;
        }
        return cursor;
    }
}