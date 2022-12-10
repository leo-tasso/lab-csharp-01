namespace Indexers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <inheritdoc cref="IMap2D{TKey1,TKey2,TValue}" />
    public class Map2D<TKey1, TKey2, TValue> : IMap2D<TKey1, TKey2, TValue>
    {
        IDictionary<Tuple<TKey1, TKey2>, TValue> m = new Dictionary<Tuple<TKey1, TKey2>, TValue>();
        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.NumberOfElements" />
        public int NumberOfElements
        {
            get => m.Count;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.this" />
        public TValue this[TKey1 key1, TKey2 key2]
        {
            get => m[new Tuple<TKey1, TKey2>(key1, key2)];
            set => m[new Tuple<TKey1, TKey2>(key1, key2)] = value;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetRow(TKey1)" />
        public IList<Tuple<TKey2, TValue>> GetRow(TKey1 key1)
        {
            IList<Tuple<TKey2, TValue>> res = new List<Tuple<TKey2, TValue>>();
            foreach (Tuple<TKey1, TKey2> t in m.Keys)
            {
                if (t.Item1.Equals(key1))
                {
                    res.Add(new Tuple<TKey2, TValue>(t.Item2, m[new Tuple<TKey1, TKey2>(key1, t.Item2)]));
                }
            }
            return res;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetColumn(TKey2)" />
        public IList<Tuple<TKey1, TValue>> GetColumn(TKey2 key2)
        {
            IList<Tuple<TKey1, TValue>> res = new List<Tuple<TKey1, TValue>>();
            foreach (Tuple<TKey1, TKey2> t in m.Keys)
            {
                if (t.Item2.Equals(key2))
                {
                    res.Add(new Tuple<TKey1, TValue>(t.Item1, m[new Tuple<TKey1, TKey2>(t.Item1, key2)]));
                }
            }
            return res;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetElements" />
        public IList<Tuple<TKey1, TKey2, TValue>> GetElements()
        {
            IList<Tuple<TKey1, TKey2, TValue>> res = new List<Tuple<TKey1, TKey2, TValue>>();
            foreach (Tuple<TKey1, TKey2> k in m.Keys)
            {
                res.Add(new Tuple<TKey1, TKey2, TValue>(k.Item1, k.Item2, m[k]));
            }
            return res;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.Fill(IEnumerable{TKey1}, IEnumerable{TKey2}, Func{TKey1, TKey2, TValue})" />
        public void Fill(IEnumerable<TKey1> keys1, IEnumerable<TKey2> keys2, Func<TKey1, TKey2, TValue> generator)
        {
            foreach (TKey1 k1 in keys1)
            {
                foreach (TKey2 k2 in keys2)
                {
                    m.Add(new Tuple<TKey1, TKey2>(k1, k2), generator(k1, k2));
                }
            }
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)" />
        public bool Equals(IMap2D<TKey1, TKey2, TValue> other)
        {
            if (other is Map2D<TKey1, TKey2, TValue> otherMap2d)
            {
                return this.Equals(otherMap2d);
            }

            return false;
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj == this)
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals(obj as Map2D<TKey1, TKey2, TValue>);
        }

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
            return this.m != null ? this.m.GetHashCode() : 0;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.ToString"/>
        public override string ToString()
        {
            return "{ " + string.Join(", ", this.GetElements()
                       .Select(t => $"({t.Item1}, {t.Item2}) -> {t.Item3}")) + "}";
        }
    }
}
