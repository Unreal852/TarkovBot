using System.Text.Json;
using Serilog;
using TarkovBot.Config;
using TarkovBot.Services.Abstractions;

namespace TarkovBot.Services;

public sealed class ConfigService : IConfigService
{
    private static readonly string     ConfigDirectory = Path.Combine(AppContext.BaseDirectory, "Config");
    private                 BotConfig? _config;

    public BotConfig Config => _config ??= LoadConfig();

    private static BotConfig LoadConfig()
    {
        if (!Directory.Exists(ConfigDirectory))
            throw new DirectoryNotFoundException(ConfigDirectory);
        var filePath = Path.Combine(ConfigDirectory, "config.json");
        if (!File.Exists(filePath))
            throw new FileNotFoundException("Config file not found", filePath);
        var fileContent =File.ReadAllText(filePath);
        var config = JsonSerializer.Deserialize<BotConfig>(fileContent) ??
                     throw new Exception("Failed to deserialize config");
        return config;
    }
}