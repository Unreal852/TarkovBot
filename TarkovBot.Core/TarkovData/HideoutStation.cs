using System.Text.Json.Serialization;
using TarkovBot.Core.GraphQL.Attributes;

namespace TarkovBot.Core.TarkovData;

[GraphQl("hideoutStations")]
public class HideoutStation
{
    [JsonPropertyName("id")]           public string                Id           { get; set; }
    [JsonPropertyName("name")]         public string                Name         { get; set; }
    [JsonPropertyName("tarkovDataId")] public int?                  TarkovDataId { get; set; }
    [JsonPropertyName("levels")]       public HideoutStationLevel[] Levels       { get; set; }
    [JsonPropertyName("crafts")]       public IdOnly[]              Crafts       { get; set; }
}