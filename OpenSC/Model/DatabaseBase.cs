using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model
{
    public abstract class DatabaseBase<T>: IDatabaseBase, IObservableList<T>
        where T: class, IModel
    {

        public delegate void AddingItemDelegate(DatabaseBase<T> database, T item);
        public delegate void AddedItemDelegate(DatabaseBase<T> database, T item);

        public event AddingItemDelegate AddingItem;
        public event AddedItemDelegate AddedItem;
        public event ObservableListItemAddedDelegate ItemAdded;

        public delegate void RemovingItemDelegate(DatabaseBase<T> database, T item);
        public delegate void RemovedItemDelegate(DatabaseBase<T> database, T item);

        public event RemovingItemDelegate RemovingItem;
        public event RemovedItemDelegate RemovedItem;
        public event ObservableListItemRemovedDelegate ItemRemoved;

        public delegate void ChangingItemsDelegate(DatabaseBase<T> database);
        public delegate void ChangedItemsDelegate(DatabaseBase<T> database);

        public event ChangingItemsDelegate ChangingItems;
        public event ChangedItemsDelegate ChangedItems;
        public event ObservableListItemsChangedDelegate ItemsChanged;

        protected Dictionary<int, T> items = new Dictionary<int, T>();

        public IReadOnlyDictionary<int, T> Items
        {
            get => items;
        }

        public IReadOnlyList<T> ItemsAsList
        {
            get => items.Values.ToList();
        }

        public int Count
        {
            get { return items.Count; }
        }

        public T this[int index]
        {
            get => GetTById(index);
        }

        public DatabaseBase()
        {
            persister  = new DatabasePersister<T>(this);
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

            ItemAdded?.Invoke();
            ItemsChanged?.Invoke();

            Save();

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

            ItemRemoved?.Invoke();
            ItemsChanged?.Invoke();

            Save();

            return true;

        }

        public T GetTById(int id)
        {
            if (items.TryGetValue(id, out T value))
                return value;
            return null;            
        }

        public object GetById(int id)
        {
            return GetTById(id);
        }

        public bool CanIdBeUsedForItem(int id, T forItem)
        {
            if (!items.TryGetValue(id, out T foundItem))
                return true;
            if (foundItem == forItem)
                return true;
            return false;
        }

        public int NextValidId()
        {
            if (items.Count == 0)
                return 1;
            return items.Keys.Max() + 1;
        }

        private DatabasePersister<T> persister;

        public void Save()
        {
            persister.Save(items);
        }

        public void Load()
        {
            items.Clear();
            var loadedItems = persister.Load();
            if (loadedItems != null)
            {
                ChangingItems?.Invoke(this);
                items = loadedItems;
                ChangedItems?.Invoke(this);
            }
        }

        public void BuildRelationsByForeignKeys()
        {
            persister.BuildRelationsByForeignKeys(ref items);
        }

        public void NotifyItemsRestored()
        {
            foreach (T item in items.Values)
                item.Restored();
        }

        public IEnumerator GetEnumerator()
        {
            return items.Values.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return items.Values.GetEnumerator();
        }

    }
}
