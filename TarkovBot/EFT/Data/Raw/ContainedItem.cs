using System.Text.Json.Serialization;

namespace TarkovBot.EFT.Data.Raw;

public class ContainedItem
{
    [JsonPropertyName("item")]       public IdOnly           Item       { get; set; }
    [JsonPropertyName("count")]      public float            Count      { get; set; }
    [JsonPropertyName("quantity")]   public float            Quantity   { get; set; }
    [JsonPropertyName("attributes")] public ItemAttribute[]? Attributes { get; set; }
}