using TarkovBot.Config;

namespace TarkovBot.Services.Abstractions;

public interface IConfigService
{
    BotConfig Config { get; }
}