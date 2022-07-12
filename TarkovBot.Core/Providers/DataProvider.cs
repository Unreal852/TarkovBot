using System.Collections;
using System.Collections.Concurrent;
using TarkovBot.Core.GraphQL;

namespace TarkovBot.Core.Providers;

public abstract class DataProvider<TKey, TValue> : IEnumerable<TValue>
        where TKey : notnull
{
    protected DataProvider(GraphQlQuery query)
    {
        Query = query;
    }

    public ConcurrentDictionary<TKey, TValue> Cache { get; } = new();
    public GraphQlQuery                       Query { get; protected set; }
    public int                                Count => Cache.Count;

    public IEnumerator<TValue> GetEnumerator()
    {
        return Cache.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public abstract Task<int> UpdateCache();

    public TValue? GetByKey(TKey key)
    {
        return Cache.TryGetValue(key, out TValue? value) ? value : default;
    }

    public virtual IEnumerable<TValue> Where(Predicate<TValue> predicate)
    {
        foreach (KeyValuePair<TKey, TValue> pair in Cache)
        {
            if (predicate(pair.Value))
                yield return pair.Value;
        }
    }

    public virtual TValue? Single(Predicate<TValue> predicate)
    {
        foreach (KeyValuePair<TKey, TValue> pair in Cache)
        {
            if (predicate(pair.Value))
                return pair.Value;
        }

        return default;
    }
}