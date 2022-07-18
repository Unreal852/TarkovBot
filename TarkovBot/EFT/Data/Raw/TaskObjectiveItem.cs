namespace TarkovBot.EFT.Data.Raw;

public class TaskObjectiveItem : TaskObjective
{
    public IdOnly Item          { get; set; }
    public int    Count         { get; set; }
    public bool   FoundInRaid   { get; set; }
    public int?   DogTagLevel   { get; set; }
    public int?   MaxDurability { get; set; }
    public int?   MinDurability { get; set; }
}