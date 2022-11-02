using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.GpioInterfaces.BlackMagicDesign
{

    public class BmdTallyBoxOutputXmlSerializer : GpioInterfaceOutputXmlSerializer
    {

        public override Type Type => typeof(BmdTallyBoxOutput);

        public override object DeserializeItem(XmlNode serializedItem, object parentItem, object[] keysOrIndices)
        {
            BmdTallyBoxOutput restoredOutput = base.DeserializeItem(serializedItem, parentItem, keysOrIndices) as BmdTallyBoxOutput;
            return restoredOutput;
        }

        public override void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] keysOrIndices)
        {
            if (item is not BmdTallyBoxOutput)
                return;
            base.SerializeItem(item, parentItem, xmlNode, xmlDocument, keysOrIndices);
        }

    }

}
