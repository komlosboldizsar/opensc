using OpenSC.Model.Persistence;
using System;
using System.Xml;

namespace OpenSC.Model.GpioInterfaces
{

    public class GpioInterfaceInputXmlSerializer : IInstantiatingXmlSerializer
    {

        public virtual Type Type => typeof(GpioInterfaceInput);

        private const string ATTRIBUTE_INDEX = "index";
        private const string ATTRIBUTE_NAME = "name";
        private const string ATTRIBUTE_DEBOUNCE_TIME = "debounce_time";

        public virtual object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {
            GpioInterface parentGpioInterface = parentItem as GpioInterface;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_INDEX]?.Value, out int index))
                index = 0;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_DEBOUNCE_TIME]?.Value, out int debounceTime))
                debounceTime = 0;
            GpioInterfaceInput restoredInput = parentGpioInterface.CreateInput(serializedItem.Attributes[ATTRIBUTE_NAME]?.Value, index);
            restoredInput.DebounceTime = debounceTime;
            return restoredInput;
        }

        public virtual void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] indicesOrKeys)
        {
            if (xmlNode is not XmlElement xmlElement)
                return;
            if (item is not GpioInterfaceInput input)
                return;
            xmlElement.SetAttribute(ATTRIBUTE_INDEX, input.Index.ToString());
            xmlElement.SetAttribute(ATTRIBUTE_NAME, input.Name);
            xmlElement.SetAttribute(ATTRIBUTE_DEBOUNCE_TIME, input.DebounceTime.ToString());
        }
        
    }

}
