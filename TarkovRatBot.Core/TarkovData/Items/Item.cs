using System.Text.Json.Serialization;
using TarkovRatBot.Core.GraphQL.Attributes;

// ReSharper disable ClassNeverInstantiated.Global

namespace TarkovRatBot.Core.TarkovData.Items;

[GraphQl("items")]
public class Item
{
    [JsonPropertyName("id")]              public string      Id              { get; set; }
    [JsonPropertyName("backgroundColor")] public string      BackgroundColor { get; set; }
    [JsonPropertyName("name")]            public string?     Name            { get; set; }
    [JsonPropertyName("normalizedName")]  public string?     Normalizedname  { get; set; }
    [JsonPropertyName("shortName")]       public string?     ShortName       { get; set; }
    [JsonPropertyName("updated")]         public DateTime?   Updated         { get; set; }
    [JsonPropertyName("wikiLink")]        public string?     WikiLink        { get; set; }
    [JsonPropertyName("gridImageLink")]   public string?     GridImageLink   { get; set; }
    [JsonPropertyName("width")]           public int         Width           { get; set; }
    [JsonPropertyName("height")]          public int         Height          { get; set; }
    [JsonPropertyName("loudness")]        public int         Loudness        { get; set; }
    [JsonPropertyName("basePrice")]       public int         BasePrice       { get; set; }
    [JsonPropertyName("lastLowPrice")]    public int?        LastLowPrice    { get; set; }
    [JsonPropertyName("fleaMarketFee")]   public int?        FleaMarketFee   { get; set; }
    [JsonPropertyName("weight")]          public float?      Weight          { get; set; }
    [JsonPropertyName("types")]           public ItemType[]  Types           { get; set; }
    [JsonPropertyName("sellFor")]         public ItemPrice[] SellFor         { get; set; }
    [JsonPropertyName("buyFor")]          public ItemPrice[] BuyFor          { get; set; }
    [JsonPropertyName("bartersFor")]      public IdOnly[]    BartersFor      { get; set; }
    [JsonPropertyName("bartersUsing")]    public IdOnly[]    BartersUsing    { get; set; }
    [JsonPropertyName("craftsFor")]       public IdOnly[]    CraftsFor       { get; set; }
    [JsonPropertyName("craftsUsing")]     public IdOnly[]    CraftsUsing     { get; set; }
}