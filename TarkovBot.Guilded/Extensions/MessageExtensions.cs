using Guilded.Base.Content;
using TarkovBot.Core.Data;
using TarkovBot.Core.Extensions;
using Task = System.Threading.Tasks.Task;

namespace TarkovBot.Guilded.Extensions;

public static class MessageExtensions
{
    public static async Task AppendReactions(this Message message, Item item)
    {
        if (item.IsAmmo())
            await message.AddReactionAsync(Constants.EmoteLargeRedSquare);
        if (item.UsedInTasks is { Length: > 0 })
            await message.AddReactionAsync(Constants.EmoteGrayQuestion);
    }
}