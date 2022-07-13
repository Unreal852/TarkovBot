using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public interface IIdentifiable
{
    [JsonPropertyName("id")] public string Id { get; set; }
}