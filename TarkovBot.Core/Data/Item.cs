using System.Text.Json.Serialization;
using TarkovBot.Core.GraphQL.Attributes;

namespace TarkovBot.Core.Data;

[GraphQl("items")]
public class Item : IIdentifiable
{
    [JsonPropertyName("id")]                   public string        Id                   { get; set; }
    [JsonPropertyName("name")]                 public string?       Name                 { get; set; }
    [JsonPropertyName("normalizedName")]       public string?       NormalizedName       { get; set; }
    [JsonPropertyName("shortName")]            public string?       ShortName            { get; set; }
    [JsonPropertyName("basePrice")]            public int           BasePrice            { get; set; }
    [JsonPropertyName("updated")]              public DateTime?     Updated              { get; set; }
    [JsonPropertyName("width")]                public int           Width                { get; set; }
    [JsonPropertyName("height")]               public int           Height               { get; set; }
    [JsonPropertyName("backgroundColor")]      public string        BackgroundColor      { get; set; }
    [JsonPropertyName("iconLink")]             public string?       IconLink             { get; set; }
    [JsonPropertyName("wikiLink")]             public string?       WikiLink             { get; set; }
    [JsonPropertyName("gridImageLink")]        public string?       GridImageLink        { get; set; }
    [JsonPropertyName("types")]                public ItemType[]    Types                { get; set; }
    [JsonPropertyName("avg24hPrice")]          public int?          Avg24hPrice          { get; set; }
    [JsonPropertyName("accuracyModifier")]     public float?        AccuracyModifier     { get; set; }
    [JsonPropertyName("recoilModifier")]       public float?        RecoilModifier       { get; set; }
    [JsonPropertyName("ergonomicsModifier")]   public float?        ErgonomicsModifier   { get; set; }
    [JsonPropertyName("hasGrid")]              public bool?         HasGrid              { get; set; }
    [JsonPropertyName("blocksHeadphones")]     public bool?         BlocksHeadphones     { get; set; }
    [JsonPropertyName("link")]                 public string?       Link                 { get; set; }
    [JsonPropertyName("lastLowPrice")]         public int?          LastLowPrice         { get; set; }
    [JsonPropertyName("changeLast48h")]        public float?        ChangeLast48h        { get; set; }
    [JsonPropertyName("changeLast48hPercent")] public float?        ChangeLast48hPercent { get; set; }
    [JsonPropertyName("low24hPrice")]          public int?          Low24hPrice          { get; set; }
    [JsonPropertyName("high24hPrice")]         public int?          High24hPrice         { get; set; }
    [JsonPropertyName("lastOfferCount")]       public int?          LastOfferCount       { get; set; }
    [JsonPropertyName("sellFor")]              public ItemPrice[]?  SellFor              { get; set; }
    [JsonPropertyName("buyFor")]               public ItemPrice[]?  BuyFor               { get; set; }
    [JsonPropertyName("category")]             public ItemCategory? Category             { get; set; }
    [JsonPropertyName("weight")]               public float?        Weight               { get; set; }
    [JsonPropertyName("velocity")]             public float?        Velocity             { get; set; }
    [JsonPropertyName("loudness")]             public int?          Loudness             { get; set; }
    [JsonPropertyName("craftsFor")]            public IdOnly[]      CraftsFor            { get; set; }
    [JsonPropertyName("craftsUsing")]          public IdOnly[]      CraftsUsing          { get; set; }

    // [JsonPropertyName("usedInTasks")]           public Task[]           UsedInTasks           { get; set; }
    // [JsonPropertyName("receivedFromTasks")]     public Task[]           ReceivedFromTasks     { get; set; }
    // [JsonPropertyName("bartersFor")]            public Barter[]         BartersFor            { get; set; }
    // [JsonPropertyName("bartersUsing")]          public Barter[]         BartersUsing          { get; set; }
}