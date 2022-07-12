using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class HideoutStation : IIdentifiable
{
    [JsonPropertyName("id")]           public string                Id           { get; set; }
    [JsonPropertyName("name")]         public string                Name         { get; set; }
    [JsonPropertyName("levels")]       public HideoutStationLevel[] Levels       { get; set; }
    [JsonPropertyName("tarkovDataId")] public int?                  TarkovDataId { get; set; }
    [JsonPropertyName("crafts")]       public IdOnly[]              Crafts       { get; set; }
}