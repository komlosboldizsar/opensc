using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Routers.Mirrors
{

    public abstract class RouterMirrorAssociationXmlSerializer<TAssociation, TSerialized> : IInstantiatingXmlSerializer
        where TAssociation : class
        where TSerialized : RouterMirrorAssociation<TAssociation>, new()
    {

        public virtual Type Type => typeof(TSerialized);

        private const string ATTRIBUTE_ITEM_A = "item_a";
        private const string ATTRIBUTE_ITEM_B = "item_b";

        public object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_ITEM_A]?.Value, out int itemAindex))
                itemAindex = -1;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_ITEM_B]?.Value, out int itemBindex))
                itemBindex = -1;
            return new TSerialized()
            {
                _itemA = itemAindex,
                _itemB = itemBindex
            };
        }

        public void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] indicesOrKeys)
        {
            if (xmlNode is not XmlElement xmlElement)
                return;
            if (item is not TSerialized itemCasted)
                return;
            xmlElement.SetAttribute(ATTRIBUTE_ITEM_A, getItemIndex(itemCasted.ItemA).ToString());
            xmlElement.SetAttribute(ATTRIBUTE_ITEM_B, getItemIndex(itemCasted.ItemB).ToString());
        }

        protected abstract int getItemIndex(TAssociation item);
        
    }

}
