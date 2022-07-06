using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class TraderOffer
{
    [JsonPropertyName("name")]           public string Name           { get; set; }
    [JsonPropertyName("trader")]         public Trader Trader         { get; set; }
    [JsonPropertyName("minTraderLevel")] public int?   MinTraderLevel { get; set; }
    [JsonPropertyName("taskUnlock")]     public Task?  TaskUnlock     { get; set; }
}