using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Routers
{

    public class RouterOutputXmlSerializer : IInstantiatingXmlSerializer
    {

        public virtual Type Type => typeof(RouterOutput);

        private const string ATTRIBUTE_INDEX = "index";
        private const string ATTRIBUTE_NAME = "name";

        public virtual object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {
            Router parentRouter = parentItem as Router;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_INDEX]?.Value, out int index))
                index = 0;
            RouterOutput restoredOutput = parentRouter.Outputs.CreateEmptyInstance();
            restoredOutput.Name = serializedItem.Attributes[ATTRIBUTE_NAME]?.Value;
            restoredOutput.Index = index;
            return restoredOutput;
        }

        public virtual void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] indicesOrKeys)
        {
            if (xmlNode is not XmlElement xmlElement)
                return;
            if (item is not RouterOutput output)
                return;
            xmlElement.SetAttribute(ATTRIBUTE_INDEX, output.Index.ToString());
            xmlElement.SetAttribute(ATTRIBUTE_NAME, output.Name);
        }

    }

}
