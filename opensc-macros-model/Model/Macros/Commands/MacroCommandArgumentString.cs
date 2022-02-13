using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{
    public class MacroCommandArgumentString : MacroCommandArgumentBase
    {
        public MacroCommandArgumentString() : base(typeof(string), MacroArgumentKeyType.String)
        { }
        public override object GetObjectByKey(string key, object[] previousArgumentObjects) => key;
    }
}
