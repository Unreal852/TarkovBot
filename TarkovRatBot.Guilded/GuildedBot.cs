// ReSharper disable IdentifierTypo

using System.Text;
using Guilded;
using Guilded.Base.Embeds;
using Guilded.Base.Events;
using Guilded.Base.Users;
using TarkovRatBot.Core.TarkovData;
using TarkovRatBot.Core.TarkovData.Ammos;
using TarkovRatBot.Guilded.Extensions;
using static TarkovRatBot.Core.TarkovCore;

namespace TarkovRatBot.Guilded;

public class GuildedBot
{
    public GuildedBot()
    {
        Bot = new GuildedBotClient();
        Bot.Prepared.Subscribe(OnBotReady);
        Bot.MessageCreated.Subscribe(OnMessageReceived);
    }

    public GuildedBotClient Bot { get; }

    public async Task Initialize(string token)
    {
        WriteLine("Initializing Guilded Bot...", ConsoleColor.Yellow);
        if (string.IsNullOrWhiteSpace(token))
        {
            WriteLine("Failed to initialize guilded bot. Missing Token.", ConsoleColor.Red);
            return;
        }

        await Bot.ConnectAsync(token);
    }

    private void OnBotReady(Me me)
    {
        WriteLine("Guilded Bot Initialized !", ConsoleColor.Green);
    }

    private async void OnMessageReceived(MessageEvent msg)
    {
        if (msg.Content.StartsWith("?p"))
            await OnPriceCommand(msg);
        if (msg.Content.StartsWith("?a"))
            await OnAmmoCommand(msg);
    }

    private async Task OnPriceCommand(MessageEvent msg)
    {
        string[] split = msg.Content.Split(' ', 2);
        if (split.Length != 2 || string.IsNullOrWhiteSpace(split[1]))
        {
            await msg.ReplyAsync("Wrong Arguments. ?p {item-name}", true);
            return;
        }

        // Exclude items without wiki link. 
        Item[] result = (await ItemsByNameQuery.ExecuteAs<Item[]>(split[1])).Where(i => !string.IsNullOrWhiteSpace(i.WikiLink)).ToArray();
        if (result is { Length: 0 })
        {
            await msg.ReplyAsync($"No items found for '{split[1]}'", true);
            return;
        }

        if (result.Length > 1)
        {
            var embed = new Embed
            {
                    Title = "Your request is not precise enough, please refine your request.",
                    Author = new EmbedAuthor("Provided by tarkov.dev", "https://tarkov.dev/")
            };

            var builder = new StringBuilder();
            for (var i = 0; i < (result.Length <= 20 ? result.Length : 20); i++)
                builder.AppendLine($"- **{result[i].Name}**");
            embed.Description = builder.ToString();
            await msg.ReplyAsync(true, embeds: embed);
            return;
        }

        foreach (Embed embed in result[0].BuildMessageContent())
        {
            if (embed == null)
                continue;
            await msg.ReplyAsync(embeds: embed);
        }
    }

    private async Task OnAmmoCommand(MessageEvent msg)
    {
        string[] split = msg.Content.Split(' ', 2);
        if (split.Length != 2 || string.IsNullOrWhiteSpace(split[1]))
        {
            await msg.ReplyAsync("Wrong Arguments. ?a {ammo-name}", true);
            return;
        }

        Ammo[] ammos = AmmoCache.Where(a => a.Item.Name.Contains(split[1])).ToArray();
        if (ammos is { Length: 0 })
        {
            await msg.ReplyAsync($"No ammo found for '{split[1]}'", true);
            return;
        }

        if (ammos.Length > 1)
        {
            var embed = new Embed
            {
                    Title = "Your request is not precise enough, please refine your request.",
                    Author = new EmbedAuthor("Provided by tarkov.dev", "https://tarkov.dev/")
            };

            var builder = new StringBuilder();
            for (var i = 0; i < (ammos.Length <= 20 ? ammos.Length : 20); i++)
                builder.AppendLine($"- **{ammos[i].Item.Name}**");
            embed.Description = builder.ToString();
            await msg.ReplyAsync(true, embeds: embed);
            return;
        }

        foreach (Embed embed in ammos[0].Item.BuildMessageContent())
        {
            if (embed == null)
                continue;
            await msg.ReplyAsync(embeds: embed);
        }
    }
}