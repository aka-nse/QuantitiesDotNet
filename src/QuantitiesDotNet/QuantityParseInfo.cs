using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace QuantitiesDotNet;

public partial record QuantityParseInfo(
    string Number,
    string UnitSelector)
{
    // lang=regex
    private const string _numberPattern = @"(?<number>[\+\-]?\d+(?:[\.\,]\d*)?(?:[Ee][\+\-]\d+)?)";

    // lang=regex
    private const string _bracketedUnitPattern = @"\[(?<unit>[\w/^*()\s\+\-]+)\]";

    // lang=regex
    private const string _bareUnitPattern = @"(?<unit>[\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Lm}\p{Nl}_(][\w/^*()\s\+\-]*)";

    // lang=regex
    private const string _formatMatcherPattern = @$"^{_numberPattern}\s*(?:{_bracketedUnitPattern}|{_bareUnitPattern})$";

    private static readonly Regex _formatMatcher
#if NET7_0_OR_GREATER
        = GenerateFormatMatcher();
    [GeneratedRegex(_formatMatcherPattern)]
    private static partial Regex GenerateFormatMatcher();
#else
        = new(_formatMatcherPattern, RegexOptions.Compiled);
#endif

    public static bool TryCompile(
        string? expression,
        [NotNullWhen(true)] out QuantityParseInfo? info)
    {
        expression = expression is { } ? expression.Trim() : "";
        var match = _formatMatcher.Match(expression);
        if (!match.Success)
        {
            info = default!;
            return false;
        }
        info = new(match.Groups["number"].Value, match.Groups["unit"].Value);
        return true;
    }
}