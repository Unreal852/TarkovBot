using System.Collections.Concurrent;

namespace TarkovRatBot.Core;

public abstract class TarkovCache<TKey, T>
{
    public ConcurrentDictionary<TKey, T> Cache    { get; } = new();
    public bool                          IsCached => !Cache.IsEmpty;
    public int                           Count    => Cache.Count;

    public abstract Task<bool> UpdateCache();

    public virtual T Find(Predicate<T> predicate)
    {
        foreach (T value in Cache.Values)
        {
            if (predicate(value))
                return value;
        }

        return default;
    }
}