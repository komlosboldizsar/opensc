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
        {
            LeitchRouterOutput restoredOutput = base.DeserializeItem(serializedItem, parentItem, keysOrIndices) as LeitchRouterOutput;
            return restoredOutput;
        }

        public override XElement SerializeItem(object item, object parentItem, object[] keysOrIndices)
        {
            LeitchRouterOutput output = item as LeitchRouterOutput;
            if (output == null)
                return null;
            XElement serializedOutput = base.SerializeItem(item, parentItem, keysOrIndices);
            return serializedOutput;
        }

    }

}
