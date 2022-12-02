using Jab;
using Serilog;
using TarkovBot.Services.Abstractions;
using TarkovBot.Services.Commands;

// ReSharper disable UnusedType.Local

namespace TarkovBot.Services;

[ServiceProvider]
[Import<ICommandsProviderModule>]
[Singleton<ILogger>(Instance = nameof(Logger))]
[Singleton<IConfigService, ConfigService>]
[Singleton<IGuildedService, GuildedService>]
[Singleton<IGuildedMessageReactionService, GuildedMessageReactionService>]
[Singleton<IGraphQlClientService, GraphQlClientServiceService>]
[Singleton<IItemsProvider, TarkovItemsProviderService>]
public partial class ServiceProvider
{
    public static ServiceProvider Instance { get; private set; } = default!;

    public ILogger Logger => Log.Logger;

    public ServiceProvider()
    {
        Instance = this;
    }
}

[ServiceProviderModule]
[Singleton<IGuildedCommand, ItemsCommand>]
file interface ICommandsProviderModule
{
}