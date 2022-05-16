using Discord;
using Discord.WebSocket;
using TarkovRatBot.Core.TarkovData;
using TarkovRatBot.Core.TarkovData.Ammos;
using TarkovRatBot.Discord.Extensions;
using static TarkovRatBot.Core.TarkovCore;

namespace TarkovRatBot.Discord;

public class DiscordBot
{
    public DiscordBot()
    {
        Bot = new DiscordSocketClient();
        Bot.Ready += OnBotReady;
        Bot.SlashCommandExecuted += OnSlashCommandExecuted;
        Bot.SelectMenuExecuted += OnSelectMenuExecuted;
        Bot.ButtonExecuted += OnButtonExecuted;
        Bot.Log += BotOnLog;
    }

    private Task BotOnLog(LogMessage arg)
    {
        WriteLine(arg.Message);
        if (arg.Exception != null)
        {
            WriteLine(arg.Exception.Message);
            WriteLine(arg.Exception.StackTrace);
        }

        return Task.CompletedTask;
    }

    public DiscordSocketClient Bot { get; }

    public async Task Initialize(string token)
    {
        WriteLine("Initializing Discord Bot...", ConsoleColor.Yellow);
        if (string.IsNullOrWhiteSpace(token))
        {
            WriteLine("Failed to initialize discord bot. Missing Token.", ConsoleColor.Red);
            return;
        }

        await Bot.LoginAsync(TokenType.Bot, token);
        await Bot.StartAsync();
    }

    private async Task OnBotReady()
    {
        try
        {
            var commandBuilder = new SlashCommandBuilder();
            commandBuilder.WithName("price").WithDescription("Find item price")
                          .AddOption("name", ApplicationCommandOptionType.String, "The name of the item to find", true);
            //await Bot.CreateGlobalApplicationCommandAsync(commandBuilder.Build());
            SocketGuild guild = Bot.Guilds.First();
            foreach (SocketApplicationCommand socketApplicationCommand in await Bot.GetGlobalApplicationCommandsAsync())
                await socketApplicationCommand.DeleteAsync();

            foreach (SocketApplicationCommand socketApplicationCommand in await guild.GetApplicationCommandsAsync()) await socketApplicationCommand.DeleteAsync();

            await guild.CreateApplicationCommandAsync(commandBuilder.Build());
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message + Environment.NewLine + ex.StackTrace, ConsoleColor.Red);
        }

        WriteLine("Discord Bot Initialized !", ConsoleColor.Green);
    }

    private async Task OnSlashCommandExecuted(SocketSlashCommand cmd)
    {
        switch (cmd.Data.Name)
        {
            case Consts.CommandPrice:
                await OnPriceCommandExecuted(cmd);
                break;
        }
    }

    private async Task OnSelectMenuExecuted(SocketMessageComponent arg)
    {
        switch (arg.Data.CustomId)
        {
            case Consts.SelectMenuItemChoiceId:
            {
                Item[] result = await ItemsByIdsQuery.ExecuteAs<Item[]>(arg.Data.Values.FirstOrDefault());
                if (result is { Length: 0 })
                    return;
                (Embed embed, MessageComponent component) = result[0].BuildItemEmbed();
                await arg.DeferAsync();
                await arg.ModifyOriginalResponseAsync(properties =>
                {
                    properties.Embed = embed;
                    properties.Components = component;
                });
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
                    await arg.RespondAsync(embed: ammoInfo.BuildAmmoEmbed());

                break;
            }
        }
    }

    private async Task OnPriceCommandExecuted(SocketSlashCommand cmd)
    {
        var itemName = cmd.Data.Options.First().Value.ToString();
        if (string.IsNullOrWhiteSpace(itemName))
        {
            await cmd.RespondAsync("Missing item name.", ephemeral: true);
            return;
        }

        Item[] result = await ItemsByNameQuery.ExecuteAs<Item[]>(itemName);
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
                await cmd.RespondAsync(embed: embedBuilder.Build(), components: componentBuilder.Build());
                return;
            }

            (Embed Embed, MessageComponent Component) message = result[0].BuildItemEmbed();
            await cmd.RespondAsync(embed: message.Embed, components: message.Component);
        }
    }
}