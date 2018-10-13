﻿using OpenSC.Model.Persistence;
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

            xmlElement.SetAttributeValue("id", item.ID);

            // Get fields
            foreach (FieldInfo fieldInfo in storedType.GetFields(memberLookupBindingFlags))
                storeValueOfFieldOrProperty(fieldInfo, ref item, ref xmlElement);

            // Get properties
            foreach (PropertyInfo propertyInfo in storedType.GetProperties(memberLookupBindingFlags))
                storeValueOfFieldOrProperty(propertyInfo, ref item, ref xmlElement);

            return xmlElement;

        }

        private T deserializeItem(XmlNode xmlElement)
        {

            T item = null;

            if (xmlElement.NodeType != XmlNodeType.Element)
                return null;

            foreach (ConstructorInfo ctor in storedType.GetConstructors()) {
                if (ctor.GetParameters().Length == 0)
                    try
                    {
                        item = (T)ctor.Invoke(new object[] { });
                    }
                    catch { }
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
                    persistedValues.Add(node.LocalName, node.InnerText);

            // Set fields
            foreach (FieldInfo fieldInfo in storedType.GetFields(memberLookupBindingFlags))
                restoreValueForFieldOrProperty(fieldInfo, persistedValues, ref item);

            // Set properties
            foreach (PropertyInfo propertyInfo in storedType.GetProperties(memberLookupBindingFlags))
                restoreValueForFieldOrProperty(propertyInfo, persistedValues, ref item);

            return item;

        }

        private void storeValueOfFieldOrProperty(MemberInfo memberInfo, ref T item, ref XElement xmlElement)
        {

            FieldInfo fieldInfo = memberInfo as FieldInfo;
            PropertyInfo propertyInfo = memberInfo as PropertyInfo;

            if ((fieldInfo == null) && (propertyInfo == null))
                return;

            PersistAsAttribute persistAsAttribute = memberInfo.GetCustomAttribute<PersistAsAttribute>();
            if (persistAsAttribute == null)
                return;

            if ((propertyInfo != null) && !(propertyInfo.CanRead && propertyInfo.CanWrite))
                return;

            object fieldValue = (fieldInfo != null) ? fieldInfo.GetValue(item) : propertyInfo.GetValue(item);

            IModel fieldValueAsIModel = fieldValue as IModel;
            if (fieldValueAsIModel != null)
                fieldValue = fieldValueAsIModel.ID;

            xmlElement.Add(new XElement(persistAsAttribute.TagName, fieldValue));

        }

        private void restoreValueForFieldOrProperty(MemberInfo memberInfo, Dictionary<string, object> persistedValues, ref T item)
        {

            FieldInfo fieldInfo = memberInfo as FieldInfo;
            PropertyInfo propertyInfo = memberInfo as PropertyInfo;

            if ((fieldInfo == null) && (propertyInfo == null))
                return;

            PersistAsAttribute persistAsAttribute = memberInfo.GetCustomAttribute<PersistAsAttribute>();
            if (persistAsAttribute == null)
                return;

            if ((propertyInfo != null) && !(propertyInfo.CanRead && propertyInfo.CanWrite))
                return;

            if (!persistedValues.TryGetValue(persistAsAttribute.TagName, out object value))
                return;

            Type type = (fieldInfo != null) ? fieldInfo.FieldType : propertyInfo.PropertyType;

            if (type.IsEnum)
            {
                value = Enum.Parse(type, value?.ToString());
            }
            else
            {
                try
                {
                    value = Convert.ChangeType(value, type);
                }
                catch { }
            }

            if (fieldInfo != null)
                fieldInfo.SetValue(item, value);
            else
                propertyInfo.SetValue(item, value);
 
        }

        public void BuildRelationsByForeignKeys()
        {

            foreach (T item in items.Values)
            {
                foreach (FieldInfo foreignKeyField in storedType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
                {

                    TempForeignKeyAttribute attr = foreignKeyField.GetCustomAttribute<TempForeignKeyAttribute>();
                    if (attr == null)
                        continue;

                    FieldInfo originalField = item.GetType().GetField(attr.OriginalFieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    if (originalField == null)
                        continue;

                    int? foreignKey = foreignKeyField.GetValue(item) as int?;
                    if (foreignKey == null)
                        continue;

                    object foreignObject = MasterDatabase.Instance.GetItem(attr.DatabaseName, (int)foreignKey);
                    originalField.SetValue(item, foreignObject);

                }
            }

        }

    }
}