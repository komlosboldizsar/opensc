using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Routers
{

    public class RouterOutputXmlSerializer : IValueXmlSerializer
    {

        public virtual Type Type => typeof(RouterOutput);

        private const string TAG_NAME = "output";
        private const string ATTRIBUTE_INDEX = "index";
        private const string ATTRIBUTE_NAME = "name";

        public virtual object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {

            Router parentRouter = parentItem as Router;

            if (serializedItem.LocalName != TAG_NAME)
                return null;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_INDEX]?.Value, out int index))
                index = 0;

            RouterOutput restoredOutput = parentRouter.CreateOutput(serializedItem.Attributes[ATTRIBUTE_NAME]?.Value, index);
            return restoredOutput;

        }

        public virtual XElement SerializeItem(object item, object parentItem, object[] indicesOrKeys)
        {

            RouterOutput output = item as RouterOutput;
            if (output == null)
                return null;

            XElement xmlElement = new XElement(TAG_NAME);
            xmlElement.SetAttributeValue(ATTRIBUTE_INDEX, output.Index);
            xmlElement.SetAttributeValue(ATTRIBUTE_NAME, output.Name);
            return xmlElement;

        }

    }

}
