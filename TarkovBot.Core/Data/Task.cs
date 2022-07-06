using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class Task
{
    [JsonPropertyName("id")]                      public string?                 Id                      { get; set; }
    [JsonPropertyName("tarkovDataId")]            public int?                    TarkovDataId            { get; set; }
    [JsonPropertyName("name")]                    public string                  Name                    { get; set; }
    [JsonPropertyName("trader")]                  public Trader                  Trader                  { get; set; }
    [JsonPropertyName("map")]                     public Map?                    Map                     { get; set; }
    [JsonPropertyName("experience")]              public int                     Experience              { get; set; }
    [JsonPropertyName("wikiLink")]                public string?                 WikiLink                { get; set; }
    [JsonPropertyName("minPlayerLevel")]          public int?                    MinPlayerLevel          { get; set; }
    [JsonPropertyName("taskRequirements")]        public TaskStatusRequirement[] TaskRequirements        { get; set; }
    [JsonPropertyName("traderLevelRequirements")] public RequirementTrader[]     TraderLevelRequirements { get; set; }
    [JsonPropertyName("objectives")]              public TaskObjective[]         Objectives              { get; set; }
    [JsonPropertyName("startRewards")]            public TaskRewards?            StartRewards            { get; set; }
    [JsonPropertyName("finishRewards")]           public TaskRewards?            FinishRewards           { get; set; }
    [JsonPropertyName("factionName")]             public string?                 FactionName             { get; set; }
    [JsonPropertyName("neededKeys")]              public TaskKey[]?              NeededKeys              { get; set; }
}