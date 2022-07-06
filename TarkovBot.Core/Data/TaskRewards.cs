using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class TaskRewards
{
    [JsonPropertyName("traderStanding")]   public TraderStanding[] TraderStanding   { get; set; }
    [JsonPropertyName("items")]            public ContainedItem[]  Items            { get; set; }
    [JsonPropertyName("offerUnlock")]      public OfferUnlock[]    OfferUnlock      { get; set; }
    [JsonPropertyName("skillLevelReward")] public SkillLevel[]     SkillLevelReward { get; set; }
    [JsonPropertyName("traderUnlock")]     public Trader[]         TraderUnlock     { get; set; }
}