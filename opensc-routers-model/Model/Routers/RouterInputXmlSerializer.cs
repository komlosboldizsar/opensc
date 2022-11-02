using OpenSC.Model.Persistence;
using OpenSC.Model.Signals;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Routers
{

    public class RouterInputXmlSerializer : IInstantiatingXmlSerializer
    {

        public Type Type => typeof(RouterInput);

        private const string ATTRIBUTE_INDEX = "index";
        private const string ATTRIBUTE_NAME = "name";
        private const string ATTRIBUTE_SOURCE = "source";
        private const string ATTRIBUTE_TIELINE_COST = "tieline_cost";
        private const string ATTRIBUTE_TIELINE_RESERVED = "tieline_reserved";

        public object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {

            Router parentRouter = parentItem as Router;

            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_INDEX]?.Value, out int index))
                index = 0;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_TIELINE_COST]?.Value, out int tielineCost))
                tielineCost = 0;
            if (!bool.TryParse(serializedItem.Attributes[ATTRIBUTE_TIELINE_RESERVED]?.Value, out bool tielineIsReserved))
                tielineIsReserved = false;

            RouterInput restoredInput = parentRouter.Inputs.CreateEmptyInstance();
            restoredInput.Name = serializedItem.Attributes[ATTRIBUTE_NAME]?.Value;
            restoredInput.Index = index;
            restoredInput._sourceUniqueId = serializedItem.Attributes[ATTRIBUTE_SOURCE]?.Value;
            restoredInput._tielineCost = tielineCost;
            restoredInput._tielineIsReserved = tielineIsReserved;

            return restoredInput;

        }

        public void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] indicesOrKeys)
        {
            if (xmlNode is not XmlElement xmlElement)
                return;
            if (item is not RouterInput input)
                return;
            xmlElement.SetAttribute(ATTRIBUTE_INDEX, input.Index.ToString());
            xmlElement.SetAttribute(ATTRIBUTE_NAME, input.Name);
            xmlElement.SetAttribute(ATTRIBUTE_SOURCE, (input.CurrentSource as ISignalSourceRegistered)?.SignalUniqueId);
            xmlElement.SetAttribute(ATTRIBUTE_TIELINE_COST, input.TielineCost?.ToString());
            xmlElement.SetAttribute(ATTRIBUTE_TIELINE_RESERVED, input.TielineIsReserved?.ToString());
        }
        
    }

}
