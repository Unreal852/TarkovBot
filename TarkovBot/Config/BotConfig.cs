namespace TarkovBot.Config;

public class BotConfig
{
    public string Token  { get; set; } = default!;
    public string Prefix { get; set; } = default!;

    public static BotConfig? FromEnvVariables()
    {
        var token = Environment.GetEnvironmentVariable("TOKEN");
        var prefix = Environment.GetEnvironmentVariable("PREFIX");
        if (!string.IsNullOrWhiteSpace(token))
            return new BotConfig() { Token = token, Prefix = prefix ?? "!" };
        return default;
    }
}