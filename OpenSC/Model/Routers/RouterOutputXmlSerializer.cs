﻿using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Routers
{

    class RouterOutputXmlSerializer : IValueXmlSerializer
    {

        public Type Type => typeof(RouterOutput);

        private const string TAG_NAME = "output";
        private const string ATTRIBUTE_INDEX = "index";
        private const string ATTRIBUTE_NAME = "name";

        public object DeserializeItem(XmlNode serializedItem)
        {
            if (serializedItem.LocalName != TAG_NAME)
                return null;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_INDEX]?.Value, out int index))
                index = 0;
            return new RouterOutput()
            {
                Index = index,
                Name = serializedItem.Attributes[ATTRIBUTE_NAME]?.Value,
            };
        }

        public XElement SerializeItem(object item)
        {

            RouterOutput output = item as RouterOutput;
            if (output == null)
                return null;

            XElement xmlElement = new XElement(TAG_NAME);
            xmlElement.SetAttributeValue(ATTRIBUTE_INDEX, output.Index);
            xmlElement.SetAttributeValue(ATTRIBUTE_NAME, output.Name);
            return xmlElement;

        }

    }

}
