using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.General
{
    public abstract class ObservableKeyedCollection<TKey, TValue>
    {

        protected readonly Dictionary<TKey, TValue> underlying = new();
        private Func<TValue, TKey> keyFinder;

        public ObservableKeyedCollection(Func<TValue, TKey> keyFinder = null)
        {
            this.keyFinder = keyFinder;
        }

        public int Count => underlying.Count;
        public bool IsReadOnly => ((IDictionary<TKey, TValue>)underlying).IsReadOnly;

        protected abstract void itemsAdded(IEnumerable<IObservableCollection<KeyValuePair<TKey, TValue>>.ItemWithPosition> itemsWithPositions);
        protected abstract void itemsRemoved(IEnumerable<IObservableCollection<KeyValuePair<TKey, TValue>>.ItemWithPosition> itemsWithPositions);

        public virtual void Add(KeyValuePair<TKey, TValue> keyValuePair)
            => Add(keyValuePair.Key, keyValuePair.Value);

        public virtual void Add(TKey key, TValue value)
        {
            ThrowIfKeyCantBeUsedForItem(key, value);
            underlying.Add(key, value);
            KeyValuePair<TKey, TValue> keyValuePair = KeyValuePair.Create(key, value);
            itemsAdded(new IObservableCollection<KeyValuePair<TKey, TValue>>.ItemWithPosition[] { new(keyValuePair, underlying.GetIndexOf(keyValuePair)) });
        }

        public virtual void Add(TValue value)
        {
            if (keyFinder == null)
                throw new NoKeyFinderDelegateException();
            Add(keyFinder(value), value);
        }

        public bool ChangeKey(TKey oldKey, TKey newKey)
        {
            if (EqualityComparer<TKey>.Default.Equals(oldKey, newKey))
                return true;
            if (!underlying.TryGetValue(oldKey, out TValue value))
                return false;
            ThrowIfKeyCantBeUsedForItem(newKey, value);
            underlying.Remove(oldKey);
            underlying.Add(newKey, value);
            return true;
        }

        public bool TryChangeKey(TKey oldKey, TKey newKey)
        {
            try
            {
                return ChangeKey(oldKey, newKey);
            }
            catch (KeyIsAlreadyUsedException)
            {
                return false;
            }
        }

        public bool ChangeKeyOfItem(TValue item, TKey newKey)
        {
            TKey oldKey = GetKeyOf(item);
            return ChangeKey(oldKey, newKey);
        }

        public bool TryChangeKeyOfItem(TValue item, TKey newKey)
        {
            try
            {
                return ChangeKeyOfItem(item, newKey);
            }
            catch (KeyIsAlreadyUsedException)
            {
                return false;
            }
        }

        public bool CanKeyBeUsedForItem(TKey key, TValue item, out TValue keyOwnerItem)
        {
            if (!underlying.TryGetValue(key, out keyOwnerItem))
                return true;
            return EqualityComparer<TValue>.Default.Equals(keyOwnerItem, item);
        }

        public void ThrowIfKeyCantBeUsedForItem(TKey key, TValue item)
        {
            if (!CanKeyBeUsedForItem(key, item, out TValue keyOwnerItem))
                throw new KeyIsAlreadyUsedException(key, item, keyOwnerItem);
        }

        public TKey GetKeyOf(TValue item)
            => underlying.FirstOrDefault(kvp => EqualityComparer<TValue>.Default.Equals(kvp.Value, item)).Key;

        public void Clear()
        {
            int count = underlying.Count;
            IEnumerable<IObservableEnumerable<KeyValuePair<TKey, TValue>>.ItemWithPosition> removedItems = underlying.Select((kvp, i) => new IObservableEnumerable<KeyValuePair<TKey, TValue>>.ItemWithPosition(kvp, i));
            underlying.Clear();
            if (count > 0)
                itemsRemoved(removedItems);
        }

        public bool Remove(KeyValuePair<TKey, TValue> keyValuePair)
        {
            int position = underlying.GetIndexOf(keyValuePair);
            if (((IDictionary<TKey, TValue>)underlying).Remove(keyValuePair))
            {
                itemsRemoved(new IObservableEnumerable<KeyValuePair<TKey, TValue>>.ItemWithPosition[] { new(keyValuePair, position) });
                return true;
            }
            return false;
        }

        public virtual bool RemoveByKey(TKey key, out TValue removedItem)
        {
            int position = underlying.Keys.GetIndexOf(key);
            if (underlying.Remove(key, out removedItem))
            {
                itemsRemoved(new IObservableEnumerable<KeyValuePair<TKey, TValue>>.ItemWithPosition[] { new(KeyValuePair.Create(key, removedItem), position) });
                return true;
            }
            return false;
        }

        public virtual bool RemoveByKey(TKey key)
            => RemoveByKey(key, out TValue _);

        public virtual bool RemoveValue(TValue value)
        {
            TKey key = GetKeyOf(value);
            return RemoveByKey(key);
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value) => underlying.TryGetValue(key, out value);

        public class KeyIsAlreadyUsedException : Exception
        {
            public KeyIsAlreadyUsedException(TKey key, TValue keyWantingItem, TValue keyOwnerItem)
                : base($"The key [{key}] can't be used for the item [{keyWantingItem}], because it is already used by [{keyOwnerItem}].")
            { }
        }

        public class NoKeyFinderDelegateException : Exception
        {
            public NoKeyFinderDelegateException()
                : base($"No key finder delegate defined for observable dictionary, so Add({nameof(TValue)}) can't be used.")
            { }
        }

    }
}
