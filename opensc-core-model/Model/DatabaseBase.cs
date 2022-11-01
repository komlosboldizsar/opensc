using OpenSC.Logger;
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

    public abstract class DatabaseBase<T>: ObservableEnumerableAdapter<T, KeyValuePair<int, T>>, IDatabaseBase
        where T: class, IModel
    {

        private const string LOG_TAG_DBNAME_PLACEHOLDER = "@dbname";
        private const string LOG_TAG = "Database: " + LOG_TAG_DBNAME_PLACEHOLDER;
        private const string LOG_TAG_DBNAME_UNKNOWN = "(unknown)";

        private readonly string SPECIFIC_LOG_TAG = "Database: ?";
        
        public delegate void AddedItemDelegate(DatabaseBase<T> database, T item);
        public event AddedItemDelegate AddedItem;

        public delegate void RemovedItemDelegate(DatabaseBase<T> database, T item);
        public event RemovedItemDelegate RemovedItem;

        public delegate void ChangedItemsDelegate(DatabaseBase<T> database);
        public event ChangedItemsDelegate ChangedItems;

        protected ObservableDictionary<int, T> items = new ObservableDictionary<int, T>();
        public int Count => items.Count;
        public T this[int id] => GetTById(id);
        public bool IsReadOnly => false;
        protected override IEnumerator<T> getEnumerator() => items.Values.GetEnumerator();
        protected override T convertAdaptee(KeyValuePair<int, T> adaptee) => adaptee.Value;

        public string Name { get; init; }

        public DatabaseBase()
        {
            Adaptee = items;
            Name = GetType().GetAttribute<DatabaseNameAttribute>()?.Name;
            if (Name == null)
                throw new Exception("A database name must be provided through an 'DatabaseName' attribute!");
            persister  = new DatabasePersister<T>(this);
            SPECIFIC_LOG_TAG = LOG_TAG.Replace(LOG_TAG_DBNAME_PLACEHOLDER, this.GetName() ?? LOG_TAG_DBNAME_UNKNOWN);
        }

        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException();
            if (item.ID <= 0)
                throw new Exception();
            if (items.ContainsKey(item.ID))
                throw new Exception();
            items.Add(item.ID, item);
            LogDispatcher.V(SPECIFIC_LOG_TAG, "An item added with ID: " + item.ID);
            AddedItem?.Invoke(this, item);
            ChangedItems?.Invoke(this);
            item.ModelAfterUpdate += itemAfterUpdateHandler;
            Save();
            afterAdd(item);
        }

        protected virtual void afterAdd(T item) { }

        public bool Remove(T item)
        {
            if (item == null)
                throw new ArgumentNullException();
            if (!items.RemoveByKey(item.ID))
                return false;
            LogDispatcher.V(SPECIFIC_LOG_TAG, "An item removed with ID: " + item.ID);
            RemovedItem?.Invoke(this, item);
            ChangedItems?.Invoke(this);
            item.ModelAfterUpdate -= itemAfterUpdateHandler;
            Save();
            afterRemove(item);
            return true;
        }

        protected virtual void afterRemove(T item) { }

        private void itemAfterUpdateHandler(IModel model) => ItemUpdated(model as T);

        public void ItemUpdated(T item)
        {
            if (items.Values.Contains(item))
            {
                Save();
                afterItemUpdate(item);
            }
        }

        protected virtual void afterItemUpdate(T item) { }

        public T GetTById(int id)
        {
            if (items.TryGetValue(id, out T value))
                return value;
            return null;            
        }

        public object GetById(int id) => GetTById(id);

        public bool CanIdBeUsedForItem(int id, object forItem)
        {
            if (!(forItem is T))
                return false;
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

        public delegate void SavedDelegate(DatabaseBase<T> database);
        public event SavedDelegate Saved;

        public void Save()
        {
            persister.Save();
            LogDispatcher.I(SPECIFIC_LOG_TAG, "Saved to file.");
            afterSave();
            Saved?.Invoke(this);
        }

        protected virtual void afterSave() { }

        public delegate void LoadedDelegate(DatabaseBase<T> database);
        public event LoadedDelegate Loaded;

        public void Load()
        {
            items.Clear();
            var loadedItems = persister.Load();
            if (loadedItems != null)
            {
                foreach (KeyValuePair<int, T> loadedItem in loadedItems)
                {
                    items.Add(loadedItem.Key, loadedItem.Value);
                    loadedItem.Value.ModelAfterUpdate += itemAfterUpdateHandler;
                }
                LogDispatcher.I(SPECIFIC_LOG_TAG, "Loaded from file.");
                afterLoad();
                Loaded?.Invoke(this);
                ChangedItems?.Invoke(this);
            }
        }

        protected virtual void afterLoad() { }

        public void BuildRelationsByForeignKeys() => persister.BuildRelationsByForeignKeys();

        public void NotifyItemsRestoredOwnFields()
        {
            foreach (T item in items.Values)
                item.RestoredOwnFields();
        }

        public void NotifyItemsRestoredBasicRelations()
        {
            foreach (T item in items.Values)
                item.RestoredBasicRelations();
        }

        public void RequestRestoreCustomRelations()
        {
            foreach (T item in items.Values)
                item.RestoreCustomRelations();
        }

        public void NotifyItemsTotallyRestored()
        {
            foreach (T item in items.Values)
                item.TotallyRestored();
        }

    }

}
