using OpenSC.Model.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
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

        private List<TItem> registeredItemsWithoutKey = new();
        private Dictionary<TKey, TItem> registeredItems = new();

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
            if (key == null)
            {
                if (registeredItemsWithoutKey.Contains(item))
                    return;
                registeredItemsWithoutKey.Add(item);
            }
            else
            {
                if (!CanKeyBeUsedForItem(item, key, out TItem foundItem))
                    throw new KeyIsAlreadyUsedException(key, item, foundItem);
                registeredItems.Add(key, item);
            }
            keyChangedSubscribeMethod(item);
            itemRemovedSubscribeMethod(item);
            ItemAdded?.Invoke(new TItem[] { item });
            ItemsChanged?.Invoke();
        }

        public void Unregister(TItem item)
        {
            TKey itemKey = getKey(item);
            if (itemKey != null)
            {
                UnregisterByKey(itemKey);
                return;
            }
            if (registeredItemsWithoutKey.Contains(item))
            {
                registeredItemsWithoutKey.Remove(item);
                itemRemoved(item);
            }
        }
         

        public void UnregisterByKey(TKey key)
        {
            if (key == null)
                return;
            if (!registeredItems.TryGetValue(key, out TItem removedItem))
                return;
            itemRemoved(removedItem);
        }

        private void itemRemoved(TItem item)
        {
            keyChangedUnsubscribeMethod(item);
            itemRemovedUnsubscribeMethod(item);
            ItemRemoved?.Invoke(new TItem[] { item });
            ItemsChanged?.Invoke();
        }

        public void ItemKeyChanged(TItem item)
        {
            TKey key = getKey(item);
            bool registeredWithoutKey = registeredItemsWithoutKey.Contains(item);
            bool registeredWithKey = registeredItems.ContainsValue(item);
            if (!registeredWithoutKey && !registeredWithKey)
                return;
            if (!CanKeyBeUsedForItem(item, key, out TItem foundItem))
                throw new KeyIsAlreadyUsedException(key, item, foundItem);
            if (registeredWithoutKey && (key != null))
            {
                registeredItemsWithoutKey.Remove(item);
                registeredItems.Add(key, item); 
            }
            if (registeredWithKey)
            {
                TKey previousRegisteredKey = registeredItems.FirstOrDefault(kvp => kvp.Value == item).Key;
                registeredItems.Remove(previousRegisteredKey);
                if (key != null)
                    registeredItems.Add(key, item);
                else
                    registeredItemsWithoutKey.Add(item);
            }
        }

        public bool CanKeyBeUsedForItem(TItem item, TKey key, out TItem keyOwnerItem)
        {
            keyOwnerItem = null;
            if (key == null)
                return true;
            if (!registeredItems.TryGetValue(key, out keyOwnerItem))
            {
                keyOwnerItem = null;
                return true;
            }
            return keyOwnerItem == item;
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
