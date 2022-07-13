// ReSharper disable MemberCanBePrivate.Global

namespace TarkovBot.Guilded;

//Todo: Some emotes should be changed when bots will be able to use custom emotes.
internal class Constants
{
    public const uint EmoteZeroId         = 90002198;
    public const uint EmoteOneId          = 90002199;
    public const uint EmoteTwoId          = 90002200;
    public const uint EmoteThreeId        = 90002201;
    public const uint EmoteFourId         = 90002202;
    public const uint EmoteFiveId         = 90002203;
    public const uint EmoteSixId          = 90002204;
    public const uint EmoteSevenId        = 90002205;
    public const uint EmoteEightId        = 90002206;
    public const uint EmoteNineId         = 90002207;
    public const uint EmoteTenId          = 90002208;
    public const uint EmoteLargeRedSquare = 90003276; // Ammo
    public const uint EmoteGrayQuestion   = 90002189; // Tasks

    public static readonly uint[] SelectionEmotesIds =
    {
            EmoteZeroId, EmoteOneId, EmoteTwoId,
            EmoteThreeId, EmoteFourId, EmoteFiveId,
            EmoteSixId, EmoteSevenId, EmoteEightId,
            EmoteNineId, EmoteTenId
    };
}