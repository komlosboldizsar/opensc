using OpenSC.Model.Persistence;
using OpenSC.Model.Signals;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Routers
{

    class RouterInputXmlSerializer : IValueXmlSerializer
    {

        public Type Type => typeof(RouterInput);

        private const string TAG_NAME = "router_input";
        private const string ATTRIBUTE_NAME = "name";
        private const string ATTRIBUTE_SOURCE = "source";

        public object DeserializeItem(XmlNode serializedItem)
        {
            if (serializedItem.LocalName != TAG_NAME)
                return null;
            return new RouterInput()
            {
                Name = serializedItem.Attributes[ATTRIBUTE_NAME]?.Value,
                _sourceString = serializedItem.Attributes[ATTRIBUTE_SOURCE]?.Value
            };
        }

        public XElement SerializeItem(object item)
        {

            RouterInput input = item as RouterInput;
            if (input == null)
                return null;

            XElement xmlElement = new XElement(TAG_NAME);
            xmlElement.SetAttributeValue(ATTRIBUTE_NAME, input.Name);

            string sourceStr = "";
            if (input.Source is InputSourceSignal)
            {
                Signal signal = ((InputSourceSignal)input.Source).Signal;
                sourceStr = string.Format(SOURCE_CODE_FORMAT_SIGNAL, signal.ID);
            }
            else if (input.Source is RouterOutput)
            {
                RouterOutput output = (RouterOutput)input.Source;
                sourceStr = string.Format(SOURCE_CODE_FORMAT_OUTPUT, output.Router.ID, output.Index);
            }
            xmlElement.SetAttributeValue(ATTRIBUTE_SOURCE, sourceStr);

            return xmlElement;

        }

        public const string SOURCE_CODE_FORMAT_SIGNAL = "signal,{0}";
        public const string SOURCE_CODE_FORMAT_OUTPUT = "output,{0},{1}";

        internal static IRouterInputSource GetSourceByString(string sourceString)
        {

            string[] tokens = sourceString.Split(',');

            if ((tokens.Length == 2) && (tokens[0] == "signal"))
            {
                if (!int.TryParse(tokens[1], out int signalId))
                    return null;
                return new InputSourceSignal(SignalDatabases.Signals.GetTById(signalId));
            }

            if ((tokens.Length == 3) && (tokens[0] == "output"))
            {
                if (!int.TryParse(tokens[1], out int routerId))
                    return null;
                Router router = RouterDatabase.Instance.GetTById(routerId);
                if (router == null)
                    return null;
                if (!int.TryParse(tokens[2], out int outputIndex))
                    return null;
                if (router.Outputs.Count > outputIndex)
                    return router.Outputs[outputIndex];
                return null;
            }

            return null;
                
        }

    }

}
