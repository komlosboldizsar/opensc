﻿using OpenSC.Model.General;
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
    internal class DatabaseSerializer<T>
        where T : class, IModel
    {

        private readonly bool isPolymorph;
        private readonly IModelTypeRegister typeRegister;
        private readonly string rootTag;
        private readonly string itemTag;

        public DatabaseSerializer(bool isPolymorph, IModelTypeRegister typeRegister, string rootTag, string itemTag)
        {
            this.isPolymorph = isPolymorph;
            this.typeRegister = typeRegister;
            this.rootTag = rootTag;
            this.itemTag = itemTag;
        }

        private static readonly Type storedType = typeof(T);
        private static readonly XmlWriterSettings xmlWriterSettings = new()
        {
            CloseOutput = false,
            Indent = true,
            IndentChars = "\t"
        };

        public void Save(IEnumerable<T> items, DatabaseFile fileToWrite)
        {
            XElement rootElement = new(rootTag);
            foreach (T item in items)
                rootElement.Add(serializeItem(item));
            using XmlWriter writer = XmlWriter.Create(fileToWrite.Stream, xmlWriterSettings);
            rootElement.WriteTo(writer);
        }

        private XElement serializeItem(T item)
        {
            XElement xmlElement = new(itemTag);
            Type typeToSerialize = storedType;
            xmlElement.SetAttributeValue(DatabasePersisterConstants.ATTRIBUTE_ID, item.ID);
            if (isPolymorph)
            {
                Type itemType = item.GetType();
                xmlElement.SetAttributeValue(DatabasePersisterConstants.ATTRIBUTE_TYPE, typeRegister.ConvertTypeToString(itemType));
                typeToSerialize = itemType;
            }
            foreach (ExtendedMemberInfo extendedMemberInfo in typeToSerialize.GetExtendedMemberInfos())
                storeValueOfMember(extendedMemberInfo, ref item, ref xmlElement);
            return xmlElement;
        }

        private void storeValueOfMember(ExtendedMemberInfo extendedMemberInfo, ref T item, ref XElement xmlElement)
        {
            if ((extendedMemberInfo.FieldInfo == null) && (extendedMemberInfo.PropertyInfo == null))
                return;
            if ((extendedMemberInfo.PropertyInfo != null) && !(extendedMemberInfo.PropertyInfo.CanRead && extendedMemberInfo.PropertyInfo.CanWrite))
                return;
            object xmlElementInner = serializeValue(extendedMemberInfo, extendedMemberInfo.ValueType, extendedMemberInfo.GetValue(item), item);
            xmlElement.Add(new XElement(extendedMemberInfo.PersistAsAttributes[0].TagName, xmlElementInner));
        }

        private object serializeValue(ExtendedMemberInfo extendedMemberInfo, Type memberType, object item, object parentItem, object containingCollection = null, int arrayDimension = 0, object[] indices = null)
        {
            if (item == null)
                return string.Empty;
            if ((item is ISystemObject itemAsSystemObject) && (extendedMemberInfo.PersistDetailedAttribute == null))
                return itemAsSystemObject.GlobalID;
            object[] extendedIndices = DatabasePersisterHelpers.ExtendIndices(indices, arrayDimension);
            object result = null;
            if (trySerializeAsArray(ref result, extendedMemberInfo, memberType, item, parentItem, arrayDimension, extendedIndices))
                return result;
            if (trySerializeAsCollection(ref result, extendedMemberInfo, memberType, item, parentItem, arrayDimension, extendedIndices))
                return result;
            if (trySerializeAsObject(ref result, extendedMemberInfo, memberType, item, parentItem, containingCollection, arrayDimension, indices))
                return result;
            return item.ToString();
        }

        private bool trySerializeAsArray(ref object result, ExtendedMemberInfo extendedMemberInfo, Type memberType, object item, object parentItem, int arrayDimension, object[] extendedIndices)
        {
            if (!memberType.IsArray || (item is not Array))
                return false;
            Array array = item as Array;
            List<XElement> arrayElements = new();
            int elementIndex = 0;
            foreach (var element in array)
            {
                extendedIndices[arrayDimension] = elementIndex++;
                arrayElements.Add(serializeCollectionElement(extendedMemberInfo, memberType.GetElementType(), element, parentItem, item, arrayDimension, extendedIndices));
            }
            result = arrayElements;
            return true;
        }

        private bool trySerializeAsCollection(ref object result, ExtendedMemberInfo extendedMemberInfo, Type memberType, object item, object parentItem, int arrayDimension, object[] extendedIndices)
        {
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
            if (!isCollection)
                return false;
            IEnumerable enumerable = item as IEnumerable;
            List<XElement> arrayElements = new();
            int elementIndex = 0;
            foreach (var element in enumerable)
            {
                extendedIndices[arrayDimension] = elementIndex++;
                arrayElements.Add(serializeCollectionElement(extendedMemberInfo, elementType, element, parentItem, item, arrayDimension, extendedIndices));
            }
            result = arrayElements;
            return true;
        }

        private bool trySerializeAsObject(ref object result, ExtendedMemberInfo extendedMemberInfo, Type memberType, object item, object parentItem, object containingCollection, int arrayDimension, object[] indices)
        {

            if (Type.GetTypeCode(memberType) != TypeCode.Object)
                return false;

            Type itemType = item.GetType();
            Type serializeAsType = memberType;

            PersistSubclassAttribute persistSubclassAttribute = extendedMemberInfo.GetPersistSubclassAttributeForDimension(arrayDimension);
            if (persistSubclassAttribute != null) // should check if given type is subclass of member type
            {
                MethodInfo subclassTypeGetterMethodInfo = parentItem.GetType().GetMethod(persistSubclassAttribute.SubclassTypeGetterName, DatabasePersisterConstants.MEMBER_LOOKUP_BINDING_FLAGS);
                serializeAsType = subclassTypeGetterMethodInfo.Invoke(parentItem, null) as Type;
            }

            PolymorphFieldAttribute polymorphFieldAttribute = extendedMemberInfo.PolymorphFieldAttribute;
            string itemTypeString = null;
            if (polymorphFieldAttribute != null)
            {
                MethodInfo typeStringDictionaryGetterMethodInfo = parentItem.GetType().GetMethod(polymorphFieldAttribute.TypeStringDictionaryGetterName, DatabasePersisterConstants.MEMBER_LOOKUP_BINDING_FLAGS);
                Dictionary<Type, string> typeStringDictionary = typeStringDictionaryGetterMethodInfo.Invoke(parentItem, Array.Empty<object>()) as Dictionary<Type, string>;
                if (typeStringDictionary?.TryGetValue(itemType, out itemTypeString) == true)
                    serializeAsType = itemType;
            }


            if (extendedMemberInfo.MemberInfo.Name == "Texts")
                Debug.WriteLine("hello");

            string subitemTypeString = null;
            if (containingCollection is IHeterogenousCollection heterogenousCollection)
            {
                subitemTypeString = heterogenousCollection.GetTypeCode(itemType);
                if (subitemTypeString != null)
                    serializeAsType = heterogenousCollection.GetType(itemTypeString);
            }

            ICompleteXmlSerializer completeSerializer = SerializerRegister.GetCompleteSerializerForType(serializeAsType);
            IMemberXmlSerializer memberSerializer = (completeSerializer == null) ? SerializerRegister.GetMemberSerializerForType(serializeAsType) : null;
            if ((completeSerializer == null) && (memberSerializer == null))
            {
                result = item.ToString();
                return true;
            }

            XElement serializedItem = (completeSerializer != null)
                ? completeSerializer.SerializeItem(item, parentItem, indices)
                : memberSerializer.SerializeItem(item, parentItem, indices);
            if ((polymorphFieldAttribute?.TypeAttributeName != null) && (itemTypeString != null))
                serializedItem.SetAttributeValue(polymorphFieldAttribute.TypeAttributeName, itemTypeString);
            if (subitemTypeString != null)
                serializedItem.SetAttributeValue(DatabasePersisterConstants.HETEROGENOUS_COLLECTION_TYPE, subitemTypeString);
            result = serializedItem;
            return true;

        }

        private XElement serializeCollectionElement(ExtendedMemberInfo extendedMemberInfo, Type memberType, object element, object parentItem, object containingCollection, int arrayDimension, object[] indices)
        {

            object elementKey = null;
            object elementToSerialize = element;
            Type typeToSerialize = memberType;
            Type elementType = element.GetType();
            if (elementType.IsGenericType && (elementType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>)))
            {
                PropertyInfo keyPropertyInfo = elementType.GetProperty(nameof(KeyValuePair<object, object>.Key));
                PropertyInfo valuePropertyInfo = elementType.GetProperty(nameof(KeyValuePair<object, object>.Value));
                elementKey = keyPropertyInfo.GetValue(element);
                elementToSerialize = valuePropertyInfo.GetValue(element);
                typeToSerialize = elementType.GetGenericArguments()[1];
                indices[arrayDimension] = elementKey;
            }

            object arrayElementValue = serializeValue(extendedMemberInfo, typeToSerialize, elementToSerialize, parentItem, containingCollection, arrayDimension + 1, indices);
            PersistAsAttribute persistData = extendedMemberInfo.GetPersistAsAttributeForDimension(arrayDimension + 1);
            XElement returnElement = ((arrayElementValue is XElement arrayEleentValueX) && (persistData?.TagName == null))
                ? arrayEleentValueX
                : new XElement(persistData?.TagName ?? DatabasePersisterConstants.UNDEFINED_ARRAY_ITEM_TAG, arrayElementValue);
            if (elementKey != null)
                returnElement.SetAttributeValue(persistData?.KeyAttribute ?? "key", (elementKey is ISystemObject elementKeyObj) ? elementKeyObj.GlobalID : elementKey.ToString());

            return returnElement;

        }

    }
}
