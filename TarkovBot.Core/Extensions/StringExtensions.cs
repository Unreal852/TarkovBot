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

    public static string SimpleReplace(this string str, params object[] args)
    {
        if (args.Length == 0)
            return str;
        var final = str;
        for (var i = 0; i < args.Length; i++)
        {
            final = final.Replace($"${{{i}}}", args[i].ToString());
        }

        return final;
    }
}