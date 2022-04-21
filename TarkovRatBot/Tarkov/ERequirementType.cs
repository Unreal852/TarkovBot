using System.Text.Json.Serialization;

namespace TarkovRatBot.Tarkov;

public enum ERequirementType
{
    [JsonPropertyName("playerLevel")]    PlayerLevel,
    [JsonPropertyName("loyaltyLevel")]   LoyaltyLevel,
    [JsonPropertyName("questCompleted")] QuestCompleted,
    [JsonPropertyName("stationLevel")]   StationLevel,
}