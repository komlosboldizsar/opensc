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
        {
            BmdAtemMvRouterOutput restoredOutput = base.DeserializeItem(serializedItem, parentItem, keysOrIndices) as BmdAtemMvRouterOutput;
            return restoredOutput;
        }

        public override XElement SerializeItem(object item, object parentItem, object[] keysOrIndices)
        {
            BmdAtemMvRouterOutput output = item as BmdAtemMvRouterOutput;
            if (output == null)
                return null;
            XElement serializedOutput = base.SerializeItem(item, parentItem, keysOrIndices);
            return serializedOutput;
        }

    }

}
