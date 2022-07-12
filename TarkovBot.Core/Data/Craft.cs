using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class Craft : IIdentifiable
{
    [JsonPropertyName("id")]            public string          Id            { get; set; }
    [JsonPropertyName("station")]       public HideoutStation  Station       { get; set; }
    [JsonPropertyName("level")]         public int             Level         { get; set; }
    [JsonPropertyName("duration")]      public int             Duration      { get; set; }
    [JsonPropertyName("requiredItems")] public ContainedItem[] RequiredItems { get; set; }
    [JsonPropertyName("rewardItems")]   public ContainedItem[] RewardItems   { get; set; }
}