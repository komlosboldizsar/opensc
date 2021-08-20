using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{

    public class DynamicTextFunctionArgumentString : DynamicTextFunctionArgumentBase
    {

        public DynamicTextFunctionArgumentString(int? min = 0, int? max = null) : base(typeof(string), DynamicTextFunctionArgumentType.String)
        { }

        public override object GetObjectByKey(object key, object[] previousArgumentObjects) => (string)key;

    }

}
