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
            => base.DeserializeItem(serializedItem, parentItem, keysOrIndices) as VirtualRouterOutput;

        public override XElement SerializeItem(object item, object parentItem, object[] keysOrIndices)
        {
            if (item is not VirtualRouterOutput)
                return null;
            return base.SerializeItem(item, parentItem, keysOrIndices);
        }

    }

}
