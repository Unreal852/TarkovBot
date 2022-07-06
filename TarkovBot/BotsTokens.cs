namespace TarkovBot;

public class BotsTokens
{
    public string DiscordToken { get; set; }
    public string GuildedToken { get; set; }

    public BotsTokens()
    {
#if DEBUG
        DiscordToken = Environment.GetEnvironmentVariable(nameof(DiscordToken));
        GuildedToken = Environment.GetEnvironmentVariable(nameof(GuildedToken));
#endif
    }
}