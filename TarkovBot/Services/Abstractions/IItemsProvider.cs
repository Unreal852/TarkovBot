using TarkovBot.Data;

namespace TarkovBot.Services.Abstractions;

public interface IItemsProvider
{
    bool IsUpdating { get; }

    Task                    UpdateItems();
    IEnumerable<TarkovItem> FindByName(string itemName, int maxItems = 10);
    TarkovItem?             FindById(string itemId);
}