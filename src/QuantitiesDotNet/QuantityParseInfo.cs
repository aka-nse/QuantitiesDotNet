using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace QuantitiesDotNet;

public partial record QuantityParseInfo(
    string Number,
    string UnitSelector)
{
    // lang=regex
    private const string _NumberPattern = @"(?<number>[\+\-]?\d+(?:[\.\,]\d*)?(?:[Ee][\+\-]\d+)?)";

    // lang=regex
    private const string _BracketedUnitPattern = @"\[(?<unit>[\w/^*()\s\+\-]+)\]";

    // lang=regex
    private const string _BareUnitPattern = @"(?<unit>[\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Lm}\p{Nl}_(][\w/^*()\s\+\-]*)";

    // lang=regex
    private const string _FormatMatcherPattern = @$"^{_NumberPattern}\s*(?:{_BracketedUnitPattern}|{_BareUnitPattern})$";

    private static readonly Regex _FormatMatcher
#if NET7_0_OR_GREATER
        = GenerateFormatMatcher();
    [GeneratedRegex(_FormatMatcherPattern)]
    private static partial Regex GenerateFormatMatcher();
#else
        = new(_FormatMatcherPattern, RegexOptions.Compiled);
#endif

    public static bool TryCompile(
        string? expression,
        [NotNullWhen(true)] out QuantityParseInfo? info)
    {
        expression = expression is { } ? expression.Trim() : "";
        var match = _FormatMatcher.Match(expression);
        if (!match.Success)
        {
            info = default!;
            return false;
        }
        info = new(match.Groups["number"].Value, match.Groups["unit"].Value);
        return true;
    }
}