using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    class PersistSubclassAttribute : Attribute
    {

        public Type SubclassType { get; private set; }

        public PersistSubclassAttribute(Type SubclassType)
        {
            this.SubclassType = SubclassType;
        }

    }
}
