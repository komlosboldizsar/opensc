using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Persistence
{
    public interface ICompleteXmlSerializer
    {
        Type Type { get; }
        XElement SerializeItem(object item, object parentItem, object[] indicesOrKeys);
        object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys);
    }
}
