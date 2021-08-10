using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Routers.Virtual
{

    class VirtualRouterOutputXmlSerializer : RouterOutputXmlSerializer
    {

        public override Type Type => typeof(VirtualRouterOutput);

        public override object DeserializeItem(XmlNode serializedItem, object parentItem)
        {
            VirtualRouterOutput restoredOutput = base.DeserializeItem(serializedItem, parentItem) as VirtualRouterOutput;
            return restoredOutput;
        }

        public override XElement SerializeItem(object item, object parentItem)
        {
            VirtualRouterOutput output = item as VirtualRouterOutput;
            if (output == null)
                return null;
            XElement serializedOutput = base.SerializeItem(item, parentItem);
            return serializedOutput;
        }

    }

}
