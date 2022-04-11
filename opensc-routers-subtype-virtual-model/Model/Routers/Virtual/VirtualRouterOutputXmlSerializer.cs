using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Routers.Virtual
{

    public class VirtualRouterOutputXmlSerializer : RouterOutputXmlSerializer
    {

        public override Type Type => typeof(VirtualRouterOutput);

        public override object DeserializeItem(XmlNode serializedItem, object parentItem, object[] keysOrIndices)
        {
            VirtualRouterOutput restoredOutput = base.DeserializeItem(serializedItem, parentItem, keysOrIndices) as VirtualRouterOutput;
            return restoredOutput;
        }

        public override XElement SerializeItem(object item, object parentItem, object[] keysOrIndices)
        {
            VirtualRouterOutput output = item as VirtualRouterOutput;
            if (output == null)
                return null;
            XElement serializedOutput = base.SerializeItem(item, parentItem, keysOrIndices);
            return serializedOutput;
        }

    }

}
