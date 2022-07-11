using System.Diagnostics.CodeAnalysis;
using System.Text;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using Guilded.Commands;
using TarkovBot.Core;
using TarkovBot.Core.Data;
using TarkovBot.Guilded.Extensions;
using TarkovBot.Guilded.Messages.Implementations;
using Task = System.Threading.Tasks.Task;

namespace TarkovBot.Guilded.Commands;

[SuppressMessage("Performance", "CA1806:Ne pas ignorer les résultats des méthodes")]
public class BotCommands : CommandModule
{
    public BotCommands(GuildedBot bot)
    {
        Bot = bot;
    }

    public GuildedBot Bot { get; }

    [Command("item", Aliases = new[] { "i" })]
    public async Task ItemCommandAsync(CommandEvent commandEvent, [CommandParam] params string[] query)
    {
        string queryStr;
        if (query.Length > 1 && Enum.TryParse(query[0], out LanguageCode languageCode))
            queryStr = string.Join(' ', query, 1, query.Length - 1);
        else
        {
            queryStr = string.Join(' ', query);
            languageCode = LanguageCode.en;
        }

        Item[] items = TarkovCore.ItemsProvider.Where(languageCode,
                item => item.Name != null && item.Name.Contains(queryStr, StringComparison.CurrentCultureIgnoreCase)).ToArray();
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
                    Title = "Your request is not precise enough !",
                    Author = new EmbedAuthor("Provided by tarkov.dev", "https://tarkov.dev/"),
                    Footer = new EmbedFooter("Expire in 5 minutes.")
            };

            var builder = new StringBuilder();
            builder.AppendLine("You can select one of the belows items by reacting to this message.").AppendLine();
            int count = items.Length <= 11 ? items.Length : 11;
            for (var i = 0; i < count; i++)
                builder.AppendLine($"**{i}** . {items[i].Name}");
            embed.Description = builder.ToString();
            Message msg = await commandEvent.ReplyAsync(true, embeds: embed);
            Bot.MessagesManager.AddMessage(msg.Id, new ItemsMessageSelector(commandEvent.Message, msg, items));
            for (var i = 0; i < count; i++)
                await msg.AddReactionAsync(Constants.SelectionEmotesIds[i]);
            return;
        }

        MessageContent messageContent = items[0].BuildMessageContent();
        if (messageContent.Embeds is { Count: >= 1 })
        {
            foreach (Embed embed in messageContent.Embeds)
                await commandEvent.ReplyAsync(embeds: embed);
        }
    }
}