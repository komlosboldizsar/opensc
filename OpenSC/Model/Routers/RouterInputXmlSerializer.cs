using OpenSC.Model.Persistence;
using OpenSC.Model.Signals;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Routers
{

    class RouterInputXmlSerializer : IValueXmlSerializer
    {

        public Type Type => typeof(RouterInput);

        private const string TAG_NAME = "router_input";
        private const string ATTRIBUTE_NAME = "name";
        private const string ATTRIBUTE_SOURCE = "source";

        public object DeserializeItem(XmlNode serializedItem)
        {
            if (serializedItem.LocalName != TAG_NAME)
                return null;
            return new RouterInput()
            {
                Name = serializedItem.Attributes[ATTRIBUTE_NAME]?.Value,
                _sourceSignalUniqueId = serializedItem.Attributes[ATTRIBUTE_SOURCE]?.Value
            };
        }

        public XElement SerializeItem(object item)
        {

            RouterInput input = item as RouterInput;
            if (input == null)
                return null;

            XElement xmlElement = new XElement(TAG_NAME);
            xmlElement.SetAttributeValue(ATTRIBUTE_NAME, input.Name);
            xmlElement.SetAttributeValue(ATTRIBUTE_SOURCE, input.Source?.SignalUniqueId);

            return xmlElement;

        }
        
    }

}
