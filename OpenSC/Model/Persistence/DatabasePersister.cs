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

namespace OpenSC.Model.Persistence
{
    class DatabasePersister<T>
        where T: class, IModel
    {

        private IDatabaseBase database;

        private bool isPolymorph = false;
        private ITypeNameConverter typeNameConverter;

        private string rootTag = "root";
        private string itemTag = "item";

        public DatabasePersister(IDatabaseBase database)
        {

            this.database = database;

            PolymorphDatabaseAttribute polymorphAttr = database.GetType().GetCustomAttribute<PolymorphDatabaseAttribute>();
            if (polymorphAttr != null)
            {
                isPolymorph = true;
                typeNameConverter = polymorphAttr.Converter;
            }

            XmlTagNamesAttribute tagNameAttr = database.GetType().GetCustomAttribute<XmlTagNamesAttribute>();
            if(tagNameAttr != null)
            {
                rootTag = tagNameAttr.RootTag;
                itemTag = tagNameAttr.ItemTag;
            }

        }

        private static readonly XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
        {
            CloseOutput = false,
            Indent = true,
            IndentChars = "\t"
        };

        public void Save(Dictionary<int, T> items)
        {

            XElement rootElement = new XElement(rootTag);
            foreach (T item in items.Values)
                rootElement.Add(serializeItem(item));

            using (DatabaseFile outputFile = MasterDatabase.Instance.GetFileToWrite(database))
            using (XmlWriter writer = XmlWriter.Create(outputFile.Stream, xmlWriterSettings))
            {
                rootElement.WriteTo(writer);
            }

        }

        public Dictionary<int, T> Load()
        {

            var items = new Dictionary<int, T>();

            try
            {
                using (DatabaseFile inputFile = MasterDatabase.Instance.GetFileToRead(database))
                {

                    XmlDocument doc = new XmlDocument();
                    doc.Load(inputFile.Stream);

                    XmlNode root = doc.DocumentElement;

                    if (root.LocalName != rootTag)
                        return null;

                    foreach (XmlNode node in root.ChildNodes)
                    {
                        T item = deserializeItem(node);
                        if (item != null)
                            items.Add(item.ID, item);
                    }

                }
            }
            catch { }

            return items;

        }

        private const BindingFlags memberLookupBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        private static readonly Type storedType = typeof(T);

        private XElement serializeItem(T item)
        {

            XElement xmlElement = new XElement(itemTag);
            Type itemType = item.GetType();

            xmlElement.SetAttributeValue("id", item.ID);
            if (isPolymorph)
                xmlElement.SetAttributeValue("type", typeNameConverter.ConvertTypeToString(itemType));

            // Get fields
            foreach (FieldInfo fieldInfo in storedType.GetFields(memberLookupBindingFlags))
                storeValueOfFieldOrProperty(fieldInfo, ref item, ref xmlElement);

            if (isPolymorph)
                foreach (FieldInfo fieldInfo in itemType.GetFields(memberLookupBindingFlags))
                    storeValueOfFieldOrProperty(fieldInfo, ref item, ref xmlElement);

            // Get properties
            foreach (PropertyInfo propertyInfo in storedType.GetProperties(memberLookupBindingFlags))
                storeValueOfFieldOrProperty(propertyInfo, ref item, ref xmlElement);

            if (isPolymorph)
                foreach (PropertyInfo propertyInfo in itemType.GetProperties(memberLookupBindingFlags))
                    storeValueOfFieldOrProperty(propertyInfo, ref item, ref xmlElement);

            return xmlElement;

        }

        private T deserializeItem(XmlNode xmlElement)
        {

            T item = null;

            if (xmlElement.NodeType != XmlNodeType.Element)
                return null;

            Type type = typeof(T);
            if(isPolymorph)
            {
                string typeStr = xmlElement.Attributes["type"]?.Value;
                type = typeNameConverter.ConvertStringToType(typeStr);
                if (type == null)
                    return null;
            }

            foreach (ConstructorInfo ctor in type.GetConstructors())
            {
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
            if (!int.TryParse(idStr, out int id) || id <= 0)
                return null;
            item.ID = id;

            Dictionary<string, object> persistedValues = new Dictionary<string, object>();
            foreach (XmlNode node in xmlElement.ChildNodes)
                if (node.NodeType == XmlNodeType.Element)
                    persistedValues.Add(node.LocalName, node.InnerText);

            // Set fields
            foreach (FieldInfo fieldInfo in storedType.GetFields(memberLookupBindingFlags))
                restoreValueForFieldOrProperty(fieldInfo, persistedValues, ref item);

            if (isPolymorph)
                foreach (FieldInfo fieldInfo in type.GetFields(memberLookupBindingFlags))
                    restoreValueForFieldOrProperty(fieldInfo, persistedValues, ref item);

            // Set properties
            foreach (PropertyInfo propertyInfo in storedType.GetProperties(memberLookupBindingFlags))
                restoreValueForFieldOrProperty(propertyInfo, persistedValues, ref item);

            if (isPolymorph)
                foreach (PropertyInfo propertyInfo in type.GetProperties(memberLookupBindingFlags))
                    restoreValueForFieldOrProperty(propertyInfo, persistedValues, ref item);

            return item;

        }

        private void storeValueOfFieldOrProperty(MemberInfo memberInfo, ref T item, ref XElement xmlElement)
        {

            FieldInfo fieldInfo = memberInfo as FieldInfo;
            PropertyInfo propertyInfo = memberInfo as PropertyInfo;

            if ((fieldInfo == null) && (propertyInfo == null))
                return;

            if ((propertyInfo != null) && !(propertyInfo.CanRead && propertyInfo.CanWrite))
                return;

            string xmlTagName = getXmlTagNameForMember(memberInfo, item, Workflow.Save);
            if (xmlTagName == null)
                return;

            object fieldValue = (fieldInfo != null) ? fieldInfo.GetValue(item) : propertyInfo.GetValue(item);

            IModel fieldValueAsIModel = fieldValue as IModel;
            if (fieldValueAsIModel != null)
                fieldValue = fieldValueAsIModel.ID;

            xmlElement.Add(new XElement(xmlTagName, fieldValue));

        }

        private void restoreValueForFieldOrProperty(MemberInfo memberInfo, Dictionary<string, object> persistedValues, ref T item)
        {

            FieldInfo fieldInfo = memberInfo as FieldInfo;
            PropertyInfo propertyInfo = memberInfo as PropertyInfo;

            if ((fieldInfo == null) && (propertyInfo == null))
                return;

            if ((propertyInfo != null) && !(propertyInfo.CanRead && propertyInfo.CanWrite))
                return;

            string xmlTagName = getXmlTagNameForMember(memberInfo, item, Workflow.Load);
            if (xmlTagName == null)
                return;

            if (!persistedValues.TryGetValue(xmlTagName, out object value))
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

            try
            {
                if (fieldInfo != null)
                    fieldInfo.SetValue(item, value);
                else
                    propertyInfo.SetValue(item, value);
            }
            catch { }

        }

        public void BuildRelationsByForeignKeys(ref Dictionary<int, T> items)
        {

            FieldInfo[] baseFields = storedType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (T item in items.Values)
            {
                foreach (FieldInfo foreignKeyField in baseFields)
                    buildRelationForField(item, foreignKeyField, ref items);

                if (isPolymorph)
                {
                    FieldInfo[] extendedFields = item.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    foreach (FieldInfo foreignKeyField in extendedFields)
                        buildRelationForField(item, foreignKeyField, ref items);
                }
            }

        }

        private void buildRelationForField(T item, FieldInfo foreignKeyField, ref Dictionary<int, T> items)
        {

            TempForeignKeyAttribute attr = foreignKeyField.GetCustomAttribute<TempForeignKeyAttribute>();
            if (attr == null)
                return;

            FieldInfo originalField = storedType.GetField(attr.OriginalFieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (isPolymorph && (originalField == null))
                originalField = item.GetType().GetField(attr.OriginalFieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (originalField == null)
                return;

            int? foreignKey = foreignKeyField.GetValue(item) as int?;
            if (foreignKey == null)
                return;

            object foreignObject = MasterDatabase.Instance.GetItem(attr.DatabaseName, (int)foreignKey);
            originalField.SetValue(item, foreignObject);

        }

        private string getXmlTagNameForMember(MemberInfo memberInfo, T item, Workflow workflow, int dimension = 0)
        {

            IEnumerable<PersistAsAttribute> persistAsAttributes = memberInfo.GetCustomAttributes<PersistAsAttribute>();
            foreach (PersistAsAttribute attr in persistAsAttributes)
                if (attr.Dimension == dimension)
                    return attr.TagName;

            if (workflow == Workflow.Save)
                return null;

            TempForeignKeyAttribute tempForeignKeyAttribute = memberInfo.GetCustomAttribute<TempForeignKeyAttribute>();
            if (tempForeignKeyAttribute == null)
                return null;

            MemberInfo[] originalMemberInfo = storedType.GetMember(tempForeignKeyAttribute.OriginalFieldName, memberLookupBindingFlags);
            if (isPolymorph && ((originalMemberInfo == null) || (originalMemberInfo.Length == 0)))
                originalMemberInfo = item.GetType().GetMember(tempForeignKeyAttribute.OriginalFieldName, memberLookupBindingFlags);
            if (originalMemberInfo.Length == 0)
                return null;

            return originalMemberInfo[0].GetCustomAttribute<PersistAsAttribute>()?.TagName;

        }

        private enum Workflow
        {
            Load,
            Save
        }

        #region Serializer
        private static IValueXmlSerializer[] commonSerializers = new IValueXmlSerializer[]
        { };

        private static Dictionary<Type, IValueXmlSerializer> registeredSerializers = null;

        private static Dictionary<Type, IValueXmlSerializer> Serializers
        {
            get
            {
                if(registeredSerializers == null)
                    registeredSerializers = new Dictionary<Type, IValueXmlSerializer>();
                foreach (IValueXmlSerializer serializer in commonSerializers)
                    registeredSerializers.Add(serializer.Type, serializer);
                return registeredSerializers;
            }
        }

        public static void RegisterSerializer(IValueXmlSerializer serializer)
        {
            Serializers.Add(serializer.Type, serializer);
        }

        private static IValueXmlSerializer GetSerializerForType(Type type)
        {
            if (!Serializers.TryGetValue(type, out IValueXmlSerializer foundSerializer))
                return null;
            return foundSerializer;
        }
        #endregion

    }
}
