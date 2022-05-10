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

        public override XElement SerializeItem(object item, object parentItem, object[] keysOrIndices)
        {
            BmdTallyBoxOutput output = item as BmdTallyBoxOutput;
            if (output == null)
                return null;
            XElement serializedOutput = base.SerializeItem(item, parentItem, keysOrIndices);
            return serializedOutput;
        }

    }

}
