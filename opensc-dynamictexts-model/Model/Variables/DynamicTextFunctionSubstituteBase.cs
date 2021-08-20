using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{
    public abstract class DynamicTextFunctionSubstituteBase: IDynamicTextFunctionSubstitute
    {

        private string currentValue = "?";

        public string CurrentValue
        {
            get { return currentValue; }
            protected set
            {
                if (value == currentValue)
                    return;
                currentValue = value;
                ValueChanged?.Invoke(this);
            }
        }

        public event DynamicTextFunctionSubstituteValueChanged ValueChanged;

        public abstract void Init(object[] argumentObjects);

    }
}
