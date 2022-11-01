using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.General
{
    public class ObservableDictionary<TKey, TValue> : ObservableKeyedCollection<TKey, TValue>, IObservableDictionary<TKey, TValue>
    {

        public TValue this[TKey key]
        {
            get => underlying[key];
            set => underlying[key] = value;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => underlying.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)underlying).GetEnumerator();

        public event ObservableEnumerableItemsChangedDelegate<KeyValuePair<TKey, TValue>> ItemsAdded;
        public event ObservableEnumerableItemsChangedDelegate<KeyValuePair<TKey, TValue>> ItemsRemoved;

        protected override void itemsAdded(IEnumerable<IObservableCollection<KeyValuePair<TKey, TValue>>.ItemWithPosition> itemsWithPositions)
            => ItemsAdded?.Invoke(itemsWithPositions);

        protected override void itemsRemoved(IEnumerable<IObservableCollection<KeyValuePair<TKey, TValue>>.ItemWithPosition> itemsWithPositions)
            => ItemsRemoved?.Invoke(itemsWithPositions);


        public ICollection<TKey> Keys => new KeyCollection(this, underlying.Keys);
        public ICollection<TValue> Values => new ValueCollection(this, underlying.Values);

        public bool Contains(KeyValuePair<TKey, TValue> keyValuePair) => ((IDictionary<TKey, TValue>)underlying).Contains(keyValuePair);
        public bool ContainsKey(TKey key) => underlying.ContainsKey(key);
        public bool ContainsValue(TValue value) => Values.Contains(value);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => ((IDictionary<TKey, TValue>)underlying).CopyTo(array, arrayIndex);

        public bool Remove(TKey key) => RemoveByKey(key);

        private abstract class KeyValueCollection<T> : ObservableEnumerableAdapter<T, KeyValuePair<TKey, TValue>>, IObservableCollection<T>
        {

            private readonly ICollection<T> underlying;

            public KeyValueCollection(ObservableDictionary<TKey, TValue> dictionary, ICollection<T> underlying)
            {
                Adaptee = dictionary;
                this.underlying = underlying;
            }

            protected override IEnumerator<T> getEnumerator() => underlying.GetEnumerator();

            public int Count => underlying.Count;
            public bool IsReadOnly => underlying.IsReadOnly;
            public void Add(T item) => underlying.Add(item);
            public void Clear() => underlying.Clear();
            public bool Contains(T item) => underlying.Contains(item);
            public void CopyTo(T[] array, int arrayIndex) => underlying.CopyTo(array, arrayIndex);
            public bool Remove(T item) => underlying.Remove(item);

        }

        private class KeyCollection : KeyValueCollection<TKey>
        {
            public KeyCollection(ObservableDictionary<TKey, TValue> dictionary, ICollection<TKey> underlying) : base(dictionary, underlying) { }
            protected override TKey convertAdaptee(KeyValuePair<TKey, TValue> adaptee) => adaptee.Key;
        }

        private class ValueCollection : KeyValueCollection<TValue>
        {
            public ValueCollection(ObservableDictionary<TKey, TValue> dictionary, ICollection<TValue> underlying) : base(dictionary, underlying) { }
            protected override TValue convertAdaptee(KeyValuePair<TKey, TValue> adaptee) => adaptee.Value;
        }

    }

}
