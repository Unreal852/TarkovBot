using Guilded;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using Guilded.Base.Events;
using TarkovBot.Core;
using TarkovBot.Core.Data;
using TarkovBot.Guilded.Extensions;
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
        Ammo? ammo = TarkovCore.AmmoProvider.GetByKey(footer.Text);
        if (ammo == null)
            return;
        MessageContent messageContent = ammo.BuildMessageContent(TarkovCore.ItemsProvider.GetByKey(LanguageCode.en, footer.Text)!);
        messageContent.ReplyMessageIds = message.ReplyMessageIds;
        await bot.CreateMessageAsync(e.ChannelId, messageContent);
    }
}