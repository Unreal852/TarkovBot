using Guilded;
using Guilded.Base.Events;
using TarkovBot.Guilded.Reactions.Handlers;

namespace TarkovBot.Guilded.Reactions;

public static class ReactionsManager
{
    private static readonly Dictionary<uint, IMessageReactionHandler> ReactionsHandlers = new();

    static ReactionsManager()
    {
        ReactionsHandlers.Add(EmotesConstants.EmoteLargeRedSquare, new AmmoReactionHandler());
        ReactionsHandlers.Add(EmotesConstants.EmoteGrayQuestion, new ItemUsedInTasksReactionHandler());
        foreach (uint emotesId in EmotesConstants.SelectionEmotesIds)
            ReactionsHandlers.Add(emotesId, new ItemSelectorReactionHandler());
    }

    public static Task HandleReaction(uint emoteId, GuildedBotClient botClient, MessageReactionEvent e)
    {
        return !ReactionsHandlers.ContainsKey(emoteId)
                ? Task.CompletedTask
                : ReactionsHandlers[emoteId].OnMessageReactionAdded(botClient, e);
    }
}