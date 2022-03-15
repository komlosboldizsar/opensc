using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{
    public class MacroTriggerArgumentBool : MacroTriggerArgumentBase
    {

        public MacroTriggerArgumentBool() : base(typeof(bool), MacroArgumentKeyType.Boolean)
        {}

        public override object GetObjectByKey(string key, object[] previousArgumentObjects)
            => bool.TryParse(key, out bool objBool) && objBool;

    }

}
