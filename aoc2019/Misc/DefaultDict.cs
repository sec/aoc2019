using System.Collections.Generic;

namespace aoc2019.Misc
{
    public class DefaultDict<K, V> : Dictionary<K, V>
    {
        public new V this[K key]
        {
            get
            {
                if (!TryGetValue(key, out V v))
                {
                    Add(key, default);

                    return default;
                }
                return v;
            }
            set
            {
                base[key] = value;
            }
        }
    }
}