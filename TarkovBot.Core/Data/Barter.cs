using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class Barter
{
    [JsonPropertyName("id")]            public string          Id            { get; set; }
    [JsonPropertyName("trader")]        public Trader          Trader        { get; set; }
    [JsonPropertyName("level")]         public int             Level         { get; set; }
    [JsonPropertyName("taskUnlock")]    public Task?           TaskUnlock    { get; set; }
    [JsonPropertyName("requiredItems")] public ContainedItem[] RequiredItems { get; set; }
}