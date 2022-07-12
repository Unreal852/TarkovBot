using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class IdOnly : IIdentifiable
{
    [JsonPropertyName("id")] public string Id { get; set; }
}