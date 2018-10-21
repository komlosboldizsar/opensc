using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{

    [AttributeUsage(AttributeTargets.Class)]
    class TypeCodeAttribute : Attribute
    {

        public string Code { get; private set; }

        public TypeCodeAttribute(string Code)
        {
            this.Code = Code;
        }

    }

}
