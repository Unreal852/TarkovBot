using System.Text;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using Guilded.Commands;
using TarkovBot.Core;
using TarkovBot.Core.Data;
using TarkovBot.Guilded.Extensions;
using Task = System.Threading.Tasks.Task;

namespace TarkovBot.Guilded.Commands;

public class BotCommands : CommandModule
{
    public BotCommands()
    {
    }

    [Command("item", Aliases = new[] { "i" })]
    public async Task ItemCommandAsync(CommandEvent commandEvent, [CommandParam] string[] query)
    {
        string queryStr = string.Join(' ', query);
        Item[] items = await TarkovCore.ItemsProvider.QueryByName(queryStr, LanguageCode.en);
        //(await TarkovCore.ItemsQuery.ExecuteAs<Item[]>($"name: \"{queryStr}\"")).Where(i => !string.IsNullOrWhiteSpace(i.WikiLink)).ToArray();
        if (items is { Length: 0 })
        {
            await commandEvent.ReplyAsync($"No item found for '{queryStr}'", true);
            return;
        }

        items = items.Where(i => !string.IsNullOrWhiteSpace(i.WikiLink)).ToArray();

        if (items.Length > 1)
        {
            var embed = new Embed
            {
                    Title = "Your request is not precise enough, please refine your request.",
                    Author = new EmbedAuthor("Provided by tarkov.dev", "https://tarkov.dev/")
            };

            var builder = new StringBuilder();
            for (var i = 0; i < (items.Length <= 20 ? items.Length : 20); i++)
                builder.AppendLine($"- **{items[i].Name}**");
            embed.Description = builder.ToString();
            await commandEvent.ReplyAsync(true, embeds: embed);
            return;
        }

        MessageContent messageContent = items[0].BuildMessageContent();
        //messageContent.ReplyMessageIds = new Collection<Guid> { msg.Message.Id };
        if (messageContent.Embeds is { Count: >= 1 })
        {
            foreach (Embed embed in messageContent.Embeds)
                await commandEvent.ReplyAsync(embeds: embed);
        }
    }
}