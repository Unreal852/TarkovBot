using TarkovBot.Data;

namespace TarkovBot.Services.Abstractions;

public interface IItemsProvider
{
    Task Initialize();
    
    IEnumerable<TarkovItem> FindByName(string itemName, int maxItems = 10);
    TarkovItem?             FindById(string itemId);
}