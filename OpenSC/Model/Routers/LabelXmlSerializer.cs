using OpenSC.Model.Persistence;
using OpenSC.Model.Signals;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Routers
{

    class LabelXmlSerializer : IValueXmlSerializer
    {

        public Type Type => typeof(Label);

        private const string TAG_NAME = "label";
        private const string ATTRIBUTE_TEXT = "text";
        private const string ATTRIBUTE_ROUTER = "router";
        private const string ATTRIBUTE_INPUT = "input";

        public object DeserializeItem(XmlNode serializedItem)
        {
            
            if (serializedItem.LocalName != TAG_NAME)
                return null;

            string _routerId = serializedItem.Attributes[ATTRIBUTE_ROUTER]?.Value;
            if ((_routerId == null) || !int.TryParse(_routerId, out int routerId))
                return null;

            string _routerInputIndex = serializedItem.Attributes[ATTRIBUTE_INPUT]?.Value;
            if ((_routerInputIndex == null) || !int.TryParse(_routerInputIndex, out int routerInputIndex))
                return null;

            return new Label()
            {
                Text = serializedItem.Attributes[ATTRIBUTE_TEXT]?.Value,
                _routerId = routerId,
                _routerInputIndex = routerInputIndex
            };

        }

        public XElement SerializeItem(object item)
        {

            Label label = item as Label;
            if (label == null)
                return null;

            XElement xmlElement = new XElement(TAG_NAME);
            xmlElement.SetAttributeValue(ATTRIBUTE_TEXT, label.Text);
            xmlElement.SetAttributeValue(ATTRIBUTE_ROUTER, label.RouterInput.Router.ID);
            xmlElement.SetAttributeValue(ATTRIBUTE_INPUT, label.RouterInput.Index);

            return xmlElement;

        }
        
    }

}
