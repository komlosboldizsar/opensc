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
        public override XElement SerializeItem(object item, object parentItem, object[] keysOrIndices)
        {
            if (item is not BmdAtemMvRouterOutput)
                return null;
            return base.SerializeItem(item, parentItem, keysOrIndices);
        }

    }

}
