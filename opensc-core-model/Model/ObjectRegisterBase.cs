using OpenSC.Model.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{

    public abstract class ObjectRegisterBase<TKey, TItem> : IObservableList, IObservableList<TItem>
        where TItem : class
    {

        #region IObservableList<T> implementation
        public TItem this[int index] => registeredItems.Values.ToList()[index];

        public int Count => registeredItems.Count;

        public event ObservableListItemAddedDelegate ItemAdded;
        public event ObservableListItemRemovedDelegate ItemRemoved;
        public event ObservableListItemsChangedDelegate ItemsChanged;

        public IEnumerator<TItem> GetEnumerator() => registeredItems.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => registeredItems.Values.GetEnumerator();
        #endregion

        private Dictionary<TKey, TItem> registeredItems = new Dictionary<TKey, TItem>();

        public TItem this[TKey key]
        {
            get
            {
                if (key == null)
                    return null;
                if (!registeredItems.TryGetValue(key, out TItem foundBoolean))
                    return null;
                return foundBoolean;
            }
        }

        public void Register(TItem item)
        {
            TKey key = getKey(item);
            if (!CanKeyBeUsedForItem(item, key, out TItem foundItem))
                throw new KeyIsAlreadyUsedException(key, item, foundItem);
            registeredItems.Add(key, item);
            keyChangedSubscribeMethod(item);
            itemRemovedSubscribeMethod(item);
            ItemAdded?.Invoke(new TItem[] { item });
            ItemsChanged?.Invoke();
        }

        public void Unregister(TItem item) => UnregisterByKey(getKey(item));

        public void UnregisterByKey(TKey key)
        {
            if (key == null)
                return;
            if (!registeredItems.TryGetValue(key, out TItem removedItem))
                return;
            registeredItems.Remove(key);
            keyChangedUnsubscribeMethod(removedItem);
            itemRemovedUnsubscribeMethod(removedItem);
            ItemRemoved?.Invoke(new TItem[] { removedItem });
            ItemsChanged?.Invoke();
        }

        public void ItemKeyChanged(TItem item)
        {
            if (!registeredItems.ContainsValue(item))
                return;
            TKey key = getKey(item);
            if (!CanKeyBeUsedForItem(item, key, out TItem foundItem))
                throw new KeyIsAlreadyUsedException(key, item, foundItem);
            TKey previousRegisteredKey = registeredItems.FirstOrDefault(kvp => (kvp.Value == item)).Key;
            registeredItems.Remove(previousRegisteredKey);
            registeredItems.Add(key, item);
        }

        public bool CanKeyBeUsedForItem(TItem item, TKey key, out TItem keyOwnerItem)
        {
            if (!registeredItems.TryGetValue(key, out keyOwnerItem)) {
                keyOwnerItem = null;
                return true;
            }
            return (keyOwnerItem == item);
        }

        protected abstract TKey getKey(TItem item);
        protected abstract void keyChangedSubscribeMethod(TItem item);
        protected abstract void keyChangedUnsubscribeMethod(TItem item);
        protected abstract void itemRemovedSubscribeMethod(TItem item);
        protected abstract void itemRemovedUnsubscribeMethod(TItem item);

        public virtual string ToStringMethod(TItem item) => getKey(item).ToString();

        public class KeyIsAlreadyUsedException : Exception
        {
            public KeyIsAlreadyUsedException(TKey key, TItem keyWantingItem, TItem keyOwnerItem)
                : base($"The key [{key}] can't be used for the item [{keyWantingItem}], because it is already used by [{keyOwnerItem}].")
            { }
        }

    }

}
