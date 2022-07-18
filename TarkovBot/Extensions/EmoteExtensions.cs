using Guilded.Base.Content;
using TarkovBot.Guilded;

namespace TarkovBot.Extensions;

public static class EmoteExtensions
{
    public static int ToSelectorIndex(this Emote emote)
    {
        return emote.Id switch
        {
                EmotesConstants.EmoteZeroId  => 0,
                EmotesConstants.EmoteOneId   => 1,
                EmotesConstants.EmoteTwoId   => 2,
                EmotesConstants.EmoteThreeId => 3,
                EmotesConstants.EmoteFourId  => 4,
                EmotesConstants.EmoteFiveId  => 5,
                EmotesConstants.EmoteSixId   => 6,
                EmotesConstants.EmoteSevenId => 7,
                EmotesConstants.EmoteEightId => 8,
                EmotesConstants.EmoteNineId  => 9,
                EmotesConstants.EmoteTenId   => 10,
                _                            => throw new ArgumentOutOfRangeException()
        };
    }
}