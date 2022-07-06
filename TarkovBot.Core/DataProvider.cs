using System.Collections;
using System.Collections.Concurrent;
using TarkovBot.Core.GraphQL;

namespace TarkovBot.Core;

public abstract class DataProvider<TKey, T> : IEnumerable<T> where TKey : notnull
{
    protected DataProvider(GraphQlQuery query)
    {
        Query = query;
    }

    public ConcurrentDictionary<TKey, T> Cache    { get; } = new();
    public GraphQlQuery                  Query    { get; protected set; }
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

    public virtual IEnumerable<T> Where(Predicate<T> predicate)
    {
        foreach (KeyValuePair<TKey, T> pair in Cache)
        {
            if (predicate(pair.Value))
                yield return pair.Value;
        }
    }

    public virtual T? Single(Predicate<T> predicate)
    {
        foreach (KeyValuePair<TKey, T> pair in Cache)
        {
            if (predicate(pair.Value))
                return pair.Value;
        }

        return default;
    }
}