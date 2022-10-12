using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OpenSC.Model.Persistence
{
    internal class DatabaseDeserializer<T>
        where T : class, IModel
    {

        private readonly bool isPolymorph;
        private readonly IModelTypeRegister typeRegister;
        private readonly string rootTag;
        private readonly string itemTag;

        public DatabaseDeserializer(bool isPolymorph, IModelTypeRegister typeRegister, string rootTag, string itemTag)
        {
            this.isPolymorph = isPolymorph;
            this.typeRegister = typeRegister;
            this.rootTag = rootTag;
            this.itemTag = itemTag;
        }

        private static readonly Type storedType = typeof(T);

        public class Result
        {
            public readonly Dictionary<int, T> Items;
            public readonly ForeignKeyCollection<T> ForeignKeys;
            public Result(Dictionary<int, T> items, ForeignKeyCollection<T> foreignKeys)
            {
                Items = items;
                ForeignKeys = foreignKeys;
            }
        }

        public Result Load(DatabaseFile fileToRead)
        {
            var items = new Dictionary<int, T>();
            ForeignKeyCollection<T> foreignKeyCollection = new();
            try
            {
                XmlDocument doc = new();
                doc.Load(fileToRead.Stream);
                XmlElement root = doc.DocumentElement;
                if (root.LocalName != rootTag)
                    return null;
                foreach (XmlElement itemElement in root.ChildNodes.OfType<XmlElement>())
                {
                    T item = deserializeItem(itemElement, foreignKeyCollection);
                    if (item != null)
                        items.Add(item.ID, item);
                }
            }
            catch { }
            return new(items, foreignKeyCollection);
        }

        private T deserializeItem(XmlElement xmlElement, ForeignKeyCollection<T> foreignKeyCollection)
        {

            T item = null;

            if (xmlElement.NodeType != XmlNodeType.Element)
                return null;

            Type typeToDeserialize = typeof(T);
            if (isPolymorph)
            {
                string typeStr = xmlElement.Attributes[DatabasePersisterConstants.ATTRIBUTE_TYPE]?.Value;
                typeToDeserialize = typeRegister.ConvertStringToType(typeStr);
                if (typeToDeserialize == null)
                    return null;
            }

            foreach (ConstructorInfo ctor in typeToDeserialize.GetConstructors())
                if (ctor.GetParameters().Length == 0)
                    try
                    {
                        item = (T)ctor.Invoke(Array.Empty<object>());
                    }
                    catch { }

            if (item == null)
                return null;

            string idStr = xmlElement.Attributes[DatabasePersisterConstants.ATTRIBUTE_ID].Value;
            if (!int.TryParse(idStr, out int id) || (id <= 0))
                return null;
            item.ID = id;

            Dictionary<string, object> foreignKeysOfItem = foreignKeyCollection.GetCollectionForItem(item);
            foreach (ExtendedMemberInfo extendedMemberInfo in typeToDeserialize.GetExtendedMemberInfos())
                restoreValueForMember(extendedMemberInfo, xmlElement, foreignKeysOfItem, item);

            return item;

        }

        private void restoreValueForMember(ExtendedMemberInfo extendedMemberInfo, XmlElement itemElement, Dictionary<string, object> foreignKeysOfItem, T item)
        {
            if ((extendedMemberInfo.FieldInfo == null) && (extendedMemberInfo.PropertyInfo == null))
                return;
            if ((extendedMemberInfo.PropertyInfo != null) && !(extendedMemberInfo.PropertyInfo.CanRead && extendedMemberInfo.PropertyInfo.CanWrite))
                return;
            PersistAsAttribute persistData = extendedMemberInfo.GetPersistAsAttributeForDimension(0);
            if (itemElement.SelectSingleNode(persistData.TagName) is not XmlElement xmlElement)
                return;
            object value = deserializeXmlElement(extendedMemberInfo, extendedMemberInfo.ValueType, xmlElement, item);
            try
            {
                if (extendedMemberInfo.RequiresRelationBuilding)
                    foreignKeysOfItem.Add(extendedMemberInfo.MemberInfo.Name, value);
                else
                    extendedMemberInfo.SetValue(item, value);
            }
            catch { }
        }

        private object deserializeXmlElement(ExtendedMemberInfo extendedMemberInfo, Type memberType, XmlElement xmlElement, object parentItem, int arrayDimension = 0, object[] indices = null)
        {
            if (memberType == typeof(string))
                return xmlElement.InnerText;
            if ((xmlElement.InnerText == string.Empty) && PRIMITIVE_TYPES_NULL_IS_0.Contains(memberType))
                return 0;
            object result = null;
            if (tryDeserializeAsCollection(ref result, extendedMemberInfo, memberType, xmlElement, parentItem, arrayDimension, indices))
                return result;
            if (tryDeserializeAsEnum(ref result, memberType, xmlElement))
                return result;
            if (tryDeserializeAsObject(ref result, extendedMemberInfo, memberType, xmlElement, parentItem, arrayDimension, indices))
                return result;
            return Convert.ChangeType(xmlElement.InnerText, memberType);
        }

        private static readonly Type[] PRIMITIVE_TYPES_NULL_IS_0 = new Type[]
        {
            typeof(int),
            typeof(float),
            typeof(double),
            typeof(decimal)
        };

        private bool tryDeserializeAsCollection(ref object result, ExtendedMemberInfo extendedMemberInfo, Type memberType, XmlElement xmlElement, object parentItem, int arrayDimension, object[] indices)
        {
            bool isCollection = memberType.GetInterfaces().Any(interfaceType => interfaceType.IsGenericType && (interfaceType.GetGenericTypeDefinition() == typeof(ICollection<>)));
            if (!memberType.IsArray && !isCollection)
                return false;
            result = deserializeCollection(extendedMemberInfo, memberType, xmlElement.ChildNodes.OfType<XmlElement>().ToList(), parentItem, arrayDimension, indices);
            return true;
        }

        private object deserializeCollection(ExtendedMemberInfo extendedMemberInfo, Type memberType, List<XmlElement> childElements, object parentItem, int arrayDimension, object[] indices)
        {

            int childElementCount = childElements.Count;

            object[] extendedIndices = DatabasePersisterHelpers.ExtendIndices(indices, arrayDimension);
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

            CollectionDetails collectionDetails = memberType.GetCollectionDetails();
            if (collectionDetails.IsCollection)
            {
                if (extendedMemberInfo.PersistDetailedAttribute == null)
                {
                    Type keyType = collectionDetails.KeyType;
                    Type elementType = collectionDetails.ElementType;
                    if (keyType?.IsAssignableTo(typeof(ISystemObject)) == true)
                        keyType = typeof(string);
                    if (elementType.IsAssignableTo(typeof(ISystemObject)))
                        elementType = typeof(string);
                    collectionDetails = collectionDetails with { KeyType = keyType, ElementType = elementType };
                    memberType = memberType.GetGenericTypeDefinition().MakeGenericType(collectionDetails.AsTypeArray);
                }
                object collection = Activator.CreateInstance(memberType, Array.Empty<object>());
                MethodInfo addMethod = memberType.GetMethod(nameof(ICollection<object>.Add), collectionDetails.AsTypeArray);
                for (int i = 0; i < childElementCount; i++)
                {
                    extendedIndices[arrayDimension] = i;
                    object deserializedKey = null;
                    if (collectionDetails.IsDictionary)
                    {
                        PersistAsAttribute persistData = extendedMemberInfo.GetPersistAsAttributeForDimension(arrayDimension + 1);
                        if (persistData == null)
                            persistData = new(DatabasePersisterConstants.UNDEFINED_ARRAY_ITEM_TAG);
                        deserializedKey = childElements[i].GetAttribute(persistData.KeyAttribute);
                        extendedIndices[arrayDimension] = deserializedKey;
                    }
                    object deserializedElement = deserializeXmlElement(extendedMemberInfo, collectionDetails.ElementType, childElements[i], parentItem, arrayDimension + 1, extendedIndices);
                    if (collectionDetails.IsDictionary && (deserializedKey != null))
                        addMethod.Invoke(collection, new object[] { deserializedKey, deserializedElement });
                    else if (!collectionDetails.IsDictionary)
                        addMethod.Invoke(collection, new object[] { deserializedElement });
                }
                return collection;
            }

            return null;

        }

        private bool tryDeserializeAsEnum(ref object result, Type memberType, XmlElement xmlElement)
        {
            if (memberType.IsEnum)
            {
                result = Enum.Parse(memberType, xmlElement.InnerText);
                return true;
            }
            Type nullableUnderlyingType = Nullable.GetUnderlyingType(memberType);
            if (nullableUnderlyingType?.IsEnum == true)
            {
                if (xmlElement.InnerText == string.Empty)
                    result = null;
                else
                    result = Enum.Parse(nullableUnderlyingType, xmlElement.InnerText);
                return true;
            }
            return false;
        }

        private bool tryDeserializeAsObject(ref object result, ExtendedMemberInfo extendedMemberInfo, Type memberType, XmlElement xmlElement, object parentItem, int arrayDimension, object[] indices)
        {

            Type deserializeAsType = memberType;
            if (Type.GetTypeCode(memberType) != TypeCode.Object)
                return false;

            PersistSubclassAttribute persistSubclassAttribute = extendedMemberInfo.PersistSubclassAttribute;
            if (persistSubclassAttribute != null) // should check if given type is subclass of member type
            {
                MethodInfo subclassTypeGetterMethodInfo = parentItem.GetType().GetMethod(persistSubclassAttribute.SubclassTypeGetterName, DatabasePersisterConstants.MEMBER_LOOKUP_BINDING_FLAGS);
                deserializeAsType = subclassTypeGetterMethodInfo.Invoke(parentItem, null) as Type;
            }

            PolymorphFieldAttribute polymorphFieldAttribute = extendedMemberInfo.PolymorphFieldAttribute;
            Dictionary<Type, string> typeStringDictionary = null;
            if (polymorphFieldAttribute != null)
            {
                string itemTypeString = xmlElement.GetAttribute(polymorphFieldAttribute.TypeAttributeName);
                MethodInfo typeStringDictionaryGetterMethodInfo = parentItem.GetType().GetMethod(polymorphFieldAttribute.TypeStringDictionaryGetterName, DatabasePersisterConstants.MEMBER_LOOKUP_BINDING_FLAGS);
                typeStringDictionary = typeStringDictionaryGetterMethodInfo.Invoke(parentItem, Array.Empty<object>()) as Dictionary<Type, string>;
                KeyValuePair<Type, string> foundTypeData = typeStringDictionary.FirstOrDefault(kvp => (kvp.Value == itemTypeString));
                if (foundTypeData.Key != null)
                    deserializeAsType = foundTypeData.Key;
            }

            PersistAsAttribute persistData = extendedMemberInfo.GetPersistAsAttributeForDimension(arrayDimension);
            object deserializedValue = xmlElement.InnerText;
            XmlElement itemToDeserialize = xmlElement;
            IValueXmlSerializer serializer = SerializerRegister.GetSerializerForType(deserializeAsType);

            if (serializer != null)
            {
                if ((persistData.TagName != null) && (persistData.Dimension == 0))
                    itemToDeserialize = itemToDeserialize.OfType<XmlElement>().FirstOrDefault();
                deserializedValue = extendedMemberInfo.CanDeserializeElement ? serializer.DeserializeItem(itemToDeserialize, parentItem, indices) : itemToDeserialize.InnerText;
            }

            if (extendedMemberInfo.RequiresRelationBuilding && memberType.IsKeyValuePair(out Type kvpKeyType, out Type kvpValueType))
            {
                Type kvpType = TypeHelpers.MakeKeyValuePairType(kvpKeyType, kvpValueType);
                string keyValue = itemToDeserialize.Attributes[persistData?.KeyAttribute ?? DatabasePersisterConstants.UNDEFINED_DICTIONARY_ITEM_KEY].Value;
                result = Activator.CreateInstance(kvpType, new object[] { Convert.ChangeType(keyValue, kvpKeyType), deserializedValue });
            }
            else
            {
                result = deserializedValue;
            }
            return true;

        }

    }
}
