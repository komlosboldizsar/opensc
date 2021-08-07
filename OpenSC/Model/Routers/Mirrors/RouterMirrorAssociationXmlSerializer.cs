using OpenSC.Model.Persistence;
using OpenSC.Model.Signals;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Routers.Mirrors
{

    abstract class RouterMirrorAssociationXmlSerializer<TAssociation, TSerialized> : IValueXmlSerializer
        where TAssociation : class
        where TSerialized : RouterMirrorAssociation<TAssociation>, new()
    {

        public virtual Type Type => typeof(TSerialized);

        private const string TAG_NAME = "association";
        private const string ATTRIBUTE_ITEM_A = "item_a";
        private const string ATTRIBUTE_ITEM_B = "item_b";

        public object DeserializeItem(XmlNode serializedItem, object parentItem)
        {
            
            if (serializedItem.LocalName != TAG_NAME)
                return null;

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

        public XElement SerializeItem(object item, object parentItem)
        {

            TSerialized itemCasted = item as TSerialized;
            if (itemCasted == null)
                return null;

            XElement xmlElement = new XElement(TAG_NAME);
            xmlElement.SetAttributeValue(ATTRIBUTE_ITEM_A, getItemIndex(itemCasted.ItemA));
            xmlElement.SetAttributeValue(ATTRIBUTE_ITEM_B, getItemIndex(itemCasted.ItemB));

            return xmlElement;

        }

        protected abstract int getItemIndex(TAssociation item);
        
    }

}
