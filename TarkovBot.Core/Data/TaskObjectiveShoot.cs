using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class TaskObjectiveShoot
{
    [JsonPropertyName("id")]                 public string?        Id                 { get; set; }
    [JsonPropertyName("type")]               public string         Type               { get; set; }
    [JsonPropertyName("description")]        public string         Description        { get; set; }
    [JsonPropertyName("maps")]               public Map[]          Maps               { get; set; }
    [JsonPropertyName("optional")]           public bool           Optional           { get; set; }
    [JsonPropertyName("target")]             public string         Target             { get; set; }
    [JsonPropertyName("count")]              public int            Count              { get; set; }
    [JsonPropertyName("shotType")]           public string         ShotType           { get; set; }
    [JsonPropertyName("zoneNames")]          public string[]       ZoneNames          { get; set; }
    [JsonPropertyName("bodyParts")]          public string[]       BodyParts          { get; set; }
    [JsonPropertyName("usingWeapon")]        public Item[]?        UsingWeapon        { get; set; }
    [JsonPropertyName("usingWeaponMods")]    public Item[]?        UsingWeaponMods    { get; set; }
    [JsonPropertyName("wearing")]            public Item[]?        Wearing            { get; set; }
    [JsonPropertyName("notWearing")]         public Item[]?        NotWearing         { get; set; }
    [JsonPropertyName("distance")]           public NumberCompare? Distance           { get; set; }
    [JsonPropertyName("playerHealthEffect")] public HealthEffect?  PlayerHealthEffect { get; set; }
    [JsonPropertyName("enemyHealthEffect")]  public HealthEffect?  EnemyHealthEffect  { get; set; }
}