using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class TaskObjectiveBuildItem
{
    [JsonPropertyName("id")]          public string?              Id          { get; set; }
    [JsonPropertyName("type")]        public string               Type        { get; set; }
    [JsonPropertyName("description")] public string               Description { get; set; }
    [JsonPropertyName("maps")]        public Map[]                Maps        { get; set; }
    [JsonPropertyName("optional")]    public bool                 Optional    { get; set; }
    [JsonPropertyName("item")]        public Item                 Item        { get; set; }
    [JsonPropertyName("containsAll")] public Item[]               ContainsAll { get; set; }
    [JsonPropertyName("containsOne")] public Item[]               ContainsOne { get; set; }
    [JsonPropertyName("attributes")]  public AttributeThreshold[] Attributes  { get; set; }
}