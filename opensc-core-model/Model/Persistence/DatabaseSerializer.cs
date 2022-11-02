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
            XmlDocument xmlDocument = new();
            XmlElement documentElement = xmlDocument.CreateElement(rootTag);
            xmlDocument.AppendChild(documentElement);
            foreach (T item in items)
                serializeItem(item, documentElement, xmlDocument);
            using XmlWriter writer = XmlWriter.Create(fileToWrite.Stream, xmlWriterSettings);
            xmlDocument.WriteTo(writer);
        }

        private XmlElement serializeItem(T item, XmlElement documentElement, XmlDocument xmlDocument)
        {
            XmlElement xmlElement = xmlDocument.CreateElement(itemTag);
            documentElement.AppendChild(xmlElement);
            Type typeToSerialize = storedType;
            xmlElement.SetAttribute(DatabasePersisterConstants.ATTRIBUTE_ID, item.ID.ToString());
            if (isPolymorph)
            {
                Type itemType = item.GetType();
                xmlElement.SetAttribute(DatabasePersisterConstants.ATTRIBUTE_TYPE, typeRegister.ConvertTypeToString(itemType));
                typeToSerialize = itemType;
            }
            foreach (ExtendedMemberInfo extendedMemberInfo in typeToSerialize.GetExtendedMemberInfos())
                storeValueOfMember(extendedMemberInfo, ref item, xmlElement, xmlDocument);
            return xmlElement;
        }

        private void storeValueOfMember(ExtendedMemberInfo extendedMemberInfo, ref T item, XmlElement xmlElement, XmlDocument xmlDocument)
        {
            if ((extendedMemberInfo.FieldInfo == null) && (extendedMemberInfo.PropertyInfo == null))
                return;
            if ((extendedMemberInfo.PropertyInfo != null) && !(extendedMemberInfo.PropertyInfo.CanRead && extendedMemberInfo.PropertyInfo.CanWrite))
                return;
            XmlNode xmlNode = getXmlNodeByXPath(xmlElement, xmlDocument, extendedMemberInfo.PersistAsAttributes[0].XPathPieces);
            serializeValue(extendedMemberInfo, extendedMemberInfo.ValueType, extendedMemberInfo.GetValue(item), item, xmlNode, xmlDocument);
        }

        private XmlNode getXmlNodeByXPath(XmlElement parentElement, XmlDocument documentElement, string[] xPathPieces)
        {
            XmlNode currentNode = parentElement;
            foreach (string xPathPiece in xPathPieces)
            {
                if (currentNode is not XmlElement currentElement)
                    throw new Exception("Probably invalid XPath.");
                XmlNode childNode;
                if (xPathPiece.StartsWith('@'))
                {
                    XmlAttribute childAttribute = documentElement.CreateAttribute(xPathPiece[1..]);
                    currentElement.Attributes.Append(childAttribute);
                    childNode = childAttribute;
                }
                else
                {
                    childNode = currentElement[xPathPiece];
                    if (childNode == null)
                    {
                        childNode = documentElement.CreateElement(xPathPiece);
                        currentElement.AppendChild(childNode);
                    }
                }
                currentNode = childNode;
            }
            return currentNode;
        }

        private void storeSerializedValueToXObject(XObject xObject, object serializedValue)
        {
            if (xObject is XElement xElement)
            {
                xElement.Add(serializedValue);
            }
            else if (xObject is XAttribute xAttribute)
            {
                if (serializedValue is not string serializedValueString)
                    throw new Exception("Only strings can be stored in XML attributes.");
                xAttribute.Value = serializedValueString;
            }
        }

        private void serializeValue(ExtendedMemberInfo extendedMemberInfo, Type memberType, object item, object parentItem, XmlNode rootNode, XmlDocument xmlDocument, object containingCollection = null, int arrayDimension = 0, object[] indices = null)
        {
            if (item == null)
            {
                rootNode.InnerText = string.Empty;
                return;
            }
            if ((item is ISystemObject itemAsSystemObject) && (extendedMemberInfo.PersistDetailedAttribute == null))
            {
                rootNode.InnerText = itemAsSystemObject.GlobalID;
                return;
            }
            object[] extendedIndices = DatabasePersisterHelpers.ExtendIndices(indices, arrayDimension);
            if (trySerializeAsArray(extendedMemberInfo, memberType, item, parentItem, rootNode, xmlDocument, arrayDimension, extendedIndices))
                return;
            if (trySerializeAsCollection(extendedMemberInfo, memberType, item, parentItem, rootNode, xmlDocument, arrayDimension, extendedIndices))
                return;
            if (trySerializeAsObject(extendedMemberInfo, memberType, item, parentItem, rootNode, xmlDocument, containingCollection, arrayDimension, indices))
                return;
            rootNode.InnerText = item.ToString();
        }

        private bool trySerializeAsArray(ExtendedMemberInfo extendedMemberInfo, Type memberType, object item, object parentItem, XmlNode rootNode, XmlDocument xmlDocument, int arrayDimension, object[] extendedIndices)
        {
            if (!memberType.IsArray || (item is not Array))
                return false;
            if (rootNode is not XmlElement rootElement)
                return false;
            Array array = item as Array;
            int elementIndex = 0;
            foreach (var element in array)
            {
                extendedIndices[arrayDimension] = elementIndex++;
                serializeCollectionElement(extendedMemberInfo, memberType.GetElementType(), element, parentItem, rootElement, xmlDocument, item, arrayDimension, extendedIndices);
            }
            return true;
        }

        private bool trySerializeAsCollection(ExtendedMemberInfo extendedMemberInfo, Type memberType, object item, object parentItem, XmlNode rootNode, XmlDocument xmlDocument, int arrayDimension, object[] extendedIndices)
        {
            if (rootNode is not XmlElement rootElement)
                return false;
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
            int elementIndex = 0;
            foreach (var element in enumerable)
            {
                extendedIndices[arrayDimension] = elementIndex++;
                serializeCollectionElement(extendedMemberInfo, elementType, element, parentItem, rootElement, xmlDocument, item, arrayDimension, extendedIndices);
            }
            return true;
        }

        private bool trySerializeAsObject(ExtendedMemberInfo extendedMemberInfo, Type memberType, object item, object parentItem, XmlNode rootNode, XmlDocument xmlDocument, object containingCollection, int arrayDimension, object[] indices)
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

            string subitemTypeString = null;
            if (containingCollection is IHeterogenousCollection heterogenousCollection)
            {
                subitemTypeString = heterogenousCollection.GetTypeCode(itemType);
                if (subitemTypeString != null)
                    serializeAsType = heterogenousCollection.GetType(itemTypeString);
            }

            IXmlSerializer serializer = SerializerRegister.GetSerializer(serializeAsType);
            if (serializer == null)
            {
                rootNode.InnerText = item.ToString();
                return true;
            }

            serializer.SerializeItem(item, parentItem, rootNode, xmlDocument, indices);
            if (rootNode is XmlElement rootElement)
            {
                if ((polymorphFieldAttribute?.TypeAttributeName != null) && (itemTypeString != null))
                    rootElement.SetAttribute(polymorphFieldAttribute.TypeAttributeName, itemTypeString);
                if (subitemTypeString != null)
                    rootElement.SetAttribute(DatabasePersisterConstants.HETEROGENOUS_COLLECTION_TYPE, subitemTypeString);
            }
            return true;

        }

        private void serializeCollectionElement(ExtendedMemberInfo extendedMemberInfo, Type memberType, object element, object parentItem, XmlElement rootElement, XmlDocument xmlDocument, object containingCollection, int arrayDimension, object[] indices)
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

            PersistAsAttribute persistData = extendedMemberInfo.GetPersistAsAttributeForDimension(arrayDimension + 1);
            XmlElement childElement = xmlDocument.CreateElement(persistData.XPath);
            rootElement.AppendChild(childElement);
            serializeValue(extendedMemberInfo, typeToSerialize, elementToSerialize, parentItem, childElement, xmlDocument, containingCollection, arrayDimension + 1, indices);
            if (elementKey != null)
                childElement.SetAttribute(persistData?.KeyAttribute ?? "key", (elementKey is ISystemObject elementKeyObj) ? elementKeyObj.GlobalID : elementKey.ToString());

        }

    }
}
