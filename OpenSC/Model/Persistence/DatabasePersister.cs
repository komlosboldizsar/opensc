using OpenSC.Model.Persistence;
using System;
using System.Collections;
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

        private const string ATTRIBUTE_ID = "id";
        private const string ATTRIBUTE_TYPE = "type";
        private const string UNDEFINED_ARRAY_ITEM_TAG = "arrayitem";

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

        #region Serialization
        private XElement serializeItem(T item)
        {

            XElement xmlElement = new XElement(itemTag);
            Type itemType = item.GetType();

            xmlElement.SetAttributeValue(ATTRIBUTE_ID, item.ID);
            if (isPolymorph)
                xmlElement.SetAttributeValue(ATTRIBUTE_TYPE, typeNameConverter.ConvertTypeToString(itemType));

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

        private void storeValueOfFieldOrProperty(MemberInfo memberInfo, ref T item, ref XElement xmlElement)
        {

            FieldInfo fieldInfo = memberInfo as FieldInfo;
            PropertyInfo propertyInfo = memberInfo as PropertyInfo;

            if ((fieldInfo == null) && (propertyInfo == null))
                return;

            if ((propertyInfo != null) && !(propertyInfo.CanRead && propertyInfo.CanWrite))
                return;

            if (isTemporaryForeignKeyField(memberInfo))
                return;

            string xmlTagName = getXmlTagNameForMember(memberInfo, item);
            if (xmlTagName == null)
                return;

            object fieldValue = (fieldInfo != null) ? fieldInfo.GetValue(item) : propertyInfo.GetValue(item);
            Type memberType = (fieldInfo != null) ? fieldInfo.FieldType : propertyInfo.PropertyType;

            object xmlElementInner = serializeValue(memberInfo, memberType, fieldValue);
            xmlElement.Add(new XElement(xmlTagName, xmlElementInner));

        }

        private object serializeValue(MemberInfo memberInfo, Type memberType, object item, int arrayDimension = 0)
        {

            if (item == null)
                return string.Empty;

            IModel itemAsImodel = item as IModel;
            if (itemAsImodel != null)
                return itemAsImodel.ID;

            if (memberType.IsArray && (item is Array))
            {
                Array array = item as Array;
                List<XElement> arrayElements = new List<XElement>();
                foreach (var element in array)
                    arrayElements.Add(serializeCollectionElement(memberInfo, memberType.GetElementType(), element, arrayDimension));
                return arrayElements;
            }

            if (Type.GetTypeCode(memberType) == TypeCode.Object)
            {
                IValueXmlSerializer serializer = GetSerializerForType(memberType);
                if (serializer == null)
                    return item.ToString();
                return serializer.SerializeItem(item);
            }
           
            return item.ToString();

        }

        private XElement serializeCollectionElement(MemberInfo memberInfo, Type memberType, object element, int arrayDimension)
        {
            string tagName = getXmlTagNameForMember(memberInfo, element, arrayDimension + 1);
            if (tagName == null)
                tagName = UNDEFINED_ARRAY_ITEM_TAG;
            object arrayElementValue = serializeValue(memberInfo, memberType, element, arrayDimension + 1);
            return new XElement(tagName, arrayElementValue);
        }
        #endregion

        #region Deserialization
        private T deserializeItem(XmlNode xmlElement)
        {

            T item = null;

            if (xmlElement.NodeType != XmlNodeType.Element)
                return null;

            Type type = typeof(T);
            if (isPolymorph)
            {
                string typeStr = xmlElement.Attributes[ATTRIBUTE_TYPE]?.Value;
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

            string idStr = xmlElement.Attributes[ATTRIBUTE_ID].Value;
            if (!int.TryParse(idStr, out int id) || id <= 0)
                return null;
            item.ID = id;

            Dictionary<string, XmlElement> persistedValues = new Dictionary<string, XmlElement>();
            foreach (XmlNode node in xmlElement.ChildNodes)
                if (node.NodeType == XmlNodeType.Element)
                    persistedValues[node.LocalName] = (XmlElement)node;

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

        private void restoreValueForFieldOrProperty(MemberInfo memberInfo, Dictionary<string, XmlElement> persistedValues, ref T item)
        {

            FieldInfo fieldInfo = memberInfo as FieldInfo;
            PropertyInfo propertyInfo = memberInfo as PropertyInfo;

            if ((fieldInfo == null) && (propertyInfo == null))
                return;

            if ((propertyInfo != null) && !(propertyInfo.CanRead && propertyInfo.CanWrite))
                return;

            if (isAssociationField(memberInfo))
                return;

            string xmlTagName = getXmlTagNameForMember(memberInfo, item);
            if (xmlTagName == null)
                return;

            if (!persistedValues.TryGetValue(xmlTagName, out XmlElement xmlElement))
                return;

            Type type = (fieldInfo != null) ? fieldInfo.FieldType : propertyInfo.PropertyType;

            object value = deserializeXmlElement(type, xmlElement);

            try
            {
                if (fieldInfo != null)
                    fieldInfo.SetValue(item, value);
                else
                    propertyInfo.SetValue(item, value);
            }
            catch { }

        }

        private object deserializeXmlElement(Type memberType, XmlElement xmlElement)
        {

            if (xmlElement.IsEmpty)
                return null;

            if (memberType.IsArray)
            {
                List<XmlElement> childElements = new List<XmlElement>();
                foreach (XmlNode childNode in xmlElement.ChildNodes)
                    if (childNode.NodeType == XmlNodeType.Element)
                        childElements.Add((XmlElement)childNode);
                return deserializeArray(memberType, childElements);
            }

            if (memberType.IsEnum)
                return Enum.Parse(memberType, xmlElement.InnerText);

            if (Type.GetTypeCode(memberType) == TypeCode.Object)
            {
                IValueXmlSerializer serializer = GetSerializerForType(memberType);
                if (serializer == null)
                    return xmlElement.InnerText;
                return serializer.DeserializeItem(xmlElement.OfType<XmlElement>().FirstOrDefault());
            }

            return Convert.ChangeType(xmlElement.InnerText, memberType);

        }

        private object deserializeArray(Type memberType, List<XmlElement> childElements)
        {

            int childElementCount = childElements.Count;

            if (memberType == typeof(int[]))
            {
                int[] intArray = new int[childElementCount];
                for (int i = 0; i < childElementCount; i++)
                    intArray[i] = (int)deserializeXmlElement(typeof(int), childElements[i]);
                return intArray;
            }

            object[] array = (object[])Activator.CreateInstance(memberType, new object[] { childElementCount });
            for (int i = 0; i < childElementCount; i++)
                array[i] = deserializeXmlElement(memberType.GetElementType(), childElements[i]);
            return array;

        }
        #endregion

        #region Relations/associations
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

            object foreignKeys = foreignKeyField.GetValue(item);
            if (foreignKeys == null)
                return;

            object foreignObjects = getAssociatedObjects(foreignKeyField.FieldType, originalField.FieldType, attr.DatabaseName, foreignKeys);
            originalField.SetValue(item, foreignObjects);

        }

        private object getAssociatedObjects(Type memberType, Type originalType, string databaseName, object foreignKeys)
        {

            if (memberType.IsArray && (memberType.GetElementType() != typeof(int)))
            {
                object[] foreignKeysArray = foreignKeys as object[];
                if (foreignKeysArray == null)
                    return null;
                object[] associatedObjects = (object[])Activator.CreateInstance(originalType, new object[] { foreignKeysArray.Length });
                for (int i = 0; i < foreignKeysArray.Length; i++)
                    associatedObjects[i] = getAssociatedObjects(memberType.GetElementType(), originalType.GetElementType(), databaseName, foreignKeysArray[i]);
                return Convert.ChangeType(associatedObjects, originalType);
            }
            else if(memberType.IsArray)
            {
                int[] foreignKeysArray = foreignKeys as int[];
                if (foreignKeysArray == null)
                    return null;
                object[] associatedObjects = (object[])Activator.CreateInstance(originalType, new object[] { foreignKeysArray.Length });
                for (int i = 0; i < foreignKeysArray.Length; i++)
                {
                    int? foreignKeyInt = foreignKeysArray[i] as int?;
                    if(foreignKeyInt != null)
                        associatedObjects[i] = MasterDatabase.Instance.GetItem(databaseName, (int)foreignKeyInt);
                }
                return associatedObjects;
            }
            else
            {
                int? foreignKeyInt = foreignKeys as int?;
                if (foreignKeyInt == null)
                    return null;
                return MasterDatabase.Instance.GetItem(databaseName, (int)foreignKeyInt);
            }

        }

        #endregion

        private string getXmlTagNameForMember(MemberInfo memberInfo, object item, int dimension = 0)
        {

            IEnumerable<PersistAsAttribute> persistAsAttributes = memberInfo.GetCustomAttributes<PersistAsAttribute>();
            foreach (PersistAsAttribute attr in persistAsAttributes)
                if (attr.Dimension == dimension)
                    return attr.TagName;

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

        private bool isTemporaryForeignKeyField(MemberInfo memberInfo)
        {
            return (memberInfo.GetCustomAttributes<TempForeignKeyAttribute>().Count() > 0);
        }

        private bool isAssociationField(MemberInfo memberInfo)
        {
            FieldInfo fieldInfo = memberInfo as FieldInfo;
            PropertyInfo propertyInfo = memberInfo as PropertyInfo;
            Type type = (fieldInfo != null) ? fieldInfo.FieldType : propertyInfo.PropertyType;
            return (getArrayBaseType(type).GetInterfaces().Any(iface => (iface == typeof(IModel))));
        }

        private Type getArrayBaseType(Type type)
        {
            if (type.IsArray)
                return getArrayBaseType(type.GetElementType());
            return type;
        }

        private enum Workflow
        {
            Load,
            Save
        }

        #region Serializer
        private static IValueXmlSerializer[] commonSerializers = new IValueXmlSerializer[]
        {
            new ColorXmlSerializer()
        };

        private static Dictionary<Type, IValueXmlSerializer> registeredSerializers = null;

        private static Dictionary<Type, IValueXmlSerializer> Serializers
        {
            get
            {
                if (registeredSerializers == null)
                {
                    registeredSerializers = new Dictionary<Type, IValueXmlSerializer>();
                    foreach (IValueXmlSerializer serializer in commonSerializers)
                        registeredSerializers.Add(serializer.Type, serializer);
                }
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
