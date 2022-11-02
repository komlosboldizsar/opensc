using System;

namespace OpenSC.Model.Persistence
{
    [AttributeUsage((AttributeTargets.Field | AttributeTargets.Property), AllowMultiple = true)]
    public class PersistAsAttribute : Attribute, IDimensionedPersistAttribute
    {
        public string XPath { get; init; }
        public string[] XPathPieces { get; }
        public const string KEY_ATTRIBUTE_DEFAULT = "key";
        public string KeyAttribute { get; init; }
        public int Dimension { get; init; }
        public PersistAsAttribute(string XPath, int Dimension = 0, string KeyAttribute = KEY_ATTRIBUTE_DEFAULT)
        {
            this.XPath = XPath;
            if (XPath != null)
            {
                XPathPieces = XPath.Split('/');
                if ((Dimension != 0) && (XPathPieces.Length > 1))
                    throw new ArgumentException("XPath with sub-nodes is only allowed for the 0th dimension.", nameof(XPath));
            }
            this.Dimension = Dimension;
            this.KeyAttribute = KeyAttribute;
        }
    }
}
