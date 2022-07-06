using System.Text;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using Guilded.Commands;
using TarkovBot.Core;
using TarkovBot.Core.TarkovData;
using TarkovBot.Core.TarkovData.Items;
using TarkovBot.Guilded.Extensions;

namespace TarkovBot.Guilded.Commands;

public class BotCommands : CommandModule
{
    public BotCommands()
    {
    }

    [Command("search", Aliases = new[] { "s" })]
    public async Task SearchCommandAsync(CommandEvent commandEvent, [CommandParam] string[] query)
    {
        string queryStr = string.Join(' ', query);
        Item[] result = (await TarkovCore.ItemsQuery.ExecuteAs<Item[]>($"name: \"{queryStr}\"")).Where(i => !string.IsNullOrWhiteSpace(i.WikiLink)).ToArray();
        if (result is { Length: 0 })
        {
            await commandEvent.ReplyAsync($"No item found for '{queryStr}'", true);
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
            await commandEvent.ReplyAsync(true, embeds: embed);
            return;
        }

        MessageContent messageContent = result[0].BuildMessageContent();
        //messageContent.ReplyMessageIds = new Collection<Guid> { msg.Message.Id };
        if (messageContent.Embeds is { Count: >= 1 })
        {
            foreach (Embed embed in messageContent.Embeds)
                await commandEvent.ReplyAsync(embeds: embed);
        }
    }
}