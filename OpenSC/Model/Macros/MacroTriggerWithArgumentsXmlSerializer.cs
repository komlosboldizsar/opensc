using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Macros
{

    class MacroTriggerWithArgumentsXmlSerializer : IValueXmlSerializer
    {

        public Type Type => typeof(MacroTriggerWithArguments);

        private const string TAG_NAME = "macro_trigger";
        private const string ATTRIBUTE_CODE = "code";

        private const string TAG_NAME_ARGUMENT = "argument";
        private const string ATTRIBUTE_ARGUMENT_KEY = "key";
        public object DeserializeItem(XmlNode serializedItem, object parentItem)
        {
            if (serializedItem.LocalName != TAG_NAME)
                return null;

            string code = serializedItem.Attributes[ATTRIBUTE_CODE]?.Value;

            List<string> argumentKeys = new List<string>();
            foreach (XmlNode childNode in serializedItem.ChildNodes)
            {
                if (childNode.LocalName == TAG_NAME_ARGUMENT)
                {
                    string argumentKey = childNode.Attributes[ATTRIBUTE_ARGUMENT_KEY]?.Value;
                    argumentKeys.Add(argumentKey);
                }
            }

            IMacroTrigger trigger = MacroTriggerRegister.Instance.GetTrigger(code);
            if (trigger == null)
                return null;

            return trigger.GetWithArgumentsByKeys(argumentKeys.ToArray());

        }

        public XElement SerializeItem(object item, object parentItem)
        {

            MacroTriggerWithArguments triggerWA = item as MacroTriggerWithArguments;
            if (triggerWA == null)
                return null;

            XElement xmlElement = new XElement(TAG_NAME);
            xmlElement.SetAttributeValue(ATTRIBUTE_CODE, triggerWA.TriggerCode);

            foreach (string argumentStr in triggerWA.ArgumentKeys)
            {
                XElement argElement = new XElement(TAG_NAME_ARGUMENT);
                argElement.SetAttributeValue(ATTRIBUTE_ARGUMENT_KEY, argumentStr);
                xmlElement.Add(argElement);
            }

            return xmlElement;

        }

    }

}
