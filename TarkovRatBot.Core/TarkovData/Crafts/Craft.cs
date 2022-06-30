using System.Text.Json.Serialization;
using TarkovRatBot.Core.GraphQL.Attributes;

// ReSharper disable ClassNeverInstantiated.Global

namespace TarkovRatBot.Core.TarkovData.Crafts;

[GraphQl("crafts")]
public class Craft
{
    [JsonPropertyName("id")]            public string          Id            { get; set; }
    [JsonPropertyName("level")]         public int             Level         { get; set; }
    [JsonPropertyName("duration")]      public int             Duration      { get; set; }
    [JsonPropertyName("station")]       public IdOnly          Station       { get; set; }
    [JsonPropertyName("requiredItems")] public ContainedItem[] RequiredItems { get; set; }
    [JsonPropertyName("rewardItems")]   public ContainedItem[] RewardItems   { get; set; }
}