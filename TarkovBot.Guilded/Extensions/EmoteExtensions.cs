using Guilded.Base.Content;

namespace TarkovBot.Guilded.Extensions;

public static class EmoteExtensions
{
    public static int ToSelectorIndex(this Emote emote)
    {
        return emote.Id switch
        {
                Constants.EmoteZeroId  => 0,
                Constants.EmoteOneId   => 1,
                Constants.EmoteTwoId   => 2,
                Constants.EmoteThreeId => 3,
                Constants.EmoteFourId  => 4,
                Constants.EmoteFiveId  => 5,
                Constants.EmoteSixId   => 6,
                Constants.EmoteSevenId => 7,
                Constants.EmoteEightId => 8,
                Constants.EmoteNineId  => 9,
                Constants.EmoteTenId   => 10,
                _                      => throw new ArgumentOutOfRangeException()
        };
    }
}