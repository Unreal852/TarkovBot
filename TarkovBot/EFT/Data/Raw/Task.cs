namespace TarkovBot.EFT.Data.Raw;

public class Task : IIdentifiable
{
    public string?         Id             { get; set; }
    public string          Name           { get; set; }
    public IdOnly          Trader         { get; set; }
    public IdOnly?         Map            { get; set; }
    public int             Experience     { get; set; }
    public string?         WikiLink       { get; set; }
    public int?            MinPlayerLevel { get; set; }
    public TaskObjective[] Objectives     { get; set; }
    public TaskKey[]?      NeededKeys     { get; set; }
}