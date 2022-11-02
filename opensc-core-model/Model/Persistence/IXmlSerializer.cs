using System;
using System.Xml;

namespace OpenSC.Model.Persistence
{
    public interface IXmlSerializer
    {
        Type Type { get; }
        void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] indicesOrKeys);
    }
}
