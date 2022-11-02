using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Routers.BlackMagicDesign
{

    public class BmdVideohubOutputXmlSerializer : RouterOutputXmlSerializer
    {

        public override Type Type => typeof(BmdVideohubOutput);

        public override object DeserializeItem(XmlNode serializedItem, object parentItem, object[] keysOrIndices)
            => base.DeserializeItem(serializedItem, parentItem, keysOrIndices) as BmdVideohubOutput;

        public override void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] keysOrIndices)
        {
            if (item is not BmdVideohubOutput)
                return;
            base.SerializeItem(item, parentItem, xmlNode, xmlDocument, keysOrIndices);
        }

    }

}
