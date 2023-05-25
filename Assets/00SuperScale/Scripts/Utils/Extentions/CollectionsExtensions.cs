using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperScale
{
    public static class CollectionsExtensions
    {
        public static void AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }
            else
            {
                dictionary[key] = value;
            }
        }

        public static void Foreach<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Action<KeyValuePair<TKey, TValue>> action)
        {
            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
            {
                action?.Invoke(pair);
            }
        }
    }
}
