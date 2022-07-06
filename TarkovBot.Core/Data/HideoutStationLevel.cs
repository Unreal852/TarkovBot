using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class HideoutStationLevel
{
    [JsonPropertyName("id")]                       public string                           Id                       { get; set; }
    [JsonPropertyName("level")]                    public int                              Level                    { get; set; }
    [JsonPropertyName("constructionTime")]         public int                              ConstructionTime         { get; set; }
    [JsonPropertyName("description")]              public string                           Description              { get; set; }
    [JsonPropertyName("itemRequirements")]         public RequirementItem[]                ItemRequirements         { get; set; }
    [JsonPropertyName("stationLevelRequirements")] public RequirementHideoutStationLevel[] StationLevelRequirements { get; set; }
    [JsonPropertyName("skillRequirements")]        public RequirementSkill[]               SkillRequirements        { get; set; }
    [JsonPropertyName("traderRequirements")]       public RequirementTrader[]              TraderRequirements       { get; set; }
    [JsonPropertyName("tarkovDataId")]             public int?                             TarkovDataId             { get; set; }
    [JsonPropertyName("crafts")]                   public Craft[]                          Crafts                   { get; set; }
}