using OpenSC.Model.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{

    public abstract class ObjectRegisterBase<TKey, TObject> : ObservableEnumerableAdapter<TObject, KeyValuePair<TKey, TObject>>
        where TObject : class
    {

        private List<TObject> registeredItemsWithoutKey = new();
        private ObservableDictionary<TKey, TObject> registeredItems = new();

        public ObjectRegisterBase() => Adaptee = registeredItems;
        protected override IEnumerator<TObject> getEnumerator() => registeredItems.Values.GetEnumerator();
        protected override TObject convertAdaptee(KeyValuePair<TKey, TObject> adaptee) => adaptee.Value;

        public TObject this[TKey key]
        {
            get
            {
                if (key == null)
                    return null;
                if (registeredItems.TryGetValue(key, out TObject value))
                    return value;
                return null;
            }
        }

        public void Register(TObject item)
        {
            TKey key = GetKey(item);
            if (key == null)
            {
                if (registeredItemsWithoutKey.Contains(item))
                    return;
                registeredItemsWithoutKey.Add(item);
            }
            else
            {
                if (registeredItems.ContainsValue(item))
                    return;
                if (!CanKeyBeUsedForItem(item, key, out TObject foundItem))
                    throw new KeyIsAlreadyUsedException(key, item, foundItem);
                registeredItems.Add(key, item);
            }
            keyChangedSubscribeMethod(item);
            itemRemovedSubscribeMethod(item);
        }

        public void Unregister(TObject item)
        {
            TKey itemKey = GetKey(item);
            if (itemKey != null)
            {
                UnregisterByKey(itemKey);
                return;
            }
            if (registeredItemsWithoutKey.Contains(item))
                registeredItemsWithoutKey.Remove(item);
        }


        public bool UnregisterByKey(TKey key)
        {
            if (key == null)
                return false;
            return registeredItems.Remove(key);
        }

        public bool CanKeyBeUsedForItem(TObject item, TKey key, out TObject keyOwnerItem)
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

        public void ItemKeyChanged(TObject item)
        {
            TKey key = GetKey(item);
            bool registeredWithoutKey = registeredItemsWithoutKey.Contains(item);
            bool registeredWithKey = registeredItems.ContainsValue(item);
            if (!registeredWithoutKey && !registeredWithKey)
                return;
            if (!CanKeyBeUsedForItem(item, key, out TObject foundItem))
                throw new KeyIsAlreadyUsedException(key, item, foundItem);
            if (registeredWithoutKey && (key != null))
            {
                registeredItemsWithoutKey.Remove(item);
                registeredItems.Add(key, item);
            }
            if (registeredWithKey)
                registeredItems.ChangeKeyOfItem(item, key);
        }

        public abstract TKey GetKey(TObject item);
        protected abstract void keyChangedSubscribeMethod(TObject item);
        protected abstract void keyChangedUnsubscribeMethod(TObject item);
        protected abstract void itemRemovedSubscribeMethod(TObject item);
        protected abstract void itemRemovedUnsubscribeMethod(TObject item);

        public virtual string ToStringMethod(TObject item) => GetKey(item).ToString();

        public class KeyIsAlreadyUsedException : Exception
        {
            public KeyIsAlreadyUsedException(TKey key, TObject keyWantingItem, TObject keyOwnerItem)
                : base($"The key [{key}] can't be used for the item [{keyWantingItem}], because it is already used by [{keyOwnerItem}].")
            { }
        }

    }

}
