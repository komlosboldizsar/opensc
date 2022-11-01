using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class PersistSubclassAttribute : Attribute, IDimensionedPersistAttribute
    {
        public delegate Type SubclassTypeGetterDelegate();
        public string SubclassTypeGetterName { get; init; }
        public int Dimension { get; init; }
        public PersistSubclassAttribute(string SubclassTypeGetterName, int Dimension = 0)
        {
            this.SubclassTypeGetterName = SubclassTypeGetterName;
            this.Dimension = Dimension;
        }
    }
}
