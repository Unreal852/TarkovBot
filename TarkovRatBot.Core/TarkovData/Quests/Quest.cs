using System.Text.Json.Serialization;

namespace TarkovRatBot.Core.TarkovData.Quests;

public class Quest
{
    [JsonPropertyName("id")]           public string           Id                { get; set; }
    [JsonPropertyName("title")]        public string           Title             { get; set; }
    [JsonPropertyName("wikiLink")]     public string           WikiLink          { get; set; }
    [JsonPropertyName("unlocks")]      public string[]         Unlocks           { get; set; }
    [JsonPropertyName("requirements")] public QuestRequirement QuestRequirements { get; set; }
    [JsonPropertyName("giver")]        public Trader       Giver             { get; set; }
    [JsonPropertyName("turnin")]       public Trader       TurnIn            { get; set; }
}