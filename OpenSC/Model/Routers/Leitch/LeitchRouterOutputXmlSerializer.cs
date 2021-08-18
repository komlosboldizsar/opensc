using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Routers.Leitch
{

    class LeitchRouterOutputXmlSerializer : RouterOutputXmlSerializer
    {

        public override Type Type => typeof(LeitchRouterOutput);

        public override object DeserializeItem(XmlNode serializedItem, object parentItem)
        {
            LeitchRouterOutput restoredOutput = base.DeserializeItem(serializedItem, parentItem) as LeitchRouterOutput;
            return restoredOutput;
        }

        public override XElement SerializeItem(object item, object parentItem)
        {
            LeitchRouterOutput output = item as LeitchRouterOutput;
            if (output == null)
                return null;
            XElement serializedOutput = base.SerializeItem(item, parentItem);
            return serializedOutput;
        }

    }

}
