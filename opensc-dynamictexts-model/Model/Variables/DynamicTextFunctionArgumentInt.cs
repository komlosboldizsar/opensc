using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{

    public class DynamicTextFunctionArgumentInt : DynamicTextFunctionArgumentBase
    {

        private int? min;
        private int? max;

        public DynamicTextFunctionArgumentInt(int? min = 0, int? max = null) : base(typeof(int), DynamicTextFunctionArgumentType.Integer)
        {
            this.min = min;
            this.max = max;
        }

        public override object GetObjectByKey(object key, object[] previousArgumentObjects) => (int)key;

    }

}
