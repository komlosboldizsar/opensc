using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.GpioInterfaces
{

    public class GpioInterfaceOutputXmlSerializer : IInstantiatingXmlSerializer
    {

        public virtual Type Type => typeof(GpioInterfaceOutput);

        private const string ATTRIBUTE_INDEX = "index";
        private const string ATTRIBUTE_NAME = "name";
        private const string ATTRIBUTE_DRIVER = "driver";

        public virtual object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {
            GpioInterface parentGpioInterface = parentItem as GpioInterface;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_INDEX]?.Value, out int index))
                index = 0;
            GpioInterfaceOutput restoredOutput = parentGpioInterface.CreateOutput(serializedItem.Attributes[ATTRIBUTE_NAME]?.Value, index);
            restoredOutput._driverIdentifier = serializedItem.Attributes[ATTRIBUTE_DRIVER]?.Value;
            return restoredOutput;
        }

        public virtual void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] indicesOrKeys)
        {
            if (xmlNode is not XmlElement xmlElement)
                return;
            if (item is not GpioInterfaceOutput output)
                return;
            xmlElement.SetAttribute(ATTRIBUTE_INDEX, output.Index.ToString());
            xmlElement.SetAttribute(ATTRIBUTE_NAME, output.Name);
            xmlElement.SetAttribute(ATTRIBUTE_DRIVER, output.Driver?.Identifier);
        }

    }

}
