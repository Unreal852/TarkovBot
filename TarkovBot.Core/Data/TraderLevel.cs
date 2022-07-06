using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class TraderLevel
{
    [JsonPropertyName("id")]                   public string            Id                   { get; set; }
    [JsonPropertyName("level")]                public int               Level                { get; set; }
    [JsonPropertyName("requiredPlayerLevel")]  public int               RequiredPlayerLevel  { get; set; }
    [JsonPropertyName("requiredReputation")]   public float             RequiredReputation   { get; set; }
    [JsonPropertyName("requiredCommerce")]     public int               RequiredCommerce     { get; set; }
    [JsonPropertyName("payRate")]              public float             PayRate              { get; set; }
    [JsonPropertyName("insuranceRate")]        public float?            InsuranceRate        { get; set; }
    [JsonPropertyName("repairCostMultiplier")] public float?            RepairCostMultiplier { get; set; }
    [JsonPropertyName("barters")]              public Barter[]          Barters              { get; set; }
    [JsonPropertyName("cashOffers")]           public TraderCashOffer[] CashOffers           { get; set; }
}