using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Persistence
{
    public interface IValueXmlSerializer
    {
        Type Type { get; }
        XElement SerializeItem(object item, object parentItem);
        object DeserializeItem(XmlNode serializedItem, object parentItem);
    }
}
