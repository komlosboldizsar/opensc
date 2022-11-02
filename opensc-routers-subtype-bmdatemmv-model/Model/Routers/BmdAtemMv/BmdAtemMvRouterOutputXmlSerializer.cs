using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Routers.BmdAtemMv
{

    public class BmdAtemMvRouterOutputXmlSerializer : RouterOutputXmlSerializer
    {

        public override Type Type => typeof(BmdAtemMvRouterOutput);

        public override object DeserializeItem(XmlNode serializedItem, object parentItem, object[] keysOrIndices)
            => base.DeserializeItem(serializedItem, parentItem, keysOrIndices) as BmdAtemMvRouterOutput;

        public override void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] keysOrIndices)
        {
            if (item is not BmdAtemMvRouterOutput)
                return;
            base.SerializeItem(item, parentItem, xmlNode, xmlDocument, keysOrIndices);
        }

    }

}
