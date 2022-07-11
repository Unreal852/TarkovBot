using System.Collections.Concurrent;
using TarkovBot.Core.Data;
using TarkovBot.Core.GraphQL;

namespace TarkovBot.Core.Providers;

public abstract class LocalizedDataProvider<TKey, TValue> : DataProvider<LanguageCode, ConcurrentDictionary<TKey, TValue>> where TKey : notnull
{
    protected LocalizedDataProvider(GraphQlQuery query) : base(query)
    {
    }

    public abstract Task<bool> UpdateCache(LanguageCode lang);

    public IEnumerable<TValue> Where(LanguageCode lang, Predicate<TValue> predicate)
    {
        if (!Cache.TryGetValue(lang, out ConcurrentDictionary<TKey, TValue>? cache))
            yield break;
        foreach (KeyValuePair<TKey, TValue> pair in cache)
        {
            if (predicate(pair.Value))
                yield return pair.Value;
        }
    }

    public TValue? Single(LanguageCode lang, Predicate<TValue> predicate)
    {
        return Where(lang, predicate).FirstOrDefault();
    }

    public override IEnumerable<ConcurrentDictionary<TKey, TValue>> Where(Predicate<ConcurrentDictionary<TKey, TValue>> predicate)
    {
        throw new NotSupportedException();
    }

    public override ConcurrentDictionary<TKey, TValue>? Single(Predicate<ConcurrentDictionary<TKey, TValue>> predicate)
    {
        throw new NotSupportedException();
    }
}