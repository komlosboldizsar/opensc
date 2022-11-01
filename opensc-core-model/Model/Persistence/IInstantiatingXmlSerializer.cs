using System.Xml;

namespace OpenSC.Model.Persistence
{
    public interface IInstantiatingXmlSerializer : IXmlSerializer
    {
        object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys);
    }
}
