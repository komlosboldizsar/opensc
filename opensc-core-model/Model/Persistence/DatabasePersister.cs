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

        private const BindingFlags memberLookupBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        private static readonly Type storedType = typeof(T);

        #region Serialization
        private XElement serializeItem(T item)
        {

            XElement xmlElement = new XElement(itemTag);
            Type itemType = item.GetType();

            xmlElement.SetAttributeValue(ATTRIBUTE_ID, item.ID);
            if (isPolymorph)
                xmlElement.SetAttributeValue(ATTRIBUTE_TYPE, typeRegister.ConvertTypeToString(itemType));

            // Get fields
            foreach (FieldInfo fieldInfo in storedType.GetFields(memberLookupBindingFlags))
                storeValueOfFieldOrProperty(fieldInfo, ref item, ref xmlElement);

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

        private object serializeValue(MemberInfo memberInfo, Type memberType, object item, object parentItem, int arrayDimension = 0, object[] indices = null)
        {

            if (item == null)
                return string.Empty;

            ISystemObject itemAsSystemObject = item as ISystemObject;
            if ((itemAsSystemObject != null) && (memberInfo.GetCustomAttribute<PersistDetailedAttribute>() == null))
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
                    arrayElements.Add(serializeCollectionElement(memberInfo, memberType.GetElementType(), element, parentItem, arrayDimension, extendedIndices));
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
                    arrayElements.Add(serializeCollectionElement(memberInfo, elementType, element, parentItem, arrayDimension, extendedIndices));
                }
                return arrayElements;
            }

            if (Type.GetTypeCode(memberType) == TypeCode.Object)
            {

                Type itemType = item.GetType();
                Type serializeAsType = memberType;

                PersistSubclassAttribute persistSubclassAttribute = memberInfo.GetCustomAttributes<PersistSubclassAttribute>().FirstOrDefault();
                if (persistSubclassAttribute != null) // should check if given type is subclass of member type
                {
                    MethodInfo subclassTypeGetterMethodInfo = parentItem.GetType().GetMethod(persistSubclassAttribute.SubclassTypeGetterName, memberLookupBindingFlags);
                    serializeAsType = subclassTypeGetterMethodInfo.Invoke(parentItem, null) as Type;
                }

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
                XElement serializedItem = serializer.SerializeItem(item, parentItem, indices);
                if ((polymorphFieldAttribute?.TypeAttributeName != null) && (itemTypeString != null))
                    serializedItem.SetAttributeValue(polymorphFieldAttribute.TypeAttributeName, itemTypeString);
                return serializedItem;

            }
           
            return item.ToString();

        }

        private XElement serializeCollectionElement(MemberInfo memberInfo, Type memberType, object element, object parentItem, int arrayDimension, object[] indices)
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

            object arrayElementValue = serializeValue(memberInfo, typeToSerialize, elementToSerialize, parentItem, arrayDimension + 1, indices);
            PersistAsAttribute persistData = getPersistDataForMember(memberInfo, elementToSerialize, arrayDimension + 1);
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

            Type type = typeof(T);
            if (isPolymorph)
            {
                string typeStr = xmlElement.Attributes[ATTRIBUTE_TYPE]?.Value;
                type = typeRegister.ConvertStringToType(typeStr);
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

            try
            {
                object value = deserializeXmlElement(memberInfo, type, xmlElement, item);

                try
                {
                    if (fieldInfo != null)
                        fieldInfo.SetValue(item, value);
                    else
                        propertyInfo.SetValue(item, value);
                }
                catch { }
            } catch (WillRestoreValueLaterException) { }

        }

        private static readonly Type[] PRIMITIVE_TYPES_NULL_IS_0 = new Type[]
        {
            typeof(int),
            typeof(float),
            typeof(double),
            typeof(decimal)
        };

        private object deserializeXmlElement(MemberInfo memberInfo, Type memberType, XmlElement xmlElement, object parentItem, int arrayDimension = 0, object[] indices = null)
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
                return deserializeArray(memberInfo, memberType, childElements, parentItem, arrayDimension, indices);
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
                
                PersistSubclassAttribute persistSubclassAttribute = memberInfo.GetCustomAttributes<PersistSubclassAttribute>().FirstOrDefault();
                if (persistSubclassAttribute != null) // should check if given type is subclass of member type
                {
                    MethodInfo subclassTypeGetterMethodInfo = parentItem.GetType().GetMethod(persistSubclassAttribute.SubclassTypeGetterName, memberLookupBindingFlags);
                    deserializeAsType = subclassTypeGetterMethodInfo.Invoke(parentItem, null) as Type;
                }

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
                return serializer.DeserializeItem(itemToDeserialize, parentItem, indices);
            }

            return Convert.ChangeType(xmlElement.InnerText, deserializeAsType);

        }

        private object deserializeArray(MemberInfo memberInfo, Type memberType, List<XmlElement> childElements, object parentItem, int arrayDimension, object[] indices)
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
                        typedArray.SetValue(deserializeXmlElement(memberInfo, arrayElementType, childElements[i], parentItem, arrayDimension + 1, extendedIndices), i);
                    }
                    return typedArray;
                }
                object[] array = (object[])Activator.CreateInstance(memberType, new object[] { childElementCount });
                for (int i = 0; i < childElementCount; i++)
                {
                    extendedIndices[arrayDimension] = i;
                    array[i] = deserializeXmlElement(memberInfo, memberType.GetElementType(), childElements[i], parentItem, arrayDimension + 1, extendedIndices);
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
                if (memberInfo.GetCustomAttribute<PersistDetailedAttribute>() == null)
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
                        PersistAsAttribute persistData = getPersistDataForMember(memberInfo, null, arrayDimension + 1);
                        if (persistData == null)
                            persistData = new PersistAsAttribute(UNDEFINED_ARRAY_ITEM_TAG);
                        deserializedKey = childElements[i].GetAttribute(persistData.KeyAttribute);
                        extendedIndices[arrayDimension] = deserializedKey;
                    }
                    object deserializedElement = deserializeXmlElement(memberInfo, elementType, childElements[i], parentItem, arrayDimension + 1, extendedIndices);
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

            return getPersistDataForMember(originalMemberInfo[0], item, dimension);

        }

        private bool isTemporaryForeignKeyField(MemberInfo memberInfo) => (memberInfo.GetCustomAttributes<TempForeignKeyAttribute>().Count() > 0);

        private bool isAssociationField(MemberInfo memberInfo)
        {
            FieldInfo fieldInfo = memberInfo as FieldInfo;
            PropertyInfo propertyInfo = memberInfo as PropertyInfo;
            Type type = (fieldInfo != null) ? fieldInfo.FieldType : propertyInfo.PropertyType;
            return ((getArrayBaseType(type).GetInterfaces().Any(iface => (iface == typeof(ISystemObject)))) && (memberInfo.GetCustomAttribute<PersistDetailedAttribute>() == null));
        }

        private Type getArrayBaseType(Type type) => (type.IsArray) ? getArrayBaseType(type.GetElementType()) : type;

        private object[] extendIndices(object[] original, int arrayDimension)
        {
            object[] extendedIndices = new object[arrayDimension + 1];
            for (int i = 0; i < arrayDimension; i++)
                extendedIndices[i] = original[i];
            return extendedIndices;
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
