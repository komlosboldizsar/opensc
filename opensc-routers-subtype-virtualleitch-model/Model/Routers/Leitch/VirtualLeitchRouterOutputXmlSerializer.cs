using OpenSC.Model.Persistence;
using System;
using System.Xml;

namespace OpenSC.Model.Routers.Leitch
{

    public class VirtualLeitchRouterOutputXmlSerializer : RouterOutputXmlSerializer
    {

        public override Type Type => typeof(VirtualLeitchRouterOutput);

        private const string ATTRIBUTE_ASSOCIATED_INPUT = "associated_input";
        private const string ATTRIBUTE_LOCK_STATUS = "lock_status";
        private const string ATTRIBUTE_LOCK_OWNER_PANEL_ID = "lock_owner_panel_id";

        public override object DeserializeItem(XmlNode serializedItem, object parentItem, object[] keysOrIndices)
        {
            VirtualLeitchRouterOutput restoredOutput = base.DeserializeItem(serializedItem, parentItem, keysOrIndices) as VirtualLeitchRouterOutput;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_ASSOCIATED_INPUT]?.Value, out int associatedInput))
                associatedInput = 0;
            restoredOutput._associatedInputIndex = associatedInput;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_LOCK_STATUS]?.Value, out int lockStatus))
                lockStatus = 0;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_LOCK_OWNER_PANEL_ID]?.Value, out int lockOwnerPanelId))
                lockOwnerPanelId = -1;
            restoredOutput.SetLock(lockStatus, lockOwnerPanelId);
            return restoredOutput;
        }

        public override void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] keysOrIndices)
        {
            if (item is not VirtualLeitchRouterOutput virtualLeitchOutput)
                return;
            base.SerializeItem(item, parentItem, xmlNode, xmlDocument, keysOrIndices);
            if (xmlNode is not XmlElement xmlElement)
                return;
            xmlElement.SetAttribute(ATTRIBUTE_ASSOCIATED_INPUT, (virtualLeitchOutput.CurrentInput != null) ? virtualLeitchOutput.CurrentInput.Index.ToString() : 0.ToString());
            xmlElement.SetAttribute(ATTRIBUTE_LOCK_STATUS, virtualLeitchOutput.LockStatusCode.ToString());
            xmlElement.SetAttribute(ATTRIBUTE_LOCK_OWNER_PANEL_ID, virtualLeitchOutput.LockProtectOwnerPanelId.ToString());
        }

    }

}
