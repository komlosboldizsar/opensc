using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class PolymorphFieldAttribute: Attribute
    {
        public delegate Dictionary<Type, string> TypeStringDictionaryGetterDelegate();
        public string TypeStringDictionaryGetterName { get; private set; }
        public string TypeAttributeName { get; private set; }
        public PolymorphFieldAttribute(string TypeStringDictionaryGetterName, string TypeAttributeName = "type")
        {
            this.TypeStringDictionaryGetterName = TypeStringDictionaryGetterName;
            this.TypeAttributeName = TypeAttributeName;
        }
    }
}
