using OpenSC.Model.Persistence;
using OpenSC.Model.Signals;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Routers
{

    public class RouterInputXmlSerializer : IValueXmlSerializer
    {

        public Type Type => typeof(RouterInput);

        private const string TAG_NAME = "input";
        private const string ATTRIBUTE_INDEX = "index";
        private const string ATTRIBUTE_NAME = "name";
        private const string ATTRIBUTE_SOURCE = "source";
        private const string ATTRIBUTE_TIELINE_COST = "tieline_cost";
        private const string ATTRIBUTE_TIELINE_RESERVED = "tieline_reserved";

        public object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {

            Router parentRouter = parentItem as Router;

            if (serializedItem.LocalName != TAG_NAME)
                return null;

            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_INDEX]?.Value, out int index))
                index = 0;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_TIELINE_COST]?.Value, out int tielineCost))
                tielineCost = 0;
            if (!bool.TryParse(serializedItem.Attributes[ATTRIBUTE_TIELINE_RESERVED]?.Value, out bool tielineIsReserved))
                tielineIsReserved = false;

            RouterInput restoredInput = parentRouter.CreateInput(serializedItem.Attributes[ATTRIBUTE_NAME]?.Value, index);
            restoredInput._sourceUniqueId = serializedItem.Attributes[ATTRIBUTE_SOURCE]?.Value;
            restoredInput._tielineCost = tielineCost;
            restoredInput._tielineIsReserved = tielineIsReserved;

            return restoredInput;

        }

        public XElement SerializeItem(object item, object parentItem, object[] indicesOrKeys)
        {

            RouterInput input = item as RouterInput;
            if (input == null)
                return null;

            XElement xmlElement = new XElement(TAG_NAME);
            xmlElement.SetAttributeValue(ATTRIBUTE_INDEX, input.Index);
            xmlElement.SetAttributeValue(ATTRIBUTE_NAME, input.Name);
            xmlElement.SetAttributeValue(ATTRIBUTE_SOURCE, (input.CurrentSource as ISignalSourceRegistered)?.SignalUniqueId);
            xmlElement.SetAttributeValue(ATTRIBUTE_TIELINE_COST, input.TielineCost?.ToString());
            xmlElement.SetAttributeValue(ATTRIBUTE_TIELINE_RESERVED, input.TielineIsReserved?.ToString());

            return xmlElement;

        }
        
    }

}
