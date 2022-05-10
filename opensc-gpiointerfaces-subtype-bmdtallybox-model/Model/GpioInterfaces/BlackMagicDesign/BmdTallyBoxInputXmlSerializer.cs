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

        public override XElement SerializeItem(object item, object parentItem, object[] keysOrIndices)
        {
            BmdTallyBoxInput input = item as BmdTallyBoxInput;
            if (input == null)
                return null;
            XElement serializedInput = base.SerializeItem(item, parentItem, keysOrIndices);
            return serializedInput;
        }

    }

}
