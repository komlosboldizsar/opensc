﻿using OpenSC.Logger;
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

        private const string LOG_TAG_DBNAME_PLACEHOLDER = "@dbname";
        private const string LOG_TAG = "Database: " + LOG_TAG_DBNAME_PLACEHOLDER;
        private const string LOG_TAG_DBNAME_UNKNOWN = "(unknown)";

        private readonly string SPECIFIC_LOG_TAG = "Database: ?";
        
        public delegate void AddedItemDelegate(DatabaseBase<T> database, T item);
        public event AddedItemDelegate AddedItem;
        public event ObservableListItemAddedDelegate ItemAdded;
        
        public delegate void RemovedItemDelegate(DatabaseBase<T> database, T item);
        public event RemovedItemDelegate RemovedItem;
        public event ObservableListItemRemovedDelegate ItemRemoved;
        
        public delegate void ChangedItemsDelegate(DatabaseBase<T> database);
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
            SPECIFIC_LOG_TAG = LOG_TAG.Replace(LOG_TAG_DBNAME_PLACEHOLDER, this.GetName() ?? LOG_TAG_DBNAME_UNKNOWN);
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
            items.Add(item.ID, item);

            LogDispatcher.V(SPECIFIC_LOG_TAG, "An item added with ID: " + item.ID);

            AddedItem?.Invoke(this, item);
            ChangedItems?.Invoke(this);

            ItemAdded?.Invoke();
            ItemsChanged?.Invoke();

            item.ModelAfterUpdate += itemAfterUpdateHandler;

            Save();

            afterAdd(item);

        }

        protected virtual void afterAdd(T item)
        { }

        public bool Remove(T item)
        {

            // Validate argument
            if (item == null)
                throw new ArgumentNullException();

            if (!items.ContainsValue(item))
                return false;

            // Remove element
            items.Remove(item.ID);

            LogDispatcher.V(SPECIFIC_LOG_TAG, "An item removed with ID: " + item.ID);

            RemovedItem?.Invoke(this, item);
            ChangedItems?.Invoke(this);

            ItemRemoved?.Invoke();
            ItemsChanged?.Invoke();

            item.ModelAfterUpdate -= itemAfterUpdateHandler;

            Save();

            afterRemove(item);

            return true;

        }

        protected virtual void afterRemove(T item)
        { }

        private void itemAfterUpdateHandler(IModel model)
            => ItemUpdated(model as T);

        public void ItemUpdated(T item)
        {
            if (items.ContainsValue(item))
            {
                Save();
                afterItemUpdate(item);
            }
        }

        protected virtual void afterItemUpdate(T item)
        { }

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
            LogDispatcher.I(SPECIFIC_LOG_TAG, "Saved to file.");
            afterSave();
        }

        protected virtual void afterSave()
        { }

        public void Load()
        {
            items.Clear();
            var loadedItems = persister.Load();
            if (loadedItems != null)
            {
                items = loadedItems;
                LogDispatcher.I(SPECIFIC_LOG_TAG, "Loaded from file.");
                afterLoad();
                ChangedItems?.Invoke(this);
            }
        }
        protected virtual void afterLoad()
        { }

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
