// ReSharper disable IdentifierTypo

using System.Text;
using Guilded;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using Guilded.Base.Events;
using Guilded.Base.Users;
using TarkovRatBot.Core.TarkovData;
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
        if (msg.Content is not { })
            return;
        if (msg.Content.StartsWith("!p"))
            await OnPriceCommand(msg);
    }

    private async Task OnPriceCommand(MessageEvent msg)
    {
        string[] split = msg.Content!.Split(' ', 2);
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

        MessageContent messageContent = result[0].BuildMessageContent();
        //messageContent.ReplyMessageIds = new Collection<Guid> { msg.Message.Id };
        if (messageContent.Embeds is { Count: >= 1 })
        {
            foreach (Embed embed in messageContent.Embeds) await msg.ReplyAsync(embed);
        }
    }
}