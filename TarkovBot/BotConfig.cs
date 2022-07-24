using TarkovBot.EFT.Data;

namespace TarkovBot;

/// <summary>
/// Represents the bot configuration.
/// </summary>
public class BotConfig
{
    public static BotConfig Instance { get; private set; }

    public string         GuildedToken       { get; set; }
    public string         Prefix             { get; set; }
    public LanguageCode[] SupportedLanguages { get; set; }
    public bool           IsValid            => !string.IsNullOrWhiteSpace(GuildedToken) && !string.IsNullOrWhiteSpace(Prefix);

    public BotConfig()
    {
#if DEBUG
        GuildedToken = Environment.GetEnvironmentVariable(nameof(GuildedToken)) ??
                       throw new ArgumentNullException($"Missing guilded token in environment variable.");
        Prefix = Environment.GetEnvironmentVariable(nameof(Prefix)) ?? "?t";
        SupportedLanguages = new[] { LanguageCode.en, LanguageCode.fr }; // TODO: Not yet implemented.
#endif
        Instance = this;
    }
}