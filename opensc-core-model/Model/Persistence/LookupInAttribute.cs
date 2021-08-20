using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    class LookupInAttribute: Attribute
    {
        public string DatabaseName { get; private set; }

        public LookupInAttribute(string DatabaseName)
        {
            this.DatabaseName = DatabaseName;
        }

    }
}
