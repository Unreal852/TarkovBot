using System.Text;

namespace TarkovBot.Misc;

public class MarkdownBuilder
{
    private readonly StringBuilder _stringBuilder;

    public MarkdownBuilder(string text = "")
    {
        _stringBuilder = new StringBuilder(text);
    }

    public MarkdownBuilder AppendBold(string text)
    {
        _stringBuilder.Append($"**{text}**");
        return this;
    }

    public MarkdownBuilder AppendLineBold(string text)
    {
        _stringBuilder.AppendLine($"**{text}**");
        return this;
    }

    public MarkdownBuilder AppendItalic(string text)
    {
        _stringBuilder.Append($"*{text}*");
        return this;
    }

    public MarkdownBuilder AppendLineItalic(string text)
    {
        _stringBuilder.AppendLine($"*{text}*");
        return this;
    }

    public MarkdownBuilder AppendUnderline(string text)
    {
        _stringBuilder.Append($"_{text}_");
        return this;
    }

    public MarkdownBuilder AppendLineUnderline(string text)
    {
        _stringBuilder.AppendLine($"_{text}_");
        return this;
    }

    public MarkdownBuilder AppendStrike(string text)
    {
        _stringBuilder.Append($"~~{text}~~");
        return this;
    }

    public MarkdownBuilder AppendLineStrike(string text)
    {
        _stringBuilder.AppendLine($"~~{text}~~");
        return this;
    }

    public MarkdownBuilder AppendCode(string text)
    {
        _stringBuilder.Append($"`{text}`");
        return this;
    }

    public MarkdownBuilder AppendLineCode(string text)
    {
        _stringBuilder.AppendLine($"`{text}`");
        return this;
    }

    public MarkdownBuilder AppendSpoiler(string text)
    {
        _stringBuilder.Append($"||{text}||");
        return this;
    }

    public MarkdownBuilder AppendLineSpoiler(string text)
    {
        _stringBuilder.AppendLine($"||{text}||");
        return this;
    }

    public MarkdownBuilder AppendHeader(string text, int level)
    {
        _stringBuilder.Append($"{new('#', level)} {text}");
        return this;
    }

    public MarkdownBuilder AppendLineHeader(string text, int level)
    {
        _stringBuilder.AppendLine($"{new('#', level)}{text}");
        return this;
    }

    public MarkdownBuilder AppendBulletedList(params string[] list)
    {
        foreach (string element in list)
            _stringBuilder.AppendLine($"* {element}");
        return this;
    }

    public MarkdownBuilder AppendLineDivider()
    {
        _stringBuilder.AppendLine("---");
        return this;
    }

    public MarkdownBuilder AppendLink(string text, string url)
    {
        _stringBuilder.Append($"[{text}]({url})");
        return this;
    }

    public MarkdownBuilder AppendLineLink(string text, string url)
    {
        _stringBuilder.AppendLine($"[{text}]({url})");
        return this;
    }

    public MarkdownBuilder AppendNumberedList(params string[] list)
    {
        for (var i = 0; i < list.Length; i++)
            _stringBuilder.AppendLine($"{i}. {list[i]}");
        return this;
    }

    public MarkdownBuilder Append(string text)
    {
        _stringBuilder.Append(text);
        return this;
    }

    public MarkdownBuilder AppendLine(string text)
    {
        _stringBuilder.AppendLine(text);
        return this;
    }

    public override string ToString()
    {
        return _stringBuilder.ToString();
    }
}