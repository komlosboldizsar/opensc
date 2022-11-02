using OpenSC.Model.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
            //try
            //{
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
            //}
            //catch { }
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

            ConstructorInfo constructor = typeToDeserialize.GetConstructor(Array.Empty<Type>());
            if (constructor != null)
            {
                try
                {
                    item = (T)constructor.Invoke(Array.Empty<object>());
                }
                catch { }
            }

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
            XmlNode xmlNode = itemElement.SelectSingleNode(persistData.XPath);
            if (xmlNode is not XmlElement && xmlNode is not XmlAttribute)
                return;
            object originalValue = null;
            bool deserializeMembersOnly = (extendedMemberInfo.DeserializeMembersOnlyAttribute != null);
            if (deserializeMembersOnly)
                originalValue = extendedMemberInfo.GetValue(item);
            object value = deserializeXmlElement(extendedMemberInfo, extendedMemberInfo.ValueType, xmlNode, deserializeMembersOnly, originalValue, item);
            if (!deserializeMembersOnly)
            {
                try
                {
                    if (extendedMemberInfo.RequiresRelationBuilding)
                        foreignKeysOfItem.Add(extendedMemberInfo.MemberInfo.Name, value);
                    else
                        extendedMemberInfo.SetValue(item, value);
                }
                catch { }
            }
        }

        private object deserializeXmlElement(ExtendedMemberInfo extendedMemberInfo, Type memberType, XmlNode xmlNode, bool deserializeMembersOnly, object originalValue, object parentItem, object containerCollection = null, int arrayDimension = 0, object[] indices = null)
        {
            if (memberType == typeof(string))
                return xmlNode.InnerText;
            if ((xmlNode.InnerText == string.Empty) && PRIMITIVE_TYPES_NULL_IS_0.Contains(memberType))
                return 0;
            object result = originalValue;
            if (tryDeserializeAsCollection(ref result, extendedMemberInfo, memberType, xmlNode, deserializeMembersOnly, originalValue, parentItem, arrayDimension, indices))
                return result;
            if (tryDeserializeAsEnum(ref result, memberType, xmlNode))
                return result;
            if (tryDeserializeAsObject(ref result, extendedMemberInfo, memberType, xmlNode, deserializeMembersOnly, parentItem, containerCollection, arrayDimension, indices))
                return result;
            return Convert.ChangeType(xmlNode.InnerText, memberType);
        }

        private static readonly Type[] PRIMITIVE_TYPES_NULL_IS_0 = new Type[]
        {
            typeof(int),
            typeof(float),
            typeof(double),
            typeof(decimal)
        };

        private bool tryDeserializeAsCollection(ref object result, ExtendedMemberInfo extendedMemberInfo, Type memberType, XmlNode xmlNode, bool deserializeMembersOnly, object originalValue, object parentItem, int arrayDimension, object[] indices)
        {
            if (xmlNode is not XmlElement)
                return false;
            bool isCollection = memberType.GetInterfaces().Any(interfaceType => interfaceType.IsGenericType && (interfaceType.GetGenericTypeDefinition() == typeof(ICollection<>)));
            if (!memberType.IsArray && !isCollection)
                return false;
            IEnumerable<XmlElement> childElements = xmlNode.ChildNodes.OfType<XmlElement>();
            object[] extendedIndices = DatabasePersisterHelpers.ExtendIndices(indices, arrayDimension);
            if (tryDeserializeAsArray(ref result, extendedMemberInfo, memberType, childElements, deserializeMembersOnly, originalValue, parentItem, arrayDimension, extendedIndices))
                return true;
            result = deserializeCollection(extendedMemberInfo, memberType, childElements, deserializeMembersOnly, result, parentItem, arrayDimension, extendedIndices);
            return true;
        }

        private bool tryDeserializeAsArray(ref object result, ExtendedMemberInfo extendedMemberInfo, Type memberType, IEnumerable<XmlElement> childElements, bool deserializeMembersOnly, object originalValue, object parentItem, int arrayDimension, object[] extendedIndices)
        {
            if (!memberType.IsArray)
                return false;
            Type arrayElementType = memberType.GetElementType();
            Array array = deserializeMembersOnly ? null : Array.CreateInstance(arrayElementType, childElements.Count());
            object childOriginalValue = null;
            int childElementIndex = 0;
            foreach (XmlElement childElement in childElements)
            {
                extendedIndices[arrayDimension] = childElementIndex;
                if (deserializeMembersOnly && (originalValue is Array originalValueArray) && (originalValueArray.Length > childElementIndex))
                    childOriginalValue = originalValueArray.GetValue(childElementIndex);
                if (!deserializeMembersOnly || (childOriginalValue != null))
                {
                    object deserializedValue = deserializeXmlElement(extendedMemberInfo, arrayElementType, childElement, deserializeMembersOnly, childOriginalValue, parentItem, array, arrayDimension + 1, extendedIndices);
                    if (!deserializeMembersOnly)
                        array.SetValue(deserializedValue, childElementIndex);
                }
                childElementIndex++;
            }
            result = array;
            return true;
        }

       
        private object deserializeCollection(ExtendedMemberInfo extendedMemberInfo, Type memberType, IEnumerable<XmlElement> childElements, bool deserializeMembersOnly, object originalValue, object parentItem, int arrayDimension, object[] extendedIndices)
        {

            CollectionDetails collectionDetails = memberType.GetCollectionDetails();
            if (collectionDetails.IsCollection)
            {
                Type deserializeAsType = memberType;
                if (extendedMemberInfo.RequiresRelationBuilding && (extendedMemberInfo.PersistDetailedAttribute == null))
                {
                    Type keyType = collectionDetails.KeyType;
                    Type elementType = collectionDetails.ElementType;
                    if (keyType?.IsAssignableTo(typeof(ISystemObject)) == true)
                        keyType = typeof(string);
                    if (elementType.IsAssignableTo(typeof(ISystemObject)))
                        elementType = typeof(string);
                    collectionDetails = collectionDetails with { KeyType = keyType, ElementType = elementType };
                    deserializeAsType = collectionDetails.IsDictionary
                        ? typeof(Dictionary<,>).MakeGenericType(collectionDetails.AsTypeArray)
                        : typeof(List<>).MakeGenericType(collectionDetails.AsTypeArray);
                }
                else
                {
                    PersistSubclassAttribute persistSubclassAttribute = extendedMemberInfo.GetPersistSubclassAttributeForDimension(arrayDimension);
                    if (persistSubclassAttribute != null) // should check if given type is subclass of member type
                    {
                        MethodInfo subclassTypeGetterMethodInfo = parentItem.GetType().GetMethod(persistSubclassAttribute.SubclassTypeGetterName, DatabasePersisterConstants.MEMBER_LOOKUP_BINDING_FLAGS);
                        deserializeAsType = subclassTypeGetterMethodInfo.Invoke(parentItem, null) as Type;
                    }
                }
                object collection = null;
                MethodInfo addMethod = null;
                IEnumerator enumerableEnumerator = null;
                MethodInfo dictionaryTryGetMethod = null;
                if (deserializeMembersOnly)
                {
                    collection = originalValue;
                    if (collectionDetails.IsDictionary)
                        dictionaryTryGetMethod = deserializeAsType.GetMethod(nameof(IDictionary<object, object>.TryGetValue), collectionDetails.AsTypeArray);
                    else
                        enumerableEnumerator = (collection as IEnumerable)?.GetEnumerator();
                }
                else
                {
                    Type[] constructorTypeArgs = Array.Empty<Type>();
                    object[] constructorArgs = Array.Empty<object>();
                    if (collectionDetails.IsComponentCollection)
                    {
                        constructorTypeArgs = new Type[] { collectionDetails.ComponentOwnerType };
                        constructorArgs = new object[] { parentItem };
                    }
                    ConstructorInfo constructor = deserializeAsType.GetConstructor(constructorTypeArgs);
                    collection = (constructor != null)
                        ? constructor.Invoke(constructorArgs)
                        : Activator.CreateInstance(deserializeAsType, Array.Empty<object>()); // fix
                    addMethod = deserializeAsType.GetMethod(nameof(ICollection<object>.Add), collectionDetails.AsTypeArray);
                }
                int childElementIndex = 0;
                foreach (XmlElement childElement in childElements)
                {
                    object childOriginalValue = null;
                    extendedIndices[arrayDimension] = childElementIndex;
                    object deserializedKey = null;
                    if (collectionDetails.IsDictionary)
                    {
                        PersistAsAttribute persistData = extendedMemberInfo.GetPersistAsAttributeForDimension(arrayDimension + 1);
                        if (persistData == null)
                            persistData = new(DatabasePersisterConstants.UNDEFINED_ARRAY_ITEM_TAG);
                        deserializedKey = childElement.GetAttribute(persistData.KeyAttribute);
                        extendedIndices[arrayDimension] = deserializedKey;
                        if (deserializeMembersOnly)
                        {
                            object[] args = new object[] { deserializedKey, null };
                            if ((bool)dictionaryTryGetMethod.Invoke(collection, args))
                                childOriginalValue = args[1];
                        }
                    }
                    else
                    {
                        if ((enumerableEnumerator != null) && enumerableEnumerator.MoveNext())
                            childOriginalValue = enumerableEnumerator.Current;
                    }
                    if (!deserializeMembersOnly || (childOriginalValue != null))
                    {
                        object deserializedElement = deserializeXmlElement(extendedMemberInfo, collectionDetails.ElementType, childElement, deserializeMembersOnly, childOriginalValue, parentItem, collection, arrayDimension + 1, extendedIndices);
                        if (!deserializeMembersOnly)
                        {
                            object[] addArguments = null;
                            if (collectionDetails.IsDictionary && (deserializedKey != null))
                                addArguments = new object[] { deserializedKey, deserializedElement };
                            else if (!collectionDetails.IsDictionary)
                                addArguments = new object[] { deserializedElement };
                            if (addArguments != null)
                                addMethod.Invoke(collection, addArguments);
                        }
                    }
                    childElementIndex++;
                }
                return collection;
            }

            return null;

        }

        private bool tryDeserializeAsEnum(ref object result, Type memberType, XmlNode xmlNode)
        {
            if (memberType.IsEnum)
            {
                result = Enum.Parse(memberType, xmlNode.InnerText);
                return true;
            }
            Type nullableUnderlyingType = Nullable.GetUnderlyingType(memberType);
            if (nullableUnderlyingType?.IsEnum == true)
            {
                result = (xmlNode.InnerText == string.Empty)
                    ? null
                    : Enum.Parse(nullableUnderlyingType, xmlNode.InnerText);
                return true;
            }
            return false;
        }

        private bool tryDeserializeAsObject(ref object result, ExtendedMemberInfo extendedMemberInfo, Type memberType, XmlNode xmlNode, bool deserializeMembersOnly, object parentItem, object containerCollection, int arrayDimension, object[] indices)
        {

            XmlElement xmlElement = xmlNode as XmlElement;

            Type deserializeAsType = memberType;
            if (Type.GetTypeCode(memberType) != TypeCode.Object)
                return false;

            PersistSubclassAttribute persistSubclassAttribute = extendedMemberInfo.GetPersistSubclassAttributeForDimension(arrayDimension);
            if (persistSubclassAttribute != null) // should check if given type is subclass of member type
            {
                MethodInfo subclassTypeGetterMethodInfo = parentItem.GetType().GetMethod(persistSubclassAttribute.SubclassTypeGetterName, DatabasePersisterConstants.MEMBER_LOOKUP_BINDING_FLAGS);
                deserializeAsType = subclassTypeGetterMethodInfo.Invoke(parentItem, null) as Type;
            }

            if (xmlElement != null)
            {
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
                if (containerCollection is IHeterogenousCollection heterogenousCollection)
                {
                    string itemTypeString = xmlElement.GetAttribute(DatabasePersisterConstants.HETEROGENOUS_COLLECTION_TYPE);
                    deserializeAsType = heterogenousCollection.GetType(itemTypeString);
                }
            }

            PersistAsAttribute persistData = extendedMemberInfo.GetPersistAsAttributeForDimension(arrayDimension);
            object deserializedValue = xmlNode.InnerText;

            (IInstantiatingXmlSerializer completeSerializer, IValueOnlyXmlSerializer memberSerializer) =
                SerializerRegister.GetDeserializer(deserializeAsType, !deserializeMembersOnly);

            if (completeSerializer != null)
            {
                if (extendedMemberInfo.CanDeserializeElement)
                    deserializedValue = completeSerializer.DeserializeItem(xmlNode, parentItem, indices);
                else
                    deserializedValue = xmlNode.InnerText;
            }
            else if (memberSerializer != null)
            {
                deserializedValue = deserializeMembersOnly ? result : Activator.CreateInstance(deserializeAsType);
                memberSerializer.DeserializeItem(xmlNode, deserializedValue, parentItem, indices);
            }

            if (extendedMemberInfo.RequiresRelationBuilding && memberType.IsKeyValuePair(out Type kvpKeyType, out Type kvpValueType))
            {
                Type kvpType = TypeHelpers.MakeKeyValuePairType(kvpKeyType, kvpValueType);
                string keyValue = xmlNode.Attributes[persistData?.KeyAttribute ?? DatabasePersisterConstants.UNDEFINED_DICTIONARY_ITEM_KEY].Value;
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
