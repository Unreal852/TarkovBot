namespace TarkovBot.EFT.Data;

/// <summary>
/// Represents the name and short name of an item.
/// This may be used later when storing multiple localized names within a single <see cref="ItemInfos"/>
/// </summary>
public class ItemName
{
    public string Name      { get; init; }
    public string ShortName { get; init; }
}