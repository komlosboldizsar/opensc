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
        private const string ATTRIBUTE_TIELINE_COST = "tieline_cost";
        private const string ATTRIBUTE_TIELINE_RESERVED = "tieline_reserved";

        public object DeserializeItem(XmlNode serializedItem)
        {
            if (serializedItem.LocalName != TAG_NAME)
                return null;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_TIELINE_COST]?.Value, out int tielineCost))
                tielineCost = 0;
            if (!bool.TryParse(serializedItem.Attributes[ATTRIBUTE_TIELINE_RESERVED]?.Value, out bool tielineIsReserved))
                tielineIsReserved = false;
            return new RouterInput()
            {
                Name = serializedItem.Attributes[ATTRIBUTE_NAME]?.Value,
                _sourceSignalUniqueId = serializedItem.Attributes[ATTRIBUTE_SOURCE]?.Value,
                _tielineCost = tielineCost,
                _tielineIsReserved = tielineIsReserved
            };
        }

        public XElement SerializeItem(object item)
        {

            RouterInput input = item as RouterInput;
            if (input == null)
                return null;

            XElement xmlElement = new XElement(TAG_NAME);
            xmlElement.SetAttributeValue(ATTRIBUTE_NAME, input.Name);
            xmlElement.SetAttributeValue(ATTRIBUTE_SOURCE, (input.CurrentSource as ISignalSourceRegistered)?.SignalUniqueId);
            xmlElement.SetAttributeValue(ATTRIBUTE_TIELINE_COST, input.TielineCost?.ToString());
            xmlElement.SetAttributeValue(ATTRIBUTE_TIELINE_RESERVED, input.TielineIsReserved?.ToString());

            return xmlElement;

        }
        
    }

}
