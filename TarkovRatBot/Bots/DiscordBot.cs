using Discord;
using Discord.WebSocket;
using TarkovRatBot.Tarkov;
using static Program;

namespace TarkovRatBot.Bots;

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

    private async Task OnMessageReceived(SocketMessage msg)
    {
        if (msg.Author.IsWebhook || msg.Author.IsBot)
            return;
        if (msg.Content.StartsWith("?price"))
        {
            string[] split = msg.Content.Split(' ', 2);
            if (split.Length != 2 || string.IsNullOrWhiteSpace(split[1]))
            {
                await msg.Channel.SendMessageAsync("Wrong Arguments. ?price {item-name}", messageReference: msg.Reference);
                return;
            }

            var result = await ItemsByNameQuery.ExecuteAs<DummyData>(split[1]);
            if (result is { Data.ItemsByName.Length: > 0 })
            {
                foreach (ItemInfo itemInfo in result.Data.ItemsByName)
                {
                    var p = itemInfo.SellFor.Where(s => s.Price is > 0).OrderByDescending(s => s.Price).First();
                    EmbedBuilder builder = new EmbedBuilder
                    {
                            Title = $"{itemInfo.Name} ({itemInfo.ShortName})",
                            Url = itemInfo.WikiLink,
                            ThumbnailUrl = itemInfo.ImageLink,
                            Footer = new EmbedFooterBuilder { Text = "Last Updated" },
                            Timestamp = itemInfo.Updated,
                            Author = new EmbedAuthorBuilder { Name = "Provided by tarkov.dev", Url = "https://tarkov.dev/" },
                    };
                    builder.Fields = new List<EmbedFieldBuilder>
                    {
                            new() { Name = "Base Price", Value = itemInfo.BasePrice ?? 0, IsInline = true },
                            new()
                            {
                                    Name = "LOW | AVG | HIGH Price (24h)",
                                    Value = $"{itemInfo.Low24hPrice ?? 0} | {itemInfo.Average24hPrice ?? 0} | {itemInfo.High24hPrice ?? 0}", IsInline = true
                            },
                            new() { Name = $"Sell To {p.ItemSourceName}", Value = $"{p.Price} {p.Currency}", IsInline = true },
                    };
                    await msg.Channel.SendMessageAsync(embed: builder.Build(), messageReference: msg.Reference);
                }
            }
        }
    }
}