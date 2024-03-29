﻿using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.GpioInterfaces
{

    public class GpioInterfaceInputXmlSerializer : IValueXmlSerializer
    {

        public virtual Type Type => typeof(GpioInterfaceInput);

        private const string TAG_NAME = "input";
        private const string ATTRIBUTE_INDEX = "index";
        private const string ATTRIBUTE_NAME = "name";
        private const string ATTRIBUTE_DEBOUNCE_TIME = "debounce_time";

        public virtual object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {
            GpioInterface parentGpioInterface = parentItem as GpioInterface;
            if (serializedItem.LocalName != TAG_NAME)
                return null;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_INDEX]?.Value, out int index))
                index = 0;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_DEBOUNCE_TIME]?.Value, out int debounceTime))
                debounceTime = 0;
            GpioInterfaceInput restoredInput = parentGpioInterface.CreateInput(serializedItem.Attributes[ATTRIBUTE_NAME]?.Value, index);
            restoredInput.DebounceTime = debounceTime;
            return restoredInput;
        }

        public virtual XElement SerializeItem(object item, object parentItem, object[] indicesOrKeys)
        {
            GpioInterfaceInput input = item as GpioInterfaceInput;
            if (input == null)
                return null;
            XElement xmlElement = new XElement(TAG_NAME);
            xmlElement.SetAttributeValue(ATTRIBUTE_INDEX, input.Index);
            xmlElement.SetAttributeValue(ATTRIBUTE_NAME, input.Name);
            xmlElement.SetAttributeValue(ATTRIBUTE_DEBOUNCE_TIME, input.DebounceTime);
            return xmlElement;
        }
        
    }

}
