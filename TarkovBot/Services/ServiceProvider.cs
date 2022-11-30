using Jab;
using Serilog;
using TarkovBot.Services.Abstractions;
using TarkovBot.Services.Commands;

// ReSharper disable UnusedType.Local

namespace TarkovBot.Services;

[ServiceProvider]
[Import(typeof(ICommandsProviderModule))]
[Singleton(typeof(ILogger), Instance = nameof(Logger))]
[Singleton(typeof(IConfigService), typeof(ConfigService))]
[Singleton(typeof(IGuildedService), typeof(GuildedService))]
[Singleton(typeof(IGuildedMessageReactionService), typeof(GuildedMessageReactionService))]
[Singleton(typeof(IGraphQlClientService), typeof(GraphQlClientServiceService))]
[Singleton(typeof(IItemsProvider), typeof(TarkovItemsProviderService))]
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
[Singleton(typeof(IGuildedCommand), typeof(ItemsCommand))]
file interface ICommandsProviderModule
{
}