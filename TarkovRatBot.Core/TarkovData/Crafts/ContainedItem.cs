using System.Text.Json.Serialization;

namespace TarkovRatBot.Core.TarkovData.Crafts;

public class ContainedItem
{
    [JsonPropertyName("item")]       public Item            Item       { get; set; }
    [JsonPropertyName("count")]      public float           Count      { get; set; }
    [JsonPropertyName("quantity")]   public float           Quantity   { get; set; }
    [JsonPropertyName("attributes")] public ItemAttribute[] Attributes { get; set; }
}