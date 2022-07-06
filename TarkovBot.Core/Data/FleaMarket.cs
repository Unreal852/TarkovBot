using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class FleaMarket
{
    [JsonPropertyName("name")]                   public string                      Name                   { get; set; }
    [JsonPropertyName("minPlayerLevel")]         public int                         MinPlayerLevel         { get; set; }
    [JsonPropertyName("enabled")]                public bool                        Enabled                { get; set; }
    [JsonPropertyName("sellOfferFeeRate")]       public float                       SellOfferFeeRate       { get; set; }
    [JsonPropertyName("sellRequirementFeeRate")] public float                       SellRequirementFeeRate { get; set; }
    [JsonPropertyName("reputationLevels")]       public FleaMarketReputationLevel[] ReputationLevels       { get; set; }
}