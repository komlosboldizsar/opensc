using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Routers.Salvos
{

    public class SalvoCrosspointXmlSerializer : IValueXmlSerializer
    {

        public virtual Type Type => typeof(SalvoCrosspoint);

        private const string TAG_NAME = "crosspoint";
        private const string ATTRIBUTE_ROUTER = "router";
        private const string ATTRIBUTE_OUTPUT = "output";
        private const string ATTRIBUTE_INPUT = "input";

        public virtual object DeserializeItem(XmlNode serializedItem, object parentItem)
        {
            if (serializedItem.LocalName != TAG_NAME)
                return null;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_ROUTER]?.Value, out int routerId))
                routerId = 0;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_OUTPUT]?.Value, out int outputIndex))
                outputIndex = 0;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_INPUT]?.Value, out int inputIndex))
                inputIndex = 0;
            SalvoCrosspoint crosspoint = new SalvoCrosspoint()
            {
                __routerId = routerId,
                __outputIndex = outputIndex,
                __inputIndex = inputIndex
            };
            return crosspoint;
        }

        public virtual XElement SerializeItem(object item, object parentItem)
        {
            SalvoCrosspoint crosspoint = item as SalvoCrosspoint;
            if (crosspoint == null)
                return null;
            XElement xmlElement = new XElement(TAG_NAME);
            xmlElement.SetAttributeValue(ATTRIBUTE_ROUTER, crosspoint.Router?.ID.ToString() ?? "");
            xmlElement.SetAttributeValue(ATTRIBUTE_OUTPUT, crosspoint.Output?.Index.ToString() ?? "");
            xmlElement.SetAttributeValue(ATTRIBUTE_INPUT, crosspoint.Input?.Index.ToString() ?? "");
            return xmlElement;
        }

    }

}
