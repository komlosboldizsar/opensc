using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{

    [AttributeUsage(AttributeTargets.Class)]
    class TypeLabelAttribute: Attribute
    {

        public string Label { get; private set; }

        public TypeLabelAttribute(string Label)
        {
            this.Label = Label;
        }

    }

}
