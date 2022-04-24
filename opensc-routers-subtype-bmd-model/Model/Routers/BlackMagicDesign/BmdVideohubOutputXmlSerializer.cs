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
        {
            BmdVideohubOutput restoredOutput = base.DeserializeItem(serializedItem, parentItem, keysOrIndices) as BmdVideohubOutput;
            return restoredOutput;
        }

        public override XElement SerializeItem(object item, object parentItem, object[] keysOrIndices)
        {
            BmdVideohubOutput output = item as BmdVideohubOutput;
            if (output == null)
                return null;
            XElement serializedOutput = base.SerializeItem(item, parentItem, keysOrIndices);
            return serializedOutput;
        }

    }

}
