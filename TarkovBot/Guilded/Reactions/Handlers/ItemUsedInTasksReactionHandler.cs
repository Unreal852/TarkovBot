using Guilded;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using Guilded.Base.Events;
using TarkovBot.EFT.Data;
using TarkovBot.EFT.Data.Provider;
using TarkovBot.EFT.Data.Raw;
using Task = TarkovBot.EFT.Data.Raw.Task;

namespace TarkovBot.Guilded.Reactions.Handlers;

public class ItemUsedInTasksReactionHandler : IMessageReactionHandler
{
    public async System.Threading.Tasks.Task OnMessageReactionAdded(GuildedBotClient bot, MessageReactionEvent e)
    {
        Message originalMessage = await bot.GetMessageAsync(e.ChannelId, e.MessageId);
        if (originalMessage.Embeds == null || originalMessage.Embeds.Count == 0)
            return;
        Embed originalEmbed = originalMessage.Embeds[0];
        EmbedFooter? footer = originalEmbed.Footer;
        if (footer == null)
            return;
        ItemInfos? item = DataProviders.ItemsProvider.GetByKey(LanguageCode.en, footer.Text);
        if (item == null || item.Item.UsedInTasks is { Length: 0 })
            return;

        var messageContent = new MessageContent
        {
                Embeds = originalMessage.Embeds,
                ReplyMessageIds = originalMessage.ReplyMessageIds
        };

        originalEmbed.Fields = new List<EmbedField>();

        int maxFields = item.Item.UsedInTasks.Length < 25 ? item.Item.UsedInTasks.Length : 25;
        for (int i = 0; i < maxFields; i++)
        {
            IdOnly id = item.Item.UsedInTasks[i];
            Task? task = DataProviders.TasksProvider.GetByKey(LanguageCode.en, id.Id);
            TaskObjective? objective = task?.Objectives.FirstOrDefault(o => o.Type == TaskType.findItem && o.Item?.Id == item.Id);
            if (task == null || objective == null || objective.Count == 0)
                continue;
            originalEmbed.AddField(task.Name, $"{objective.Description}\n*({objective.Count} required)* Minimum Level {task.MinPlayerLevel}");
        }

        await bot.CreateMessageAsync(e.ChannelId, messageContent);
    }
}