// ReSharper disable IdentifierTypo

using Guilded;
using Guilded.Base.Events;
using Guilded.Base.Users;
using TarkovRatBot.Core.TarkovData;
using static TarkovRatBot.Core.TarkovCore;

namespace TarkovRatBot.Guilded;

public class GuildedBot
{
    public GuildedBot(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException($"The specified parameter {nameof(token)} is null or empty.");
        Token = token;
        Bot = new GuildedBotClient(token);
        Bot.Prepared.Subscribe(OnBotReady);
        Bot.MessageCreated.Subscribe(OnMessageReceived);
    }

    public  GuildedBotClient Bot   { get; }
    private string           Token { get; }

    public async Task Initialize()
    {
        WriteLine("Initializing Guilded Bot...", ConsoleColor.Yellow);
        await Bot.ConnectAsync();
    }

    private void OnBotReady(Me me)
    {
        WriteLine("Guilded Bot Initialized !", ConsoleColor.Green);
    }

    private async void OnMessageReceived(MessageEvent msg)
    {
        if (msg.Content.StartsWith("?p"))
        {
            string[] split = msg.Content.Split(' ', 2);
            if (split.Length != 2 || string.IsNullOrWhiteSpace(split[1]))
            {
                await msg.ReplyAsync("Wrong Arguments. ?price {item-name}", true);
                return;
            }

            Item[] result = await ItemsByNameQuery.ExecuteAs<Item[]>(split[1]);
            if (result is { Length: > 0 })
                foreach (Item itemInfo in result)
                        /*
                    Embed embed = new Embed
                    {
                            Title = $"{itemInfo.Name} ({itemInfo.ShortName})",
                            Thumbnail = new EmbedMedia(itemInfo.ImageLink),
                            Fields = new Collection<EmbedField>
                            {
                                    new("Base Price", itemInfo.BasePrice.ToString()),
                                    new("Lowest Price (24h)", itemInfo.Low24hPrice.ToString()),
                                    new("Average Price (24h)", itemInfo.Average24hPrice.ToString()),
                                    new("Highest Price (24h)", itemInfo.High24hPrice.ToString()),
                            },
                            Timestamp = itemInfo.Updated,
                            Footer = new EmbedFooter("Last Updated"),
                    }; */

                    await msg.ReplyAsync($"{itemInfo.Name} ({itemInfo.ShortName}): \n " +
                                         $"Base Price: {itemInfo.BasePrice}\n"          +
                                         $"Lowest Price: {itemInfo.Low24hPrice}\n"      +
                                         $"Average Price: {itemInfo.Average24hPrice}\n" +
                                         $"Highest Price: {itemInfo.High24hPrice}\n"    +
                                         $"Last Updated {itemInfo.Updated:g}");
        }
    }
}