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

        public override void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] keysOrIndices)
        {
            if (item is not VirtualRouterOutput)
                return;
            base.SerializeItem(item, parentItem, xmlNode, xmlDocument, keysOrIndices);
        }

    }

}
