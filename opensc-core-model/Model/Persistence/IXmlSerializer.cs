using System;
using System.Xml.Linq;

namespace OpenSC.Model.Persistence
{
    public interface IXmlSerializer
    {
        Type Type { get; }
        XElement SerializeItem(object item, object parentItem, object[] indicesOrKeys);
    }
}
