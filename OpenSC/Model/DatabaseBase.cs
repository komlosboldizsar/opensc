using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model
{
    abstract class DatabaseBase<T>: IDatabaseBase
        where T: class, IModel
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

        private static readonly XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
        {
            CloseOutput = false,
            Indent = true,
            IndentChars = "\t"
        };

        public void Save()
        {
            
            XElement rootElement = new XElement("root");
            foreach (T item in items.Values)
                rootElement.Add(serializeItem(item));

            using(DatabaseFile outputFile = MasterDatabase.Instance.GetFileToWrite(this))
            using(XmlWriter writer = XmlWriter.Create(outputFile.Stream, xmlWriterSettings))
            {
                rootElement.WriteTo(writer);
            }

        }

        public void Load()
        {

            ChangingItems?.Invoke(this);

            items = new Dictionary<int, T>();

            using (DatabaseFile inputFile = MasterDatabase.Instance.GetFileToRead(this))
            {

                XmlDocument doc = new XmlDocument();
                doc.Load(inputFile.Stream);

                XmlNode root = doc.DocumentElement;

                if (root.LocalName != "root")
                    return;

                foreach(XmlNode node in root.ChildNodes)
                {
                    T item = deserializeItem(node);
                    if (item != null)
                        items.Add(item.ID, item);
                }

            }

            ChangedItems?.Invoke(this);

        }

        private const BindingFlags memberLookupBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        private static readonly Type storedType = typeof(T);

        private XElement serializeItem(T item)
        {

            XElement xmlElement = new XElement("item");

            // Get fields
            foreach (FieldInfo fieldInfo in storedType.GetFields(memberLookupBindingFlags))
            {
                PersistAsAttribute persistAsAttribute = fieldInfo.GetCustomAttribute<PersistAsAttribute>();
                if (persistAsAttribute != null)
                {
                    string persistAsName = persistAsAttribute.TagName;
                    var fieldValue = fieldInfo.GetValue(item);
                    xmlElement.Add(new XElement(persistAsName, fieldValue));
                }
            }

            // Get properties
            foreach (PropertyInfo propertyInfo in storedType.GetProperties(memberLookupBindingFlags))
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite)
                {
                    PersistAsAttribute persistAsAttribute = propertyInfo.GetCustomAttribute<PersistAsAttribute>();
                    if (persistAsAttribute != null)
                    {
                        string persistAsName = persistAsAttribute.TagName;
                        var fieldValue = propertyInfo.GetValue(item);
                        xmlElement.Add(new XElement(persistAsName, fieldValue));
                    }
                }
            }

            return xmlElement;

        }

        private T deserializeItem(XmlNode xmlElement)
        {

            T item = null;

            if (xmlElement.NodeType != XmlNodeType.Element)
                return null;

            foreach (ConstructorInfo ctor in storedType.GetConstructors()) {
                if (ctor.GetParameters().Length == 0)
                    item = (T)ctor.Invoke(new object[] { });
            }

            if (item == null)
                return null;

            string idStr = xmlElement.Attributes["id"].Value;
            if(!int.TryParse(idStr, out int id) || id <= 0)
                return null;
            item.ID = id;

            Dictionary<string, object> persistedValues = new Dictionary<string, object>();
            foreach (XmlNode node in xmlElement.ChildNodes)
                if (node.NodeType == XmlNodeType.Element)
                    persistedValues.Add(node.LocalName, node.Value);

            // Set fields
            foreach (FieldInfo fieldInfo in storedType.GetFields(memberLookupBindingFlags))
            {
                PersistAsAttribute persistAsAttribute = fieldInfo.GetCustomAttribute<PersistAsAttribute>();
                if (persistAsAttribute != null)
                {
                    string persistAsName = persistAsAttribute.TagName;
                    if (persistedValues.TryGetValue(persistAsName, out object value))
                        fieldInfo.SetValue(item, value);
                }
            }

            // Set properties
            foreach (PropertyInfo propertyInfo in storedType.GetProperties(memberLookupBindingFlags))
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite)
                {
                    PersistAsAttribute persistAsAttribute = propertyInfo.GetCustomAttribute<PersistAsAttribute>();
                    if (persistAsAttribute != null)
                    {
                        string persistAsName = persistAsAttribute.TagName;
                        if (persistedValues.TryGetValue(persistAsName, out object value))
                            propertyInfo.SetValue(item, value);
                    }
                }
            }

            return item;

        }

    }
}
