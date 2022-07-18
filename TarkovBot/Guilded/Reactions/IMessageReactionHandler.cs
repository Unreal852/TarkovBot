using Guilded;
using Guilded.Base.Events;

namespace TarkovBot.Guilded.Reactions;

public interface IMessageReactionHandler
{
    public Task OnMessageReactionAdded(GuildedBotClient botClient, MessageReactionEvent e);
}