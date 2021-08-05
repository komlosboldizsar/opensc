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
        where T : class, IModel
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
            if (tagNameAttr != null)
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
            {
                storeValueOfFieldOrProperty(fieldInfo, ref item, ref xmlElement);
                Console.WriteLine("{0}::{1}", storedType.Name, fieldInfo.Name);
            }

            if (isPolymorph)
            {
                Type currentType = itemType;
                do
                {
                    foreach (FieldInfo fieldInfo in currentType.GetFields(memberLookupBindingFlags))
                        storeValueOfFieldOrProperty(fieldInfo, ref item, ref xmlElement);
                    currentType = currentType.BaseType;
                } while (!currentType.Equals(storedType));
            }


            // Get properties
            foreach (PropertyInfo propertyInfo in storedType.GetProperties(memberLookupBindingFlags))
                storeValueOfFieldOrProperty(propertyInfo, ref item, ref xmlElement);

            if (isPolymorph)
            { 
                Type currentType = itemType;
                do
                {
                    foreach (PropertyInfo propertyInfo in currentType.GetProperties(memberLookupBindingFlags))
                        storeValueOfFieldOrProperty(propertyInfo, ref item, ref xmlElement);
                    currentType = currentType.BaseType;
                } while (!currentType.Equals(storedType));
            }

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

            PersistAsAttribute persistData = getPersistDataForMember(memberInfo, item);
            if (persistData == null)
                return;

            object fieldValue = (fieldInfo != null) ? fieldInfo.GetValue(item) : propertyInfo.GetValue(item);
            Type memberType = (fieldInfo != null) ? fieldInfo.FieldType : propertyInfo.PropertyType;

            object xmlElementInner = serializeValue(memberInfo, memberType, fieldValue, item);
            xmlElement.Add(new XElement(persistData.TagName, xmlElementInner));

        }

        private object serializeValue(MemberInfo memberInfo, Type memberType, object item, object parentItem, int arrayDimension = 0)
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
                    arrayElements.Add(serializeCollectionElement(memberInfo, memberType.GetElementType(), element, parentItem, arrayDimension));
                return arrayElements;
            }

            if (Type.GetTypeCode(memberType) == TypeCode.Object)
            {

                Type itemType = item.GetType();
                Type serializeAsType = memberType;

                PersistSubclassAttribute persistSubclassAttribute = memberInfo.GetCustomAttributes<PersistSubclassAttribute>().FirstOrDefault();
                if (persistSubclassAttribute != null) // should check if given type is subclass of member type
                    serializeAsType = persistSubclassAttribute.SubclassType;
                
                PolymorphFieldAttribute polymorphFieldAttribute = memberInfo.GetCustomAttributes<PolymorphFieldAttribute>().FirstOrDefault();
                Dictionary<Type, string> typeStringDictionary = null;
                string itemTypeString = null;
                if (polymorphFieldAttribute != null)
                {
                    MethodInfo typeStringDictionaryGetterMethodInfo = parentItem.GetType().GetMethod(polymorphFieldAttribute.TypeStringDictionaryGetterName, memberLookupBindingFlags);
                    typeStringDictionary = typeStringDictionaryGetterMethodInfo.Invoke(parentItem, new object[] { }) as Dictionary<Type, string>;
                    if (typeStringDictionary?.TryGetValue(itemType, out itemTypeString) == true)
                        serializeAsType = itemType;
                }

                IValueXmlSerializer serializer = GetSerializerForType(serializeAsType);
                if (serializer == null)
                    return item.ToString();
                XElement serializedItem = serializer.SerializeItem(item, parentItem);
                if ((polymorphFieldAttribute?.TypeAttributeName != null) && (itemTypeString != null))
                    serializedItem.SetAttributeValue(polymorphFieldAttribute.TypeAttributeName, itemTypeString);
                return serializedItem;

            }
           
            return item.ToString();

        }

        private XElement serializeCollectionElement(MemberInfo memberInfo, Type memberType, object element, object parentItem, int arrayDimension)
        {

            PersistAsAttribute persistData = getPersistDataForMember(memberInfo, element, arrayDimension + 1);
            if (persistData == null)
                persistData = new PersistAsAttribute(UNDEFINED_ARRAY_ITEM_TAG, 0);
            object arrayElementValue = serializeValue(memberInfo, memberType, element, parentItem, arrayDimension + 1);

            if (!(arrayElementValue is XElement) || (persistData.TagName != null)) // packing
                return new XElement(persistData.TagName, arrayElementValue);

            return (XElement)arrayElementValue;

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
            {
                Type currentType = type;
                do
                {
                    foreach (FieldInfo fieldInfo in currentType.GetFields(memberLookupBindingFlags))
                        restoreValueForFieldOrProperty(fieldInfo, persistedValues, ref item);
                    currentType = currentType.BaseType;
                } while (!currentType.Equals(storedType));
            }

            // Set properties
            foreach (PropertyInfo propertyInfo in storedType.GetProperties(memberLookupBindingFlags))
                restoreValueForFieldOrProperty(propertyInfo, persistedValues, ref item);

            if (isPolymorph)
            {
                Type currentType = type;
                do
                {
                    foreach (PropertyInfo propertyInfo in currentType.GetProperties(memberLookupBindingFlags))
                        restoreValueForFieldOrProperty(propertyInfo, persistedValues, ref item);
                    currentType = currentType.BaseType;
                } while (!currentType.Equals(storedType));
            }

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

            PersistAsAttribute persistData = getPersistDataForMember(memberInfo, item);
            if (persistData == null)
                return;

            if (!persistedValues.TryGetValue(persistData.TagName, out XmlElement xmlElement))
                return;

            Type type = (fieldInfo != null) ? fieldInfo.FieldType : propertyInfo.PropertyType;

            object value = deserializeXmlElement(memberInfo, type, xmlElement, item);

            try
            {
                if (fieldInfo != null)
                    fieldInfo.SetValue(item, value);
                else
                    propertyInfo.SetValue(item, value);
            }
            catch { }

        }

        private static readonly Type[] PRIMITIVE_TYPES_NULL_IS_0 = new Type[]
        {
            typeof(int),
            typeof(float),
            typeof(double),
            typeof(decimal)
        };

        private object deserializeXmlElement(MemberInfo memberInfo, Type memberType, XmlElement xmlElement, object parentItem, int arrayDimension = 0)
        {

            if (memberType == typeof(string))
                return xmlElement.InnerText;

            if ((xmlElement.InnerText == string.Empty) && PRIMITIVE_TYPES_NULL_IS_0.Contains(memberType))
                return 0;

            if (memberType.IsArray)
            {
                List<XmlElement> childElements = new List<XmlElement>();
                foreach (XmlNode childNode in xmlElement.ChildNodes)
                    if (childNode.NodeType == XmlNodeType.Element)
                        childElements.Add((XmlElement)childNode);
                return deserializeArray(memberInfo, memberType, childElements, parentItem, arrayDimension);
            }

            if (memberType.IsEnum)
                return Enum.Parse(memberType, xmlElement.InnerText);

            Type deserializeAsType = memberType;
            if (Type.GetTypeCode(memberType) == TypeCode.Object)
            {
                
                PersistSubclassAttribute persistSubclassAttribute = memberInfo.GetCustomAttributes<PersistSubclassAttribute>().FirstOrDefault();
                if (persistSubclassAttribute != null) // should check if given type is subclass of member type
                    deserializeAsType = persistSubclassAttribute.SubclassType;

                PolymorphFieldAttribute polymorphFieldAttribute = memberInfo.GetCustomAttributes<PolymorphFieldAttribute>().FirstOrDefault();
                Dictionary<Type, string> typeStringDictionary = null;
                if (polymorphFieldAttribute != null)
                {
                    string itemTypeString = xmlElement.GetAttribute(polymorphFieldAttribute.TypeAttributeName);
                    MethodInfo typeStringDictionaryGetterMethodInfo = parentItem.GetType().GetMethod(polymorphFieldAttribute.TypeStringDictionaryGetterName, memberLookupBindingFlags);
                    typeStringDictionary = typeStringDictionaryGetterMethodInfo.Invoke(parentItem, new object[] { }) as Dictionary<Type, string>;
                    KeyValuePair<Type, string> foundTypeData = typeStringDictionary.FirstOrDefault(kvp => (kvp.Value == itemTypeString));
                    if (foundTypeData.Key != null)
                        deserializeAsType = foundTypeData.Key;
                }

                IValueXmlSerializer serializer = GetSerializerForType(deserializeAsType);
                if (serializer == null)
                    return xmlElement.InnerText;
                PersistAsAttribute persistData = getPersistDataForMember(memberInfo, null, arrayDimension);
                XmlElement itemToDeserialize = xmlElement;
                if (persistData.TagName != null)
                    itemToDeserialize = itemToDeserialize.OfType<XmlElement>().FirstOrDefault();
                return serializer.DeserializeItem(itemToDeserialize, parentItem);
            }

            return Convert.ChangeType(xmlElement.InnerText, deserializeAsType);

        }

        private object deserializeArray(MemberInfo memberInfo, Type memberType, List<XmlElement> childElements, object parentItem, int arrayDimension)
        {

            int childElementCount = childElements.Count;
            Type elementType = memberType.GetElementType();

            if (Type.GetTypeCode(elementType) != TypeCode.Object)
            {
                Array typedArray = Array.CreateInstance(elementType, childElementCount);
                for (int i = 0; i < childElementCount; i++)
                    typedArray.SetValue(deserializeXmlElement(memberInfo, elementType, childElements[i], arrayDimension + 1), i);
                return typedArray;
            }

            object[] array = (object[])Activator.CreateInstance(memberType, new object[] { childElementCount });
            for (int i = 0; i < childElementCount; i++)
                array[i] = deserializeXmlElement(memberInfo, memberType.GetElementType(), childElements[i], parentItem, arrayDimension + 1);
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
                    Type currentType = item.GetType();
                    do
                    {
                        FieldInfo[] extendedFields = currentType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                        foreach (FieldInfo foreignKeyField in extendedFields)
                            buildRelationForField(item, foreignKeyField, ref items);
                        currentType = currentType.BaseType;
                    } while (!currentType.Equals(storedType));
                }

                item.RestoreCustomRelations();
            }

        }

        private void buildRelationForField(T item, FieldInfo foreignKeyField, ref Dictionary<int, T> items)
        {

            
            TempForeignKeyAttribute attr = foreignKeyField.GetCustomAttribute<TempForeignKeyAttribute>();
            if (attr == null)
                return;

            Console.WriteLine("{0} -- {1}", item.GetType().Name, foreignKeyField.Name);

            FieldInfo originalField = storedType.GetField(attr.OriginalFieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (isPolymorph && (originalField == null))
            {
                Type currentType = item.GetType();
                do
                {
                    originalField = currentType.GetField(attr.OriginalFieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    currentType = currentType.BaseType;
                } while (!currentType.Equals(storedType) && (originalField == null));
            }
            if (originalField == null)
                return;

            object foreignKeys = foreignKeyField.GetValue(item);
            if (foreignKeys == null)
                return;
            Console.WriteLine(foreignKeys.ToString());

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

        private PersistAsAttribute getPersistDataForMember(MemberInfo memberInfo, object item, int dimension = 0)
        {

            IEnumerable<PersistAsAttribute> persistAsAttributes = memberInfo.GetCustomAttributes<PersistAsAttribute>();
            foreach (PersistAsAttribute attr in persistAsAttributes)
                if (attr.Dimension == dimension)
                    return attr;

            TempForeignKeyAttribute tempForeignKeyAttribute = memberInfo.GetCustomAttribute<TempForeignKeyAttribute>();
            if (tempForeignKeyAttribute == null)
                return null;

            MemberInfo[] originalMemberInfo = storedType.GetMember(tempForeignKeyAttribute.OriginalFieldName, memberLookupBindingFlags);
            if (isPolymorph && ((originalMemberInfo == null) || (originalMemberInfo.Length == 0)))
            {
                Type currentType = item.GetType();
                do
                {
                    originalMemberInfo = currentType.GetMember(tempForeignKeyAttribute.OriginalFieldName, memberLookupBindingFlags);
                    currentType = currentType.BaseType;
                } while (!currentType.Equals(storedType) && ((originalMemberInfo == null) || (originalMemberInfo.Length == 0)));
            }
            if (originalMemberInfo.Length == 0)
                return null;

            return originalMemberInfo[0].GetCustomAttribute<PersistAsAttribute>();

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
