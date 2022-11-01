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

        public override XElement SerializeItem(object item, object parentItem, object[] keysOrIndices)
        {
            if (item is not LeitchRouterOutput)
                return null;
            return base.SerializeItem(item, parentItem, keysOrIndices);
        }

    }

}
