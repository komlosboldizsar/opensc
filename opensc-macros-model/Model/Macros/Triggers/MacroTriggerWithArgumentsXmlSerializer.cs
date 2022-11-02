using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Macros
{

    public class MacroTriggerWithArgumentsXmlSerializer : IInstantiatingXmlSerializer
    {

        public Type Type => typeof(MacroTriggerWithArguments);

        private const string ATTRIBUTE_CODE = "code";

        private const string TAG_NAME_ARGUMENT = "argument";
        private const string ATTRIBUTE_ARGUMENT_KEY = "key";

        public object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {
            string code = serializedItem.Attributes[ATTRIBUTE_CODE]?.Value;
            List<string> argumentKeys = new();
            foreach (XmlNode childNode in serializedItem.ChildNodes)
            {
                if (childNode.LocalName == TAG_NAME_ARGUMENT)
                {
                    string argumentKey = childNode.Attributes[ATTRIBUTE_ARGUMENT_KEY]?.Value;
                    argumentKeys.Add(argumentKey);
                }
            }
            IMacroTrigger trigger = MacroTriggerRegister.Instance.GetTrigger(code);
            return trigger?.GetWithArgumentsByKeys(argumentKeys.ToArray());
        }

        public void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] indicesOrKeys)
        {
            if (xmlNode is not XmlElement xmlElement)
                return;
            if (item is not MacroTriggerWithArguments triggerWA)
                return;
            xmlElement.SetAttribute(ATTRIBUTE_CODE, triggerWA.TriggerCode);
            foreach (string argumentStr in triggerWA.ArgumentKeys)
            {
                XmlElement argElement = xmlDocument.CreateElement(TAG_NAME_ARGUMENT);
                argElement.SetAttribute(ATTRIBUTE_ARGUMENT_KEY, argumentStr);
                xmlElement.AppendChild(argElement);
            }
        }

    }

}
