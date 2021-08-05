using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    class PolymorphFieldAttribute: Attribute
    {

        public delegate Dictionary<Type, string> TypeStringDictionaryGetterDelegate();

        public TypeStringDictionaryGetterDelegate TypeStringDictionaryGetter { get; private set; }

        public string TypeAttributeName { get; private set; }

        public PolymorphFieldAttribute(TypeStringDictionaryGetterDelegate StringTypeDictionaryGetter, string TypeAttributeName = "type")
        {
            this.TypeStringDictionaryGetter = StringTypeDictionaryGetter;
            this.TypeAttributeName = TypeAttributeName;
        }

    }
}
