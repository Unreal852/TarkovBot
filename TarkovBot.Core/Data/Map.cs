using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class Map
{
    [JsonPropertyName("id")]           public string    Id           { get; set; }
    [JsonPropertyName("tarkovDataId")] public string?   TarkovDataId { get; set; }
    [JsonPropertyName("name")]         public string?   Name         { get; set; }
    [JsonPropertyName("wiki")]         public string?   Wiki         { get; set; }
    [JsonPropertyName("description")]  public string?   Description  { get; set; }
    [JsonPropertyName("enemies")]      public string[]? Enemies      { get; set; }
    [JsonPropertyName("raidDuration")] public int?      RaidDuration { get; set; }
    [JsonPropertyName("players")]      public string?   Players      { get; set; }
}