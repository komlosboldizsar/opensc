using System;

namespace OpenSC.Model.Persistence
{
    [AttributeUsage((AttributeTargets.Field | AttributeTargets.Property), AllowMultiple = true)]
    public class PersistAsAttribute: Attribute
    {
        public string TagName { get; init; }
        public const string KEY_ATTRIBUTE_DEFAULT = "key";
        public string KeyAttribute { get; init; }
        public int Dimension { get; init; }
        public PersistAsAttribute(string TagName, int Dimension = 0, string KeyAttribute = KEY_ATTRIBUTE_DEFAULT)
        {
            this.TagName = TagName;
            this.Dimension = Dimension;
            this.KeyAttribute = KeyAttribute;
        }
    }
}
