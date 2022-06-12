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
    public class DatabasePersister<T>
        where T : class, IModel
    {

        private IDatabaseBase database;

        private bool isPolymorph = false;
        private IModelTypeRegister typeRegister;

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
                typeRegister = polymorphAttr.TypeRegister;
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

        public void Save(IDictionary<int, T> items)
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

        public IDictionary<int, T> Load()
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

        private static readonly Type storedType = typeof(T);

        #region Member store
        private static Dictionary<Type, ExtendedMemberInfo[]> extendedMemberInfosForTypes = new();
        private static Dictionary<Type, Dictionary<string, ExtendedMemberInfo>> extendedMemberInfosByNameForTypes = new();

        private class ExtendedMemberInfo
        {

            public readonly MemberInfo MemberInfo;
            public readonly FieldInfo FieldInfo;
            public readonly PropertyInfo PropertyInfo;
            public readonly Type ValueType;
            public readonly PersistAsAttribute[] PersistAsAttributes;
            public readonly PersistDetailedAttribute PersistDetailedAttribute;
            public readonly PersistSubclassAttribute PersistSubclassAttribute;
            public readonly PolymorphFieldAttribute PolymorphFieldAttribute;
            public readonly TempForeignKeyAttribute TempForeignKeyAttribute;
            public readonly bool IsAssociationMember;

            public ExtendedMemberInfo(MemberInfo memberInfo)
            {
                MemberInfo = memberInfo;
                FieldInfo = memberInfo as FieldInfo;
                PropertyInfo = memberInfo as PropertyInfo;
                ValueType = (FieldInfo != null) ? FieldInfo.FieldType : PropertyInfo.PropertyType;
                IEnumerable<PersistAsAttribute> persistAsAttributes = memberInfo.GetCustomAttributes<PersistAsAttribute>();
                if (persistAsAttributes.Count() > 0)
                {
                    int dimensionMax = persistAsAttributes.Max(paa => paa.Dimension);
                    PersistAsAttributes = new PersistAsAttribute[dimensionMax + 1];
                    foreach (PersistAsAttribute persistAsAttribute in persistAsAttributes)
                        PersistAsAttributes[persistAsAttribute.Dimension] = persistAsAttribute;
                }
                else
                {
                    PersistAsAttributes = null;
                }
                PersistDetailedAttribute = memberInfo.GetCustomAttribute<PersistDetailedAttribute>();
                PersistSubclassAttribute = memberInfo.GetCustomAttribute<PersistSubclassAttribute>();
                PolymorphFieldAttribute = memberInfo.GetCustomAttribute<PolymorphFieldAttribute>();
                TempForeignKeyAttribute = memberInfo.GetCustomAttribute<TempForeignKeyAttribute>();
                IsAssociationMember = ((getArrayBaseType(ValueType).GetInterfaces().Any(iface => (iface == typeof(ISystemObject)))) && (PersistDetailedAttribute == null));
            }

            public PersistAsAttribute GetPersistAsAttributeForDimension(int dimension)
                => ((PersistAsAttributes == null) || (PersistAsAttributes.Length <= dimension)) ? null : PersistAsAttributes[dimension];

            public object GetValue(object item)
                => (FieldInfo != null) ? FieldInfo.GetValue(item) : PropertyInfo.GetValue(item);

            public void SetValue(object item, object value)
            {
                if (FieldInfo != null)
                    FieldInfo.SetValue(item, value);
                else
                    PropertyInfo.SetValue(item, value);
            }

        };

        private static ExtendedMemberInfo[] getExtendedMemberInfosForType(Type type)
        {
            if (type == null)
                return Array.Empty<ExtendedMemberInfo>();
            ExtendedMemberInfo[] extendedMemberInfos;
            while (!extendedMemberInfosForTypes.TryGetValue(type, out extendedMemberInfos))
                collectExtendedMemberInfosForType(type);
            return extendedMemberInfos;
        }

        private static Dictionary<string, ExtendedMemberInfo> getExtendedMemberInfosByNameForType(Type type)
        {
            if (type == null)
                return new Dictionary<string, ExtendedMemberInfo>();
            Dictionary<string, ExtendedMemberInfo> extendedMemberInfosByName;
            while (!extendedMemberInfosByNameForTypes.TryGetValue(type, out extendedMemberInfosByName))
                collectExtendedMemberInfosForType(type);
            return extendedMemberInfosByName;
        }

        private static ExtendedMemberInfo getExtendedMemberInfoByNameForType(Type type, string name)
        {
            getExtendedMemberInfosByNameForType(type).TryGetValue(name, out ExtendedMemberInfo extendedMemberInfo));
            return extendedMemberInfo;
        }

        private const BindingFlags memberCollectLookupBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;

        private static void collectExtendedMemberInfosForType(Type type)
        {
            IEnumerable<ExtendedMemberInfo> membersOfType = type
                .GetMembers(memberCollectLookupBindingFlags)
                .Where(filterMemberInfo1)
                .Select(mi => new ExtendedMemberInfo(mi))
                .Where(filterExtendedMemberInfo2);
            List<ExtendedMemberInfo> extendedMemberInfos = new();
            extendedMemberInfos.AddRange(getExtendedMemberInfosForType(type.BaseType));
            extendedMemberInfos.AddRange(membersOfType);
            extendedMemberInfosForTypes.Add(type, extendedMemberInfos.ToArray());
            Dictionary<string, ExtendedMemberInfo> extendedMemberInfosByName = new();
            foreach (ExtendedMemberInfo extendedMemberInfo in extendedMemberInfos)
                extendedMemberInfosByName[extendedMemberInfo.MemberInfo.Name] = extendedMemberInfo;
            extendedMemberInfosByNameForTypes.Add(type, extendedMemberInfosByName);
        }

        private static bool filterMemberInfo1(MemberInfo memberInfo)
            => ((memberInfo is FieldInfo) || (memberInfo is PropertyInfo));

        private static bool filterExtendedMemberInfo2(ExtendedMemberInfo extendedMemberInfo)
            => ((extendedMemberInfo.GetPersistAsAttributeForDimension(0) != null) || (extendedMemberInfo.TempForeignKeyAttribute != null));

        private const BindingFlags memberLookupBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        #endregion

        #region Serialization
        private XElement serializeItem(T item)
        {
            XElement xmlElement = new XElement(itemTag);
            Type typeToSerialize = storedType;
            xmlElement.SetAttributeValue(ATTRIBUTE_ID, item.ID);
            if (isPolymorph)
            {
                Type itemType = item.GetType();
                xmlElement.SetAttributeValue(ATTRIBUTE_TYPE, typeRegister.ConvertTypeToString(itemType));
                typeToSerialize = itemType;
            }
            foreach (ExtendedMemberInfo extendedMemberInfo in getExtendedMemberInfosForType(typeToSerialize))
                storeValueOfMember(extendedMemberInfo, ref item, ref xmlElement);
            return xmlElement;
        }

        private void storeValueOfMember(ExtendedMemberInfo extendedMemberInfo, ref T item, ref XElement xmlElement)
        {
            if ((extendedMemberInfo.FieldInfo == null) && (extendedMemberInfo.PropertyInfo == null))
                return;
            if ((extendedMemberInfo.PropertyInfo != null) && !(extendedMemberInfo.PropertyInfo.CanRead && extendedMemberInfo.PropertyInfo.CanWrite))
                return;
            if (extendedMemberInfo.TempForeignKeyAttribute != null)
                return;
            object xmlElementInner = serializeValue(extendedMemberInfo, extendedMemberInfo.ValueType, extendedMemberInfo.GetValue(item), item);
            xmlElement.Add(new XElement(extendedMemberInfo.PersistAsAttributes[0].TagName, xmlElementInner));
        }

        private object serializeValue(ExtendedMemberInfo extendedMemberInfo, Type memberType, object item, object parentItem, int arrayDimension = 0, object[] indices = null)
        {

            if (item == null)
                return string.Empty;

            ISystemObject itemAsSystemObject = item as ISystemObject;
            if ((itemAsSystemObject != null) && (extendedMemberInfo.PersistDetailedAttribute == null))
                return itemAsSystemObject.GlobalID;

            object[] extendedIndices = extendIndices(indices, arrayDimension);

            if (memberType.IsArray && (item is Array))
            {
                Array array = item as Array;
                List<XElement> arrayElements = new List<XElement>();
                int elementIndex = 0;
                foreach (var element in array)
                {
                    extendedIndices[arrayDimension] = elementIndex++;
                    arrayElements.Add(serializeCollectionElement(extendedMemberInfo, memberType.GetElementType(), element, parentItem, arrayDimension, extendedIndices));
                }
                return arrayElements;
            }

            bool isCollection = false;
            Type elementType = null;
            foreach (Type interfaceType in memberType.GetInterfaces())
            {
                if (interfaceType.IsGenericType && (interfaceType.GetGenericTypeDefinition() == typeof(ICollection<>)))
                {
                    isCollection = true;
                    elementType = interfaceType.GetGenericArguments()[0];
                    break;
                }
            }
            if (isCollection)
            {
                IEnumerable enumerable = item as IEnumerable;
                List<XElement> arrayElements = new List<XElement>();
                int elementIndex = 0;
                foreach (var element in enumerable)
                {
                    extendedIndices[arrayDimension] = elementIndex++;
                    arrayElements.Add(serializeCollectionElement(extendedMemberInfo, elementType, element, parentItem, arrayDimension, extendedIndices));
                }
                return arrayElements;
            }

            if (Type.GetTypeCode(memberType) == TypeCode.Object)
            {

                Type itemType = item.GetType();
                Type serializeAsType = memberType;

                PersistSubclassAttribute persistSubclassAttribute = extendedMemberInfo.PersistSubclassAttribute;
                if (persistSubclassAttribute != null) // should check if given type is subclass of member type
                {
                    MethodInfo subclassTypeGetterMethodInfo = parentItem.GetType().GetMethod(persistSubclassAttribute.SubclassTypeGetterName, memberLookupBindingFlags);
                    serializeAsType = subclassTypeGetterMethodInfo.Invoke(parentItem, null) as Type;
                }

                PolymorphFieldAttribute polymorphFieldAttribute = extendedMemberInfo.PolymorphFieldAttribute;
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
                XElement serializedItem = serializer.SerializeItem(item, parentItem, indices);
                if ((polymorphFieldAttribute?.TypeAttributeName != null) && (itemTypeString != null))
                    serializedItem.SetAttributeValue(polymorphFieldAttribute.TypeAttributeName, itemTypeString);
                return serializedItem;

            }
           
            return item.ToString();

        }

        private XElement serializeCollectionElement(ExtendedMemberInfo extendedMemberInfo, Type memberType, object element, object parentItem, int arrayDimension, object[] indices)
        {

            object elementKey = null;
            object elementToSerialize = element;
            Type typeToSerialize = memberType;

            if (memberType.IsGenericType && (memberType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>)))
            {
                PropertyInfo keyPropertyInfo = memberType.GetProperty(nameof(KeyValuePair<object, object>.Key));
                PropertyInfo valuePropertyInfo = memberType.GetProperty(nameof(KeyValuePair<object, object>.Value));
                elementKey = keyPropertyInfo.GetValue(element);
                elementToSerialize = valuePropertyInfo.GetValue(element);
                typeToSerialize = memberType.GetGenericArguments()[1];
                indices[arrayDimension] = elementKey;
            }

            object arrayElementValue = serializeValue(extendedMemberInfo, typeToSerialize, elementToSerialize, parentItem, arrayDimension + 1, indices);
            PersistAsAttribute persistData = extendedMemberInfo.GetPersistAsAttributeForDimension(arrayDimension + 1);
            XElement returnElement = ((arrayElementValue is XElement) && (persistData?.TagName == null)) ? (XElement)arrayElementValue : new XElement(persistData?.TagName ?? UNDEFINED_ARRAY_ITEM_TAG, arrayElementValue);
            if (elementKey != null)
                returnElement.SetAttributeValue(persistData?.KeyAttribute ?? "key", (elementKey is ISystemObject elementKeyObj) ? elementKeyObj.GlobalID : elementKey.ToString());

            return returnElement;

        }
        #endregion

        #region Deserialization
        private T deserializeItem(XmlNode xmlElement)
        {

            T item = null;

            if (xmlElement.NodeType != XmlNodeType.Element)
                return null;

            Type typeToDeserialize = typeof(T);
            if (isPolymorph)
            {
                string typeStr = xmlElement.Attributes[ATTRIBUTE_TYPE]?.Value;
                typeToDeserialize = typeRegister.ConvertStringToType(typeStr);
                if (typeToDeserialize == null)
                    return null;
            }

            foreach (ConstructorInfo ctor in typeToDeserialize.GetConstructors())
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

            foreach (ExtendedMemberInfo extendedMemberInfo in getExtendedMemberInfosForType(typeToDeserialize))
                restoreValueForMember(extendedMemberInfo, persistedValues, ref item);

            return item;

        }

        private void restoreValueForMember(ExtendedMemberInfo extendedMemberInfo, Dictionary<string, XmlElement> persistedValues, ref T item)
        {
            if ((extendedMemberInfo.FieldInfo == null) && (extendedMemberInfo.PropertyInfo == null))
                return;
            if ((extendedMemberInfo.PropertyInfo != null) && !(extendedMemberInfo.PropertyInfo.CanRead && extendedMemberInfo.PropertyInfo.CanWrite))
                return;
            if (extendedMemberInfo.IsAssociationMember)
                return;

            PersistAsAttribute persistData = extendedMemberInfo.GetPersistAsAttributeForDimension(0);
            if ((persistData == null) && (extendedMemberInfo.TempForeignKeyAttribute != null))
            {
                string originalFieldName = extendedMemberInfo.TempForeignKeyAttribute.OriginalFieldName;
                persistData = getExtendedMemberInfoByNameForType(extendedMemberInfo.MemberInfo.DeclaringType, originalFieldName).GetPersistAsAttributeForDimension(0);
            }
            if (!persistedValues.TryGetValue(persistData.TagName, out XmlElement xmlElement))
                return;
            try
            {
                object value = deserializeXmlElement(extendedMemberInfo, extendedMemberInfo.ValueType, xmlElement, item);
                try
                {
                    extendedMemberInfo.SetValue(item, value);
                }
                catch { }
            }
            catch (WillRestoreValueLaterException) { }
        }

        private static readonly Type[] PRIMITIVE_TYPES_NULL_IS_0 = new Type[]
        {
            typeof(int),
            typeof(float),
            typeof(double),
            typeof(decimal)
        };

        private object deserializeXmlElement(ExtendedMemberInfo extendedMemberInfo, Type memberType, XmlElement xmlElement, object parentItem, int arrayDimension = 0, object[] indices = null)
        {

            if (memberType == typeof(string))
                return xmlElement.InnerText;

            if ((xmlElement.InnerText == string.Empty) && PRIMITIVE_TYPES_NULL_IS_0.Contains(memberType))
                return 0;

            bool isCollection = memberType.GetInterfaces().Any(interfaceType => interfaceType.IsGenericType && (interfaceType.GetGenericTypeDefinition() == typeof(ICollection<>)));
            if (memberType.IsArray || isCollection)
            {
                List<XmlElement> childElements = new List<XmlElement>();
                foreach (XmlNode childNode in xmlElement.ChildNodes)
                    if (childNode.NodeType == XmlNodeType.Element)
                        childElements.Add((XmlElement)childNode);
                return deserializeArray(extendedMemberInfo, memberType, childElements, parentItem, arrayDimension, indices);
            }

            if (memberType.IsEnum)
                return Enum.Parse(memberType, xmlElement.InnerText);

            Type nullableUnderlyingType = Nullable.GetUnderlyingType(memberType);
            if (nullableUnderlyingType?.IsEnum == true)
            {
                if (xmlElement.InnerText == string.Empty)
                    return null;
                return Enum.Parse(nullableUnderlyingType, xmlElement.InnerText);
            }

            Type deserializeAsType = memberType;
            if (Type.GetTypeCode(memberType) == TypeCode.Object)
            {

                PersistSubclassAttribute persistSubclassAttribute = extendedMemberInfo.PersistSubclassAttribute;
                if (persistSubclassAttribute != null) // should check if given type is subclass of member type
                {
                    MethodInfo subclassTypeGetterMethodInfo = parentItem.GetType().GetMethod(persistSubclassAttribute.SubclassTypeGetterName, memberLookupBindingFlags);
                    deserializeAsType = subclassTypeGetterMethodInfo.Invoke(parentItem, null) as Type;
                }

                PolymorphFieldAttribute polymorphFieldAttribute = extendedMemberInfo.PolymorphFieldAttribute;
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
                PersistAsAttribute persistData = extendedMemberInfo.GetPersistAsAttributeForDimension(arrayDimension);
                if ((persistData == null) && (extendedMemberInfo.TempForeignKeyAttribute != null))
                {
                    string originalFieldName = extendedMemberInfo.TempForeignKeyAttribute.OriginalFieldName;
                    persistData = getExtendedMemberInfoByNameForType(extendedMemberInfo.MemberInfo.DeclaringType, originalFieldName).GetPersistAsAttributeForDimension(arrayDimension);
                }
                XmlElement itemToDeserialize = xmlElement;
                if (persistData.TagName != null)
                    itemToDeserialize = itemToDeserialize.OfType<XmlElement>().FirstOrDefault();
                return serializer.DeserializeItem(itemToDeserialize, parentItem, indices);
            }

            return Convert.ChangeType(xmlElement.InnerText, deserializeAsType);

        }

        private object deserializeArray(ExtendedMemberInfo extendedMemberInfo, Type memberType, List<XmlElement> childElements, object parentItem, int arrayDimension, object[] indices)
        {

            int childElementCount = childElements.Count;

            object[] extendedIndices = extendIndices(indices, arrayDimension);
            if (memberType.IsArray)
            {
                Type arrayElementType = memberType.GetElementType();
                if (Type.GetTypeCode(arrayElementType) != TypeCode.Object)
                {
                    Array typedArray = Array.CreateInstance(arrayElementType, childElementCount);
                    for (int i = 0; i < childElementCount; i++)
                    {
                        extendedIndices[arrayDimension] = i;
                        typedArray.SetValue(deserializeXmlElement(extendedMemberInfo, arrayElementType, childElements[i], parentItem, arrayDimension + 1, extendedIndices), i);
                    }
                    return typedArray;
                }
                object[] array = (object[])Activator.CreateInstance(memberType, new object[] { childElementCount });
                for (int i = 0; i < childElementCount; i++)
                {
                    extendedIndices[arrayDimension] = i;
                    array[i] = deserializeXmlElement(extendedMemberInfo, memberType.GetElementType(), childElements[i], parentItem, arrayDimension + 1, extendedIndices);
                }
                return array;
            }

            bool isCollection = false;
            bool isDictionary = false;
            Type keyType = null;
            Type elementType = null;
            foreach (Type interfaceType in memberType.GetInterfaces())
            {
                if (interfaceType.IsGenericType)
                {
                    if (interfaceType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                    {
                        isDictionary = true;
                        isCollection = true;
                        keyType = interfaceType.GetGenericArguments()[0];
                        elementType = interfaceType.GetGenericArguments()[1];
                        break;
                    }
                    if (interfaceType.GetGenericTypeDefinition() == typeof(ICollection<>))
                    {
                        isCollection = true;
                        elementType = interfaceType.GetGenericArguments()[0];
                        break;
                    }
                }
            }
            if (isCollection)
            {
                if (extendedMemberInfo.PersistDetailedAttribute == null)
                {
                    if (elementType.IsAssignableTo(typeof(ISystemObject)) || (keyType?.IsAssignableTo(typeof(ISystemObject)) == true))
                        throw new WillRestoreValueLaterException();
                    Type[] genericParams = (keyType != null) ? new Type[] { keyType, elementType } : new Type[] { elementType };
                    memberType = memberType.GetGenericTypeDefinition().MakeGenericType(genericParams);
                }
                object collection = Activator.CreateInstance(memberType, new object[] { });
                Type[] addMethodTypes = isDictionary ? new Type[] { keyType, elementType } : new Type[] { elementType };
                MethodInfo addMethod = memberType.GetMethod(nameof(ICollection<object>.Add), addMethodTypes);
                for (int i = 0; i < childElementCount; i++)
                {
                    extendedIndices[arrayDimension] = i;
                    object deserializedKey = null;
                    if (isDictionary)
                    {
                        PersistAsAttribute persistData = extendedMemberInfo.GetPersistAsAttributeForDimension(arrayDimension + 1);
                        if ((persistData == null) && (extendedMemberInfo.TempForeignKeyAttribute != null))
                        {
                            string originalFieldName = extendedMemberInfo.TempForeignKeyAttribute.OriginalFieldName;
                            persistData = getExtendedMemberInfoByNameForType(extendedMemberInfo.MemberInfo.DeclaringType, originalFieldName).GetPersistAsAttributeForDimension(arrayDimension + 1);
                        }
                        if (persistData == null)
                            persistData = new PersistAsAttribute(UNDEFINED_ARRAY_ITEM_TAG);
                        deserializedKey = childElements[i].GetAttribute(persistData.KeyAttribute);
                        extendedIndices[arrayDimension] = deserializedKey;
                    }
                    object deserializedElement = deserializeXmlElement(extendedMemberInfo, elementType, childElements[i], parentItem, arrayDimension + 1, extendedIndices);
                    if (isDictionary)
                    {
                        if (deserializedKey != null)
                            addMethod.Invoke(collection, new object[] { deserializedKey, deserializedElement });
                    }
                    else
                    {
                        addMethod.Invoke(collection, new object[] { deserializedElement });
                    }
                }
                return collection; 
            }

            return null;

        }

        private class WillRestoreValueLaterException : Exception { }
        #endregion

        #region Relations/associations
        public void BuildRelationsByForeignKeys(IDictionary<int, T> items)
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

        private void buildRelationForField(T item, FieldInfo foreignKeyField, ref IDictionary<int, T> items)
        {

            TempForeignKeyAttribute attr = foreignKeyField.GetCustomAttribute<TempForeignKeyAttribute>();
            if (attr == null)
                return;

            FieldInfo originalField = storedType.GetField(attr.OriginalFieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            PropertyInfo originalProperty = null;
            if (originalField == null)
                originalProperty = storedType.GetProperty(attr.OriginalFieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (isPolymorph && (originalField == null) && (originalField == null))
            {
                Type currentType = item.GetType();
                do
                {
                    originalField = currentType.GetField(attr.OriginalFieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    if (originalField == null)
                        originalProperty = currentType.GetProperty(attr.OriginalFieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    currentType = currentType.BaseType;
                } while (!currentType.Equals(storedType) && (originalField == null) && (originalProperty == null));
            }
            if ((originalField == null) && (originalProperty == null))
                return;

            object foreignKeys = foreignKeyField.GetValue(item);
            if (foreignKeys == null)
                return;

            if (originalProperty != null)
            {
                object foreignObjects = getAssociatedObjects(foreignKeyField.FieldType, originalProperty.PropertyType, foreignKeys);
                originalProperty.SetValue(item, foreignObjects);
            }
            else
            {
                object foreignObjects = getAssociatedObjects(foreignKeyField.FieldType, originalField.FieldType, foreignKeys);
                originalField.SetValue(item, foreignObjects);
            }

        }

        private object getAssociatedObjects(Type memberType, Type originalType, object foreignKeys)
        {

            if (memberType.IsArray && (memberType.GetElementType() != typeof(string)))
            {
                object[] foreignKeysArray = foreignKeys as object[];
                if (foreignKeysArray == null)
                    return null;
                object[] associatedObjects = (object[])Activator.CreateInstance(originalType, new object[] { foreignKeysArray.Length });
                for (int i = 0; i < foreignKeysArray.Length; i++)
                    associatedObjects[i] = getAssociatedObjects(memberType.GetElementType(), originalType.GetElementType(), foreignKeysArray[i]);
                return Convert.ChangeType(associatedObjects, originalType);
            }
            if(memberType.IsArray)
            {
                string[] foreignKeysArray = foreignKeys as string[];
                if (foreignKeysArray == null)
                    return null;
                object[] associatedObjects = (object[])Activator.CreateInstance(originalType, new object[] { foreignKeysArray.Length });
                for (int i = 0; i < foreignKeysArray.Length; i++)
                {
                    string foreignKeyString = foreignKeysArray[i] as string;
                    if (foreignKeyString != null)
                        associatedObjects[i] = SystemObjectRegister.Instance[foreignKeyString];
                }
                return associatedObjects;
            }

            bool isCollection = false;
            bool isDictionary = false;
            Type memberKeyType = null;
            Type memberElementType = null;
            foreach (Type interfaceType in memberType.GetInterfaces())
            {
                if (interfaceType.IsGenericType)
                {
                    if (interfaceType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                    {
                        isDictionary = true;
                        isCollection = true;
                        memberKeyType = interfaceType.GetGenericArguments()[0];
                        memberElementType = interfaceType.GetGenericArguments()[1];
                        break;
                    }
                    if (interfaceType.GetGenericTypeDefinition() == typeof(ICollection<>))
                    {
                        isCollection = true;
                        memberElementType = interfaceType.GetGenericArguments()[0];
                        break;
                    }
                }
            }
            Type originalKeyType = null;
            Type originalElementType = null;
            foreach (Type interfaceType in originalType.GetInterfaces())
            {
                if (interfaceType.IsGenericType)
                {
                    if (interfaceType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                    {
                        isDictionary = true;
                        isCollection = true;
                        originalKeyType = interfaceType.GetGenericArguments()[0];
                        originalElementType = interfaceType.GetGenericArguments()[1];
                        break;
                    }
                    if (interfaceType.GetGenericTypeDefinition() == typeof(ICollection<>))
                    {
                        isCollection = true;
                        originalElementType = interfaceType.GetGenericArguments()[0];
                        break;
                    }
                }
            }

            if (isCollection)
            {
                object associatedObjects = Activator.CreateInstance(originalType, new object[] { });
                Type[] addMethodTypes = isDictionary ? new Type[] { originalKeyType, originalElementType } : new Type[] { originalElementType };
                MethodInfo addMethod = originalType.GetMethod(nameof(ICollection<object>.Add), addMethodTypes);
                IEnumerable foreignKeysEnumerable = foreignKeys as IEnumerable;
                if (foreignKeysEnumerable == null)
                    return null;
                foreach (object foreignKey in foreignKeysEnumerable)
                {
                    object foreignKeyValue = foreignKey;
                    object foreignKeyKey = null;
                    if (isDictionary)
                    {
                        Type foreignKeyType = foreignKey.GetType();
                        PropertyInfo keyProperty = foreignKeyType.GetProperty(nameof(KeyValuePair<object, object>.Key), BindingFlags.Public | BindingFlags.Instance);
                        PropertyInfo valueProperty = foreignKeyType.GetProperty(nameof(KeyValuePair<object, object>.Value), BindingFlags.Public | BindingFlags.Instance);
                        foreignKeyKey = keyProperty.GetValue(foreignKey);
                        foreignKeyValue = valueProperty.GetValue(foreignKey);
                    }
                    object associatedObject = null;
                    if (memberElementType != typeof(string))
                    {                        
                        associatedObject = getAssociatedObjects(memberElementType, originalElementType, foreignKeyValue);
                    }
                    else
                    {
                        string foreignKeyString = null;
                        foreignKeyString = foreignKeyValue as string;
                        if (foreignKeyString != null)
                            associatedObject = SystemObjectRegister.Instance[foreignKeyString];
                    }
                    if (isDictionary)
                    {
                        if (foreignKeyKey != null)
                        {
                            if (originalKeyType.IsAssignableTo(typeof(ISystemObject)))
                            {
                                string foreignKeyKeyString = foreignKeyKey as string;
                                if (foreignKeyKeyString != null)
                                    foreignKeyKey = SystemObjectRegister.Instance[foreignKeyKeyString];
                            }
                            if (foreignKeyKey != null)
                                addMethod.Invoke(associatedObjects, new object[] { foreignKeyKey, associatedObject });
                        }
                    }
                    else
                    {
                        addMethod.Invoke(associatedObjects, new object[] { associatedObject });
                    }
                }
                return associatedObjects;
            }

            if (foreignKeys?.GetType() != typeof(string))
                return foreignKeys;

            string _foreignKeyString = foreignKeys as string;
            if (_foreignKeyString == null)
                return null;
            return SystemObjectRegister.Instance[_foreignKeyString];

        }

        #endregion

        private static Type getArrayBaseType(Type type) => (type.IsArray) ? getArrayBaseType(type.GetElementType()) : type;

        private object[] extendIndices(object[] original, int arrayDimension)
        {
            object[] extendedIndices = new object[arrayDimension + 1];
            for (int i = 0; i < arrayDimension; i++)
                extendedIndices[i] = original[i];
            return extendedIndices;
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
            => Serializers.Add(serializer.Type, serializer);

        private static IValueXmlSerializer GetSerializerForType(Type type)
        {
            if (!Serializers.TryGetValue(type, out IValueXmlSerializer foundSerializer))
                return null;
            return foundSerializer;
        }
        #endregion

    }
}
