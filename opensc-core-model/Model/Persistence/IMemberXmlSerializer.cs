using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Persistence
{
    public interface IMemberXmlSerializer
    {
        Type Type { get; }
        XElement SerializeItem(object item, object parentItem, object[] indicesOrKeys);
        void DeserializeItem(XmlNode serializedItem, object item, object parentItem, object[] indicesOrKeys);
    }
}
