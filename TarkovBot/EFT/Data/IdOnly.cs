using System.Text.Json.Serialization;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace TarkovBot.EFT.Data;

public class IdOnly : IIdentifiable
{
    [JsonPropertyName("id")] public string Id { get; set; }
}