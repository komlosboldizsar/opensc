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

        public override void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] indicesOrKeys)
        {
            if (item is not BmdSmartViewDisplayTally bmdSmartViewDisplayTally)
                return;
            base.SerializeItem(item, parentItem, xmlNode, xmlDocument, indicesOrKeys);
            if (xmlNode is not XmlElement xmlElement)
                return;
            xmlElement.SetAttribute(ATTRIBUTE_PRIORITY, bmdSmartViewDisplayTally.Priority.ToString());
        }

    }

}
