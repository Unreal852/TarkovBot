using Guilded;
using Guilded.Base.Events;
using TarkovBot.Guilded.Reactions.Handlers;

namespace TarkovBot.Guilded.Reactions;

public static class ReactionsManager
{
    private static readonly Dictionary<uint, IMessageReactionHandler> EmoteReactionsHandlers = new();

    static ReactionsManager()
    {
        EmoteReactionsHandlers.Add(Constants.EmoteLargeRedSquare, new AmmoReactionHandler());
        foreach (uint emotesId in Constants.SelectionEmotesIds)
            EmoteReactionsHandlers.Add(emotesId, new ItemSelectorReactionHandler());
    }

    public static Task HandleReaction(uint emoteId, GuildedBotClient botClient, MessageReactionEvent e)
    {
        return !EmoteReactionsHandlers.ContainsKey(emoteId)
                ? Task.CompletedTask
                : EmoteReactionsHandlers[emoteId].OnMessageReactionAdded(botClient, e);
    }
}