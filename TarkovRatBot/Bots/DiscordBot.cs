using Discord;
using Discord.Commands;
using Discord.WebSocket;
using TarkovRatBot.Extensions;
using TarkovRatBot.Tarkov;
using static Program;

namespace TarkovRatBot.Bots;

public class DiscordBot
{
    private const string SelectMenuItemChoiceId = "sel-item-name";
    private const string ButtonAmmoMoreInfos    = "btn-ammo-more-infos";

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
        Bot.Log += OnLogAsync;
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
            case SelectMenuItemChoiceId:
            {
                var result = await Queries.ItemsByIdsQuery.ExecuteAs<ItemInfo[]>(arg.Data.Values.FirstOrDefault());
                if (result is { Length: 0 })
                    return;
                await arg.Message.DeleteAsync();
                var embed = BuildItemEmbed(result[0]);
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
            case ButtonAmmoMoreInfos:
            {
                if (TarkovCache.AmmoCache.TryGetValue(split.Length == 2 ? split[1] : string.Empty, out AmmoInfo ammoInfo))
                {
                    await arg.RespondAsync(embed: BuildAmmoEmbed(ammoInfo));
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

            var result = await Queries.ItemsByNameQuery.ExecuteAs<ItemInfo[]>(split[1]);
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
                    var selectMenuBuilder = new SelectMenuBuilder { CustomId = SelectMenuItemChoiceId };
                    for (var i = 0; i < (result.Length < 20 ? result.Length : 20); i++)
                        selectMenuBuilder.AddOption(result[i].Name, result[i].Id);
                    rowBuilder.Components.Add(selectMenuBuilder.Build());
                    componentBuilder.AddRow(rowBuilder);
                    await msg.Channel.SendMessageAsync(embed: embedBuilder.Build(), components: componentBuilder.Build());
                    return;
                }

                var message = BuildItemEmbed(result[0]);

                await msg.Channel.SendMessageAsync(embed: message.Embed, components: message.Component);
            }
        }
    }

    private static (Embed Embed, MessageComponent Component) BuildItemEmbed(ItemInfo itemInfo)
    {
        ComponentBuilder componentBuilder = null;
        var embedBuilder = new EmbedBuilder
        {
                Title = $"{itemInfo.Name} ({itemInfo.ShortName})",
                Url = itemInfo.WikiLink,
                ThumbnailUrl = itemInfo.ImageLink,
                Footer = new EmbedFooterBuilder { Text = "Last Updated" },
                Timestamp = itemInfo.Updated,
                Author = new EmbedAuthorBuilder { Name = "Provided by tarkov.dev", Url = "https://tarkov.dev/" },
                Fields = new List<EmbedFieldBuilder>()
        };
        embedBuilder.AddField(new EmbedFieldBuilder { Name = "Base Price", Value = itemInfo.BasePrice ?? 0, IsInline = true });
        ItemPrice sellFor = itemInfo.SellFor.Where(s => s.Price is > 0).MaxBy(s => s.Price);
        ItemPrice buyFor = itemInfo.BuyFor.Where(s => s.Price is > 0).MinBy(s => s.Price);
        if (buyFor != null)
        {
            embedBuilder.AddField(new EmbedFieldBuilder
            {
                    Name = $"Buy From {buyFor.ItemSourceName.FirstCharToUpperCase()}",
                    Value = $"{buyFor.Price} {buyFor.Currency}", IsInline = true
            });
        }

        if (sellFor != null)
        {
            embedBuilder.AddField(new EmbedFieldBuilder
            {
                    Name = $"Sell To {sellFor.ItemSourceName.FirstCharToUpperCase()}",
                    Value = $"{sellFor.Price} {sellFor.Currency}", IsInline = true
            });
        }

        if (itemInfo.ItemTypes.Contains(EItemType.Ammo) && TarkovCache.AmmoCache.TryGetValue(itemInfo.Name, out AmmoInfo ammoInfo))
        {
            embedBuilder.Color = FromAmmoPenetration(ammoInfo);
            componentBuilder = new ComponentBuilder().WithButton("Ammo Infos", $"{ButtonAmmoMoreInfos}@{itemInfo.Name}");
        }

        return (embedBuilder.Build(), componentBuilder?.Build());
    }

    private static Embed BuildAmmoEmbed(AmmoInfo ammoInfo)
    {
        var embedBuilder = new EmbedBuilder
        {
                Title = $"{ammoInfo.Item.Name} ({ammoInfo.Item.ShortName})",
                Url = ammoInfo.Item.WikiLink,
                ThumbnailUrl = ammoInfo.Item.ImageLink,
                Footer = new EmbedFooterBuilder { Text = "Last Updated" },
                Timestamp = ammoInfo.Item.Updated,
                Author = new EmbedAuthorBuilder { Name = "Provided by tarkov.dev", Url = "https://tarkov.dev/" },
                Fields = new List<EmbedFieldBuilder>(),
                Color = FromAmmoPenetration(ammoInfo)
        };
        embedBuilder.AddField("Damages (Flesh)", ammoInfo.Damage                         ?? 0, true);
        embedBuilder.AddField("Penetration Power", ammoInfo.PenetrationPower             ?? 0, true);
        embedBuilder.AddField("Armor Damages", ammoInfo.ArmorDamage                      ?? 0, true);
        embedBuilder.AddField("Frag Chances", (int?)(ammoInfo.FragmentationChance * 100) ?? 0, true);
        embedBuilder.AddField("Armor Class", GetArmorClass(ammoInfo), true);
        return embedBuilder.Build();
    }

    private static Color FromAmmoPenetration(AmmoInfo ammoInfo)
    {
        return ammoInfo.PenetrationPower switch
        {
                > 60 => Color.Red,
                > 50 => Color.Orange,
                > 40 => Color.Purple,
                > 30 => Color.Blue,
                > 20 => Color.Green,
                _    => Color.LightGrey
        };
    }

    private static int GetArmorClass(AmmoInfo ammoInfo)
    {
        return ammoInfo.PenetrationPower switch
        {
                > 60 => 6,
                > 50 => 5,
                > 40 => 4,
                > 30 => 3,
                > 20 => 2,
                _    => 1
        };
    }
}