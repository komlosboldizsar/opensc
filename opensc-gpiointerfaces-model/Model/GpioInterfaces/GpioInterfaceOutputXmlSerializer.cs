using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.GpioInterfaces
{

    public class GpioInterfaceOutputXmlSerializer : ICompleteXmlSerializer
    {

        public virtual Type Type => typeof(GpioInterfaceOutput);

        private const string TAG_NAME = "output";
        private const string ATTRIBUTE_INDEX = "index";
        private const string ATTRIBUTE_NAME = "name";
        private const string ATTRIBUTE_DRIVER = "driver";

        public virtual object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {
            GpioInterface parentGpioInterface = parentItem as GpioInterface;
            if (serializedItem.LocalName != TAG_NAME)
                return null;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_INDEX]?.Value, out int index))
                index = 0;
            GpioInterfaceOutput restoredOutput = parentGpioInterface.CreateOutput(serializedItem.Attributes[ATTRIBUTE_NAME]?.Value, index);
            restoredOutput._driverIdentifier = serializedItem.Attributes[ATTRIBUTE_DRIVER]?.Value;
            return restoredOutput;
        }

        public virtual XElement SerializeItem(object item, object parentItem, object[] indicesOrKeys)
        {
            GpioInterfaceOutput output = item as GpioInterfaceOutput;
            if (output == null)
                return null;
            XElement xmlElement = new XElement(TAG_NAME);
            xmlElement.SetAttributeValue(ATTRIBUTE_INDEX, output.Index);
            xmlElement.SetAttributeValue(ATTRIBUTE_NAME, output.Name);
            xmlElement.SetAttributeValue(ATTRIBUTE_DRIVER, output.Driver?.Identifier);
            return xmlElement;
        }

    }

}
