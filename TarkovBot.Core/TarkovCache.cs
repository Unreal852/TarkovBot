using System.Collections;
using System.Collections.Concurrent;

namespace TarkovBot.Core;

public abstract class TarkovCache<TKey, T> : IEnumerable<T>
{
    public ConcurrentDictionary<TKey, T> Cache    { get; } = new();
    public bool                          IsCached => !Cache.IsEmpty;
    public int                           Count    => Cache.Count;

    public IEnumerator<T> GetEnumerator()
    {
        return Cache.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public abstract Task<bool> UpdateCache();
}