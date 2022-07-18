using Guilded;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using Guilded.Base.Events;
using TarkovBot.EFT.Data;
using TarkovBot.EFT.Data.Provider;
using TarkovBot.EFT.Data.Raw;
using TarkovBot.Extensions;
using Task = System.Threading.Tasks.Task;

namespace TarkovBot.Guilded.Reactions.Handlers;

public class AmmoReactionHandler : IMessageReactionHandler
{
    public async Task OnMessageReactionAdded(GuildedBotClient bot, MessageReactionEvent e)
    {
        Message message = await bot.GetMessageAsync(e.ChannelId, e.MessageId);
        EmbedFooter? footer = message.Embeds?[0].Footer;
        if (footer == null)
            return;
        (LanguageCode Languague, string Id) infos = footer.ParseInfos();
        ItemInfos? item = DataProviders.ItemsProvider.GetByKey(infos.Languague, infos.Id);
        if (item == null || !item.IsAmmo())
            return;
        MessageContent messageContent = item.BuildAmmoMessageContent()!;
        messageContent.ReplyMessageIds = message.ReplyMessageIds;
        await bot.CreateMessageAsync(e.ChannelId, messageContent);
    }
}