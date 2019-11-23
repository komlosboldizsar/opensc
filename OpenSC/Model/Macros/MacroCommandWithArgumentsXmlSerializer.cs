using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Macros
{

    class MacroCommandWithArgumentsXmlSerializer : IValueXmlSerializer
    {

        public Type Type => typeof(MacroCommandWithArguments);

        private const string TAG_NAME = "macro_command";
        private const string ATTRIBUTE_CODE = "code";

        private const string TAG_NAME_ARGUMENT = "argument";
        private const string ATTRIBUTE_ARGUMENT_KEY = "key";
        public object DeserializeItem(XmlNode serializedItem)
        {
            if (serializedItem.LocalName != TAG_NAME)
                return null;

            string commandCode = serializedItem.Attributes[ATTRIBUTE_CODE]?.Value;

            List<string> argumentKeys = new List<string>();
            foreach (XmlNode childNode in serializedItem.ChildNodes)
            {
                if (childNode.LocalName == TAG_NAME_ARGUMENT)
                {
                    string argumentKey = childNode.Attributes[ATTRIBUTE_ARGUMENT_KEY]?.Value;
                    argumentKeys.Add(argumentKey);
                }
            }

            IMacroCommand command = MacroCommandRegister.Instance.GetCommand(commandCode);
            if (command == null)
                return null;

            return command.GetWithArguments(argumentKeys.ToArray());

        }

        public XElement SerializeItem(object item)
        {

            MacroCommandWithArguments commandWA = item as MacroCommandWithArguments;
            if (commandWA == null)
                return null;

            XElement xmlElement = new XElement(TAG_NAME);
            xmlElement.SetAttributeValue(ATTRIBUTE_CODE, commandWA.CommandCode);

            foreach (string argumentStr in commandWA.ArgumentKeys)
            {
                XElement argElement = new XElement(TAG_NAME_ARGUMENT);
                argElement.SetAttributeValue(ATTRIBUTE_ARGUMENT_KEY, argumentStr);
                xmlElement.Add(argElement);
            }

            return xmlElement;

        }

    }

}
