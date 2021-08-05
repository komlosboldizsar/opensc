﻿using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Routers.Leitch
{

    class VirtualLeitchRouterOutputXmlSerializer : RouterOutputXmlSerializer
    {

        public override Type Type => typeof(VirtualLeitchRouterOutput);

        private const string ATTRIBUTE_ASSOCIATED_INPUT = "associated_input";

        public override object DeserializeItem(XmlNode serializedItem, object parentItem)
        {
            VirtualLeitchRouterOutput restoredOutput = base.DeserializeItem(serializedItem, parentItem) as VirtualLeitchRouterOutput;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_ASSOCIATED_INPUT]?.Value, out int associatedInput))
                associatedInput = 0;
            restoredOutput._associatedInputIndex = associatedInput;
            return restoredOutput;
        }

        public override XElement SerializeItem(object item, object parentItem)
        {
            VirtualLeitchRouterOutput output = item as VirtualLeitchRouterOutput;
            if (output == null)
                return null;
            XElement serializedOutput = base.SerializeItem(item, parentItem);
            serializedOutput.SetAttributeValue(ATTRIBUTE_ASSOCIATED_INPUT, output.CurrentInput?.Index ?? 0);
            return serializedOutput;
        }

    }

}
