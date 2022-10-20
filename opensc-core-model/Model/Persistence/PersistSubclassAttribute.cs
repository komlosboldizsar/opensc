using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class PersistSubclassAttribute : Attribute
    {
        public delegate Type SubclassTypeGetterDelegate();
        public string SubclassTypeGetterName { get; private set; }
        public PersistSubclassAttribute(string SubclassTypeGetterName)
        {
            this.SubclassTypeGetterName = SubclassTypeGetterName;
        }
    }
}
