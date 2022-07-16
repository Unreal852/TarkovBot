namespace TarkovBot.Core.Extensions;

public static class StringExtensions
{
    public static string FirstCharToLowerCase(this string? str)
    {
        if (!string.IsNullOrEmpty(str) && char.IsUpper(str[0]))
            return str.Length == 1 ? char.ToLower(str[0]).ToString() : $"{char.ToLower(str[0])}{str[1..]}";
        return str;
    }

    public static string FirstCharToUpperCase(this string? str)
    {
        if (!string.IsNullOrEmpty(str) && !char.IsUpper(str[0]))
            return str.Length == 1 ? char.ToUpper(str[0]).ToString() : $"{char.ToUpper(str[0])}{str[1..]}";
        return str;
    }

    public static string ReplaceFirst(this string text, string search, string replace)
    {
        int pos = text.IndexOf(search, StringComparison.InvariantCultureIgnoreCase);
        return pos < 0 ? text : string.Concat(text[..pos], replace, text.AsSpan(pos + search.Length));
    }
}