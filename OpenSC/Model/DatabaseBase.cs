using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{
    abstract class DatabaseBase<T> where T: class, IModel
    {

        public delegate void AddingItemDelegate(DatabaseBase<T> database, T item);
        public delegate void AddedItemDelegate(DatabaseBase<T> database, T item);

        public event AddingItemDelegate AddingItem;
        public event AddedItemDelegate AddedItem;

        public delegate void RemovingItemDelegate(DatabaseBase<T> database, T item);
        public delegate void RemovedItemDelegate(DatabaseBase<T> database, T item);

        public event RemovingItemDelegate RemovingItem;
        public event RemovedItemDelegate RemovedItem;

        public delegate void ChangingItemsDelegate(DatabaseBase<T> database);
        public delegate void ChangedItemsDelegate(DatabaseBase<T> database);

        public event ChangingItemsDelegate ChangingItems;
        public event ChangedItemsDelegate ChangedItems;

        protected Dictionary<int, T> items = new Dictionary<int, T>();

        public IReadOnlyDictionary<int, T> Items
        {
            get => items;
        }

        public IReadOnlyList<T> ItemsAsList
        {
            get => items.Values.ToList();
        }

        public void Add(T item)
        {

            // Validate argument
            if (item == null)
                throw new ArgumentNullException();

            // Validate ID
            if (item.ID <= 0)
                throw new Exception();

            if (items.ContainsKey(item.ID))
                throw new Exception();

            // Add element
            ChangingItems?.Invoke(this);
            AddingItem?.Invoke(this, item);

            items.Add(item.ID, item);

            AddedItem?.Invoke(this, item);
            ChangedItems?.Invoke(this);

        }

        public bool Remove(T item)
        {

            // Validate argument
            if (item == null)
                throw new ArgumentNullException();

            if (!items.ContainsValue(item))
                return false;

            // Remove element
            ChangingItems?.Invoke(this);
            RemovingItem?.Invoke(this, item);

            items.Remove(item.ID);

            RemovedItem?.Invoke(this, item);
            ChangedItems?.Invoke(this);

            return true;

        }

        public bool CanIdBeUsedForItem(int id, T forItem)
        {
            if (!items.TryGetValue(id, out T foundItem))
                return true;
            if (foundItem == forItem)
                return true;
            return false;
        }

    }
}
