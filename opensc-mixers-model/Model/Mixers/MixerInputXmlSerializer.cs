using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Mixers
{

    public class MixerInputXmlSerializer : IInstantiatingXmlSerializer
    {

        public Type Type => typeof(MixerInput);

        private const string TAG_NAME = "input";
        private const string ATTRIBUTE_NAME = "name";
        private const string ATTRIBUTE_INDEX = "index";
        private const string ATTRIBUTE_SOURCE = "source";

        public object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
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

        public void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] indicesOrKeys)
        {
            if (xmlNode is not XmlElement xmlElement)
                return;
            if (item is not MixerInput input)
                return;
            xmlElement.SetAttribute(ATTRIBUTE_NAME, input.Name);
            xmlElement.SetAttribute(ATTRIBUTE_INDEX, input.Index.ToString());
            xmlElement.SetAttribute(ATTRIBUTE_SOURCE, input.Source?.SignalUniqueId ?? string.Empty);
        }

    }

}
