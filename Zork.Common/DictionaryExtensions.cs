using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    public static class DictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultvalue = default(TValue))
        {
            return (dictionary != null && key != null && dictionary.TryGetValue(key, out TValue value)) ? value : defaultvalue;
        }
    }
}
