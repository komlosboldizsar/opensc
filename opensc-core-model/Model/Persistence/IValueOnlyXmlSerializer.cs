using System.Xml;

namespace OpenSC.Model.Persistence
{
    public interface IValueOnlyXmlSerializer : IXmlSerializer
    {
        void DeserializeItem(XmlNode serializedItem, object item, object parentItem, object[] indicesOrKeys);
    }
}
