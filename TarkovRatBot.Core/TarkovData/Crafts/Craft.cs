using System.Text.Json.Serialization;

namespace TarkovRatBot.Core.TarkovData.Crafts;

public class Craft
{
    [JsonPropertyName("id")]            public string             Id            { get; set; }
    [JsonPropertyName("source")]        public string             Source        { get; set; }
    [JsonPropertyName("sourceName")]    public string             SourceName    { get; set; }
    [JsonPropertyName("duration")]      public int                Duration      { get; set; }
    [JsonPropertyName("requiredItems")] public ContainedItem[]    RequiredItems { get; set; }
    [JsonPropertyName("rewardItems")]   public ContainedItem[]    RewardItems   { get; set; }
    [JsonPropertyName("requirements")]  public PriceRequirement[] Requirements  { get; set; }
}