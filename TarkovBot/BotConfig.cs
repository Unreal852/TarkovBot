namespace TarkovBot;

/// <summary>
/// Represents the bot configuration.
/// </summary>
public class BotConfig
{
    public string GuildedToken { get; set; }
    public string Prefix       { get; set; }
    public bool   IsValid      => !string.IsNullOrWhiteSpace(GuildedToken) && !string.IsNullOrWhiteSpace(Prefix);

    public BotConfig()
    {
#if DEBUG
        GuildedToken = Environment.GetEnvironmentVariable(nameof(GuildedToken)) ??
                       throw new ArgumentNullException($"Missing guilded token in environment variable.");
        Prefix = Environment.GetEnvironmentVariable(nameof(Prefix)) ?? "?";
#endif
    }
}