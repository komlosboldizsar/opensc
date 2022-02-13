using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{
    public class MacroCommandArgumentInt : MacroCommandArgumentBase
    {

        private int? min;
        private int? max;

        public MacroCommandArgumentInt(int? min = 0, int? max = null) : base(typeof(int), MacroArgumentKeyType.Integer)
        {
            this.min = min;
            this.max = max;
        }

        public override object GetObjectByKey(string key, object[] previousArgumentObjects)
        {
            if (!int.TryParse(key, out int objInt) || (objInt < min) || (objInt > max))
                return min ?? 0;
            return objInt;
        }

        protected override IEnumerable<object> _getPossibilities(object[] previousArgumentObjects)
            => new object[] { min, max };

    }

}
