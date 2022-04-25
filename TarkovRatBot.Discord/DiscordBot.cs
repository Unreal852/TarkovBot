using Discord;
using Discord.Commands;
using Discord.WebSocket;
using TarkovRatBot.Core;
using TarkovRatBot.Core.Extensions;
using TarkovRatBot.Core.TarkovData;
using TarkovRatBot.Core.TarkovData.Ammos;
using TarkovRatBot.Discord.Extensions;
using static TarkovRatBot.Core.TarkovCore;

namespace TarkovRatBot.Discord;

public class DiscordBot
{
    public DiscordBot(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException($"The specified parameter {nameof(token)} is null or empty.");
        Token = token;
        Bot = new();
        Bot.Ready += OnBotReady;
        Bot.MessageReceived += OnMessageReceived;
        Bot.SelectMenuExecuted += OnSelectMenuExecuted;
        Bot.ButtonExecuted += OnButtonExecuted;
        //Bot.Log += OnLogAsync;
    }

    public  DiscordSocketClient Bot   { get; }
    private string              Token { get; }

    public async Task Initialize()
    {
        WriteLine("Initializing Discord Bot...", ConsoleColor.Yellow);
        await Bot.LoginAsync(TokenType.Bot, Token);
        await Bot.StartAsync();
    }

    private Task OnBotReady()
    {
        WriteLine("Discord Bot Initialized !", ConsoleColor.Green);
        return Task.CompletedTask;
    }

    private Task OnLogAsync(LogMessage message)
    {
        if (message.Exception is CommandException cmdException)
        {
            Console.WriteLine($"[Command/{message.Severity}] {cmdException.Command.Aliases.First()}"
                            + $" failed to execute in {cmdException.Context.Channel}.");
            Console.WriteLine(cmdException);
        }
        else
            Console.WriteLine($"[General/{message.Severity}] {message}");

        return Task.CompletedTask;
    }

    private async Task OnSelectMenuExecuted(SocketMessageComponent arg)
    {
        switch (arg.Data.CustomId)
        {
            case Consts.SelectMenuItemChoiceId:
            {
                var result = await TarkovCore.ItemsByIdsQuery.ExecuteAs<Item[]>(arg.Data.Values.FirstOrDefault());
                if (result is { Length: 0 })
                    return;
                await arg.Message.DeleteAsync();
                var embed = result[0].BuildItemEmbed();
                await arg.Message.Channel.SendMessageAsync(embed: embed.Embed, components: embed.Component);
                break;
            }
        }
    }

    private async Task OnButtonExecuted(SocketMessageComponent arg)
    {
        string[] split = arg.Data.CustomId.Split('@');
        switch (split[0])
        {
            case Consts.ButtonAmmoMoreInfosId:
            {
                if (AmmoCache.Cache.TryGetValue(split.Length == 2 ? split[1] : string.Empty, out Ammo ammoInfo))
                {
                    await arg.RespondAsync(embed: ammoInfo.BuildAmmoEmbed());
                }

                break;
            }
        }
    }

    private async Task OnMessageReceived(SocketMessage msg)
    {
        if (msg.Author.IsBot)
            return;
        if (msg.Content.StartsWith("?p"))
        {
            string[] split = msg.Content.Split(' ', 2);
            if (split.Length != 2 || string.IsNullOrWhiteSpace(split[1]))
            {
                await msg.Channel.SendMessageAsync("Wrong Arguments. ?price {item-name}", messageReference: msg.Reference);
                return;
            }

            var result = await ItemsByNameQuery.ExecuteAs<Item[]>(split[1]);
            if (result is { Length: > 0 })
            {
                if (result.Length > 1)
                {
                    var embedBuilder = new EmbedBuilder
                    {
                            Title = "Your request is not precise enough, select an item from the list (First 20 items) below or refine your request."
                    };

                    var componentBuilder = new ComponentBuilder();
                    var rowBuilder = new ActionRowBuilder();
                    var selectMenuBuilder = new SelectMenuBuilder { CustomId = Consts.SelectMenuItemChoiceId };
                    for (var i = 0; i < (result.Length < 20 ? result.Length : 20); i++)
                        selectMenuBuilder.AddOption(result[i].Name, result[i].Id);
                    rowBuilder.Components.Add(selectMenuBuilder.Build());
                    componentBuilder.AddRow(rowBuilder);
                    await msg.Channel.SendMessageAsync(embed: embedBuilder.Build(), components: componentBuilder.Build());
                    return;
                }

                var message = result[0].BuildItemEmbed();

                await msg.Channel.SendMessageAsync(embed: message.Embed, components: message.Component);
            }
        }
    }
}