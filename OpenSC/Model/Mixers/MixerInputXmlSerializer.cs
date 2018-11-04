using OpenSC.Model.Persistence;
using OpenSC.Model.Signals;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Mixers
{

    class MixerInputXmlSerializer : IValueXmlSerializer
    {

        public Type Type => typeof(MixerInput);

        private const string TAG_NAME = "mixer_input";
        private const string ATTRIBUTE_NAME = "name";
        private const string ATTRIBUTE_INDEX = "index";
        private const string ATTRIBUTE_SOURCE = "source";

        public object DeserializeItem(XmlNode serializedItem)
        {

            if (serializedItem.LocalName != TAG_NAME)
                return null;

            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_INDEX]?.Value, out int index))
                index = 1;

            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_SOURCE]?.Value, out int sourceSignalId))
                sourceSignalId = 0;

            return new MixerInput()
            {
                Name = serializedItem.Attributes[ATTRIBUTE_NAME]?.Value,
                Index = index,
                _sourceSignalId = sourceSignalId
            };

        }

        public XElement SerializeItem(object item)
        {

            MixerInput input = item as MixerInput;
            if (input == null)
                return null;

            XElement xmlElement = new XElement(TAG_NAME);
            xmlElement.SetAttributeValue(ATTRIBUTE_NAME, input.Name);
            xmlElement.SetAttributeValue(ATTRIBUTE_INDEX, input.Index);
            xmlElement.SetAttributeValue(ATTRIBUTE_SOURCE, ((input.Source != null) ? input.Source.ID.ToString() : ""));

            return xmlElement;

        }

    }

}
