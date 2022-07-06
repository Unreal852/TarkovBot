using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class TaskObjectiveItem
{
    [JsonPropertyName("id")]            public string? Id            { get; set; }
    [JsonPropertyName("type")]          public string  Type          { get; set; }
    [JsonPropertyName("description")]   public string  Description   { get; set; }
    [JsonPropertyName("maps")]          public Map[]   Maps          { get; set; }
    [JsonPropertyName("optional")]      public bool    Optional      { get; set; }
    [JsonPropertyName("item")]          public Item    Item          { get; set; }
    [JsonPropertyName("count")]         public int     Count         { get; set; }
    [JsonPropertyName("foundInRaid")]   public bool    FoundInRaid   { get; set; }
    [JsonPropertyName("dogTagLevel")]   public int?    DogTagLevel   { get; set; }
    [JsonPropertyName("maxDurability")] public int?    MaxDurability { get; set; }
    [JsonPropertyName("minDurability")] public int?    MinDurability { get; set; }
}