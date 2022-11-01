using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.General
{
    public class ObservableValueDictionary<TKey, TValue> : ObservableKeyedCollection<TKey, TValue>, IObservableCollection<TValue>
    {

        public ObservableValueDictionary(Func<TValue, TKey> keyFinder = null)
            : base(keyFinder) { }

        public TValue this[TKey key]
        {
            get
            {
                underlying.TryGetValue(key, out TValue value);
                return value;
            }
        }

        public IEnumerator<TValue> GetEnumerator() => underlying.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => underlying.Values.GetEnumerator();

        public bool Contains(TValue value) => underlying.ContainsValue(value);

        public event ObservableEnumerableItemsChangedDelegate<TValue> ItemsAdded;
        public event ObservableEnumerableItemsChangedDelegate<TValue> ItemsRemoved;

        protected override void itemsAdded(IEnumerable<IObservableEnumerable<KeyValuePair<TKey, TValue>>.ItemWithPosition> itemsWithPositions)
            => ItemsAdded?.Invoke(itemsWithPositions.Select(iwp => new IObservableEnumerable<TValue>.ItemWithPosition(iwp.Item.Value, iwp.Position)));

        protected override void itemsRemoved(IEnumerable<IObservableEnumerable<KeyValuePair<TKey, TValue>>.ItemWithPosition> itemsWithPositions)
            => ItemsRemoved?.Invoke(itemsWithPositions.Select(iwp => new IObservableEnumerable<TValue>.ItemWithPosition(iwp.Item.Value, iwp.Position)));

        public void CopyTo(TValue[] array, int arrayIndex)
            => underlying.Values.CopyTo(array, arrayIndex);

        public virtual bool Remove(TValue item)
            => RemoveValue(item);

    }

}
