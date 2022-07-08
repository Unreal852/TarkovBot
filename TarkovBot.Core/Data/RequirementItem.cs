using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class RequirementItem
{
    [JsonPropertyName("id")]         public string?          Id         { get; set; }
    [JsonPropertyName("item")]       public Item             Item       { get; set; }
    [JsonPropertyName("count")]      public int              Count      { get; set; }
    [JsonPropertyName("quantity")]   public int              Quantity   { get; set; }
    [JsonPropertyName("attributes")] public ItemAttribute[]? Attributes { get; set; }
}