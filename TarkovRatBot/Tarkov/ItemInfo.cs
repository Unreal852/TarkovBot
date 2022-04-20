using System.Text.Json.Serialization;

// ReSharper disable ClassNeverInstantiated.Global

namespace TarkovRatBot.Tarkov;

public class ItemInfo
{
    [JsonPropertyName("id")]           public string      Id              { get; set; }
    [JsonPropertyName("name")]         public string      Name            { get; set; }
    [JsonPropertyName("shortName")]    public string      ShortName       { get; set; }
    [JsonPropertyName("wikiLink")]     public string      WikiLink        { get; set; }
    [JsonPropertyName("imageLink")]    public string      ImageLink       { get; set; }
    [JsonPropertyName("updated")]      public DateTime    Updated         { get; set; }
    [JsonPropertyName("basePrice")]    public int?        BasePrice       { get; set; }
    [JsonPropertyName("low24hPrice")]  public int?        Low24hPrice     { get; set; }
    [JsonPropertyName("avg24hPrice")]  public int?        Average24hPrice { get; set; }
    [JsonPropertyName("high24hPrice")] public int?        High24hPrice    { get; set; }
    [JsonPropertyName("buyFor")]       public ItemPrice[] BuyFor          { get; set; }
    [JsonPropertyName("sellFor")]      public ItemPrice[] SellFor         { get; set; }
}