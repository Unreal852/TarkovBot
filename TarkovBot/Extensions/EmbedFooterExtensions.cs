using Guilded.Base.Embeds;
using Serilog;
using TarkovBot.EFT.Data.Raw;

namespace TarkovBot.Extensions;

/// <summary>
/// Extensions for the <see cref="EmbedFooter"/> type.
/// </summary>
public static class EmbedFooterExtensions
{
    /// <summary>
    /// Parse infos from the specified <see cref="EmbedFooter"/>
    /// If the operation fails, the returned tuple is (<see cref="LanguageCode.en"/>, <see cref="string.Empty"/>)
    /// </summary>
    /// <param name="embedFooter">The embed footer to parse from</param>
    /// <returns>The parsed informations</returns>
    public static (LanguageCode Languague, string Id) ParseInfos(this EmbedFooter embedFooter)
    {
        if (string.IsNullOrWhiteSpace(embedFooter.Text))
        {
            Log.Warning("The embed footer text is null or empty");
            return (LanguageCode.en, string.Empty);
        }

        string[] split = embedFooter.Text.Split('-', 2);
        if (split.Length != 2)
        {
            Log.Warning("The embed footer lenght is {Lenght} but it should be {Required}", split.Length, 2);
            return (LanguageCode.en, string.Empty);
        }

        return Enum.TryParse(split[0], out LanguageCode language) ? (language, split[1]) : (LanguageCode.en, string.Empty);
    }
}