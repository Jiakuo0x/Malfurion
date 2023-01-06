namespace Malfurion.Extensions;

using System.Text.RegularExpressions;

public static class StringVerificationExtention
{
    public static bool OnlyLettersAndNumbers(this string value)
        => value.IsMatchRegex(@"^[0-9A-Za-z]+$");

    public static bool OnlyLettersAndNumbers(this string value, int length)
        => value.IsMatchRegex($"^[0-9A-Za-z]{{{length}}}$");
    
    public static bool IsMatchRegex(this string value, string regex)
    {
        Regex matcher = new Regex(regex);
        return matcher.IsMatch(value);
    }
}