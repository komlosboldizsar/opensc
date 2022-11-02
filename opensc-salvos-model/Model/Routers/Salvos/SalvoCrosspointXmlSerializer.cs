using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Routers.Salvos
{

    public class SalvoCrosspointXmlSerializer : IInstantiatingXmlSerializer
    {

        public virtual Type Type => typeof(SalvoCrosspoint);

        private const string ATTRIBUTE_ROUTER = "router";
        private const string ATTRIBUTE_OUTPUT = "output";
        private const string ATTRIBUTE_INPUT = "input";

        public virtual object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {
            int? routerId = null;
            int? outputIndex = null;
            int? inputIndex = null;
            if (int.TryParse(serializedItem.Attributes[ATTRIBUTE_ROUTER]?.Value, out int _routerId))
                routerId = _routerId;
            if (int.TryParse(serializedItem.Attributes[ATTRIBUTE_OUTPUT]?.Value, out int _outputIndex))
                outputIndex = _outputIndex;
            if (int.TryParse(serializedItem.Attributes[ATTRIBUTE_INPUT]?.Value, out int _inputIndex))
                inputIndex = _inputIndex;
            SalvoCrosspoint crosspoint = new()
            {
                __routerId = routerId,
                __outputIndex = outputIndex,
                __inputIndex = inputIndex
            };
            return crosspoint;
        }

        public virtual void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] indicesOrKeys)
        {
            if (xmlNode is not XmlElement xmlElement)
                return;
            if (item is not SalvoCrosspoint crosspoint)
                return;
            xmlElement.SetAttribute(ATTRIBUTE_ROUTER, crosspoint.Router?.ID.ToString() ?? "");
            xmlElement.SetAttribute(ATTRIBUTE_OUTPUT, crosspoint.Output?.Index.ToString() ?? "");
            xmlElement.SetAttribute(ATTRIBUTE_INPUT, crosspoint.Input?.Index.ToString() ?? "");
        }

    }

}
