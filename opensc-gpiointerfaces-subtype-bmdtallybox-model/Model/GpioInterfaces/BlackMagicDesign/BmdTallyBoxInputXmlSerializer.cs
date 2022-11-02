using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.GpioInterfaces.BlackMagicDesign
{

    public class BmdTallyBoxInputXmlSerializer : GpioInterfaceInputXmlSerializer
    {

        public override Type Type => typeof(BmdTallyBoxInput);

        public override object DeserializeItem(XmlNode serializedItem, object parentItem, object[] keysOrIndices)
        {
            BmdTallyBoxInput restoredInput = base.DeserializeItem(serializedItem, parentItem, keysOrIndices) as BmdTallyBoxInput;
            return restoredInput;
        }

        public override void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] keysOrIndices)
        {
            if (item is not BmdTallyBoxInput)
                return;
            base.SerializeItem(item, parentItem, xmlNode, xmlDocument, keysOrIndices);
        }

    }

}
