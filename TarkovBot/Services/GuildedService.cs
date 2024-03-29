﻿using Guilded;
using Guilded.Commands;
using Guilded.Connection;
using Guilded.Events;
using Serilog;
using TarkovBot.Embeds;
using TarkovBot.Services.Abstractions;
using Websocket.Client;

namespace TarkovBot.Services;

public sealed class GuildedService : IGuildedService
{
    private readonly ILogger                        _loggerService;
    private readonly IConfigService                 _configService;
    private readonly IGuildedMessageReactionService _guildedMessageReactionService;
    private readonly GuildedBotClient               _guilded;

    public GuildedService(ILogger loggerService, IConfigService configService,
                          IGuildedMessageReactionService guildedMessageReactionService,
                          IEnumerable<IGuildedCommand> commands)
    {
        _loggerService = loggerService;
        _configService = configService;
        _guildedMessageReactionService = guildedMessageReactionService;
        _guilded = new();
        _guilded.Connected.Subscribe(OnConnected);
        _guilded.Disconnected.Subscribe(OnDisconnected);

        foreach (var command in commands)
        {
            if (command is not CommandModule commandModule)
            {
                _loggerService.Warning("The command '{Command}' is not of type {CommandModuleType}",
                        command.GetType().Name, typeof(CommandModule));
                continue;
            }
#if DEBUG
            _guilded.AddCommands(commandModule, "t?");
#else
            _guilded.AddCommands(commandModule, _configService.Config.Prefix);
#endif
            _guilded.ServerAdded.Subscribe(OnServerAdded);
            _guilded.MessageReactionAdded.Subscribe(OnMessageReactionAdded);
            _loggerService.Information("Command '{Command}' registered", commandModule.GetType().Name);
        }
    }

    public Task ConnectAsync()
    {
        return _guilded.ConnectAsync(_configService.Config.Token);
    }

    public Task DisconnectAsync()
    {
        return _guilded.DisconnectAsync();
    }

    private void OnConnected(BaseGuildedConnection conn)
    {
        _loggerService.Information("Connected to guilded");
    }

    private void OnDisconnected(DisconnectionInfo info)
    {
        _loggerService.Warning("Disconnected from guilded ! Type: {DisconnectionType}", info.Type);
    }

    private void OnServerAdded(ServerAddedEvent e)
    {
        var defaultChannelId = e.Server.DefaultChannelId;
        if (defaultChannelId == null)
            return;
        var helpEmbed = new HelpEmbed("Thank you for adding me to your server !"       +
                                      "\nBelow, you will find all available commands." +
                                      "\nGood luck in the harsh world of Tarkov, I've got your back.");
        _guilded.CreateMessageAsync(defaultChannelId.Value, helpEmbed);
    }

    private void OnMessageReactionAdded(MessageReactionEvent e)
    {
        if (e.CreatedBy == e.ParentClient.Id)
            return;
        if (_guildedMessageReactionService.TryGet(e.MessageId, out var messageData))
        {
            messageData?.Callback(e, messageData);
        }
    }
}