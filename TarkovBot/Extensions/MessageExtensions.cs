using Guilded.Base.Content;
using TarkovBot.EFT.Data;
using TarkovBot.Guilded;

namespace TarkovBot.Extensions;

public static class MessageExtensions
{
    public static async Task AppendReactions(this Message message, ItemInfos item)
    {
        if (item.IsAmmo())
            await message.AddReactionAsync(EmotesConstants.EmoteLargeRedSquare);
        if (item.Item.UsedInTasks is { Length: > 0 })
            await message.AddReactionAsync(EmotesConstants.EmoteGrayQuestion);
    }
}