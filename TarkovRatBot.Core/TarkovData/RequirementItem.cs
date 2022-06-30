using System.Text.Json.Serialization;

namespace TarkovRatBot.Core.TarkovData;

public class RequirementItem
{
    [JsonPropertyName("id")]         public string? Id       { get; set; }
    [JsonPropertyName("count")]      public int     Count    { get; set; }
    [JsonPropertyName("quantity")]   public int     Quantity { get; set; }
    [JsonPropertyName("attributes")] public ItemAttribute[]? Attributes { get; set; }
    [JsonPropertyName("item")]       public IdOnly          Item       { get; set; }
}