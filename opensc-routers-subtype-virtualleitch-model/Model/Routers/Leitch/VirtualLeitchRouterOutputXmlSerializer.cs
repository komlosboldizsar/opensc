using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

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

        public override XElement SerializeItem(object item, object parentItem, object[] keysOrIndices)
        {
            VirtualLeitchRouterOutput output = item as VirtualLeitchRouterOutput;
            if (output == null)
                return null;
            XElement serializedOutput = base.SerializeItem(item, parentItem, keysOrIndices);
            serializedOutput.SetAttributeValue(ATTRIBUTE_ASSOCIATED_INPUT, output.CurrentInput?.Index ?? 0);
            serializedOutput.SetAttributeValue(ATTRIBUTE_LOCK_STATUS, output.LockStatusCode);
            serializedOutput.SetAttributeValue(ATTRIBUTE_LOCK_OWNER_PANEL_ID, output.LockProtectOwnerPanelId);
            return serializedOutput;
        }

    }

}
