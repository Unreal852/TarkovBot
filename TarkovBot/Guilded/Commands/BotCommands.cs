using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using Guilded.Commands;
using TarkovBot.EFT.Data;
using TarkovBot.EFT.Data.Provider;
using TarkovBot.EFT.Data.Raw;
using TarkovBot.Extensions;
using TarkovBot.Guilded.Messages;
using TarkovBot.Guilded.Messages.Implementations;
using Task = System.Threading.Tasks.Task;

namespace TarkovBot.Guilded.Commands;

[SuppressMessage("Performance", "CA1806:Ne pas ignorer les résultats des méthodes")]
public class BotCommands : CommandModule
{
    public BotCommands(GuildedBot botClient)
    {
        BotClient = botClient;
    }

    public GuildedBot BotClient { get; }

    [Command("item", Aliases = new[] { "i" })]
    public async Task ItemSearchCommandAsync(CommandEvent commandEvent, [CommandParam] params string[] query)
    {
        string queryStr;
        if (query.Length > 1 && Enum.TryParse(query[0], out LanguageCode languageCode))
            queryStr = string.Join(' ', query, 1, query.Length - 1);
        else
        {
            queryStr = string.Join(' ', query);
            languageCode = LanguageCode.en;
        }

        ItemInfos[] items = DataProviders.ItemsProvider.FindByLocalizedName(languageCode, queryStr).ToArray();

        if (items is { Length: 0 })
        {
            await commandEvent.ReplyAsync($"No item found for '{queryStr}'", true);
            return;
        }

        if (items.Length > 1)
        {
            var embed = new Embed
            {
                    Title = "Your request is not precise enough !",
                    Author = new EmbedAuthor("Provided by tarkov.dev", "https://tarkov.dev/"),
                    Footer = new EmbedFooter("Expire in 5 minutes.")
            };

            var builder = new StringBuilder();
            builder.AppendLine("You can select one of the items below by reacting to this message.").AppendLine();
            int count = items.Length <= 11 ? items.Length : 11;
            for (var i = 0; i < count; i++)
                builder.AppendLine($"**{i}** - {items[i].GetLocalizedInfos(languageCode).Name}");
            embed.Description = builder.ToString();
            Message msg = await commandEvent.ReplyAsync(true, embeds: embed);
            MessagesManager.AddMessage(msg.Id, new ItemsMessageSelector(languageCode, commandEvent.Message, msg, items));
            for (var i = 0; i < count; i++)
                await msg.AddReactionAsync(EmotesConstants.SelectionEmotesIds[i]);
            return;
        }

        ItemInfos item = items[0];
        MessageContent messageContent = item.BuildMessageContent(languageCode);
        messageContent.ReplyMessageIds = new Collection<Guid> { commandEvent.Message.Id };
        Message message = await commandEvent.CreateMessageAsync(messageContent);
        await message.AppendReactions(item);
    }
}