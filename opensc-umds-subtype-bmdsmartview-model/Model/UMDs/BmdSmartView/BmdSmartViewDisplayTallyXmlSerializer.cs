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

        public override object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {
            BmdSmartViewDisplayTally restoredTally = base.DeserializeItem(serializedItem, parentItem, indicesOrKeys) as BmdSmartViewDisplayTally;
            if (int.TryParse(serializedItem.Attributes[ATTRIBUTE_PRIORITY]?.Value, out int priority))
                restoredTally.Priority = priority;
            return restoredTally;
        }

        public override XElement SerializeItem(object item, object parentItem, object[] indicesOrKeys)
        {
            BmdSmartViewDisplayTally tally = item as BmdSmartViewDisplayTally;
            if (tally == null)
                return null;
            XElement serializedTally = base.SerializeItem(item, parentItem, indicesOrKeys);
            serializedTally.SetAttributeValue(ATTRIBUTE_PRIORITY, tally.Priority);
            return serializedTally;
        }

    }

}
