using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Mixers
{

    public class MixerInputXmlSerializer : IValueXmlSerializer
    {

        public Type Type => typeof(MixerInput);

        private const string TAG_NAME = "input";
        private const string ATTRIBUTE_NAME = "name";
        private const string ATTRIBUTE_INDEX = "index";
        private const string ATTRIBUTE_SOURCE = "source";

        public object DeserializeItem(XmlNode serializedItem, object parentItem)
        {

            if (serializedItem.LocalName != TAG_NAME)
                return null;

            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_INDEX]?.Value, out int index))
                index = 1;

            string sourceSignalUniqueIdAttributeValue = serializedItem.Attributes[ATTRIBUTE_SOURCE]?.Value;
            string sourceSignalUniqueId = (sourceSignalUniqueIdAttributeValue != string.Empty) ? sourceSignalUniqueIdAttributeValue : null;

            return new MixerInput()
            {
                Name = serializedItem.Attributes[ATTRIBUTE_NAME]?.Value,
                Index = index,
                _sourceSignalUniqueId = sourceSignalUniqueId
            };

        }

        public XElement SerializeItem(object item, object parentItem)
        {

            MixerInput input = item as MixerInput;
            if (input == null)
                return null;

            XElement xmlElement = new XElement(TAG_NAME);
            xmlElement.SetAttributeValue(ATTRIBUTE_NAME, input.Name);
            xmlElement.SetAttributeValue(ATTRIBUTE_INDEX, input.Index);
            xmlElement.SetAttributeValue(ATTRIBUTE_SOURCE, ((input.Source != null) ? input.Source.SignalUniqueId : ""));

            return xmlElement;

        }

    }

}
