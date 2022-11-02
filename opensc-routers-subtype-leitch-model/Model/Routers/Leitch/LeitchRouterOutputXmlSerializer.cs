using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Routers.Leitch
{

    public class LeitchRouterOutputXmlSerializer : RouterOutputXmlSerializer
    {

        public override Type Type => typeof(LeitchRouterOutput);

        public override object DeserializeItem(XmlNode serializedItem, object parentItem, object[] keysOrIndices)
            => base.DeserializeItem(serializedItem, parentItem, keysOrIndices) as LeitchRouterOutput;

        public override void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] keysOrIndices)
        {
            if (item is not LeitchRouterOutput)
                return;
            base.SerializeItem(item, parentItem, xmlNode, xmlDocument, keysOrIndices);
        }

    }

}
