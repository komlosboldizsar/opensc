using OpenSC.Model.Persistence;
using System;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.UMDs.BmdSmartView
{

    public class BmdSmartViewDisplayTallyXmlSerializer : UmdTallyXmlSerializer
    {

        public override Type Type => typeof(BmdSmartViewDisplayTally);

        private const string ATTRIBUTE_PRIORITY = "priority";

        public override void DeserializeItem(XmlNode serializedItem, object item, object parentItem, object[] indicesOrKeys)
        {
            base.DeserializeItem(serializedItem, item, parentItem, indicesOrKeys);
            if (item is not BmdSmartViewDisplayTally castedTally)
                return;
            if (int.TryParse(serializedItem.Attributes[ATTRIBUTE_PRIORITY]?.Value, out int priority))
                castedTally.Priority = priority;
        }

        public override XElement SerializeItem(object item, object parentItem, object[] indicesOrKeys)
        {
            if (item is not BmdSmartViewDisplayTally tally)
                return null;
            XElement serializedTally = base.SerializeItem(item, parentItem, indicesOrKeys);
            serializedTally.SetAttributeValue(ATTRIBUTE_PRIORITY, tally.Priority);
            return serializedTally;
        }

    }

}
