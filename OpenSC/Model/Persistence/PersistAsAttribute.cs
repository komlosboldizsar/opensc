using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    class PersistAsAttribute: Attribute
    {

        public string TagName { get; private set; }

        public int Dimension { get; private set; }

        public PersistAsAttribute(string TagName, int Dimension = 0)
        {
            this.TagName = TagName;
            this.Dimension = Dimension;
        }

    }
}
