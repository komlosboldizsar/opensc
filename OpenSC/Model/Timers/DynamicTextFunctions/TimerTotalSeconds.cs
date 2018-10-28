using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers.DynamicTextFunctions
{
    public class TimerTotalSeconds : IDynamicTextFunction
    {
        public string FunctionName => nameof(TimerTotalSeconds);

        public string Description => "Total elapsed/remaining seconds of a timer.";

        public int ParameterCount => 1;

        public DynamicTextFunctionArgumentType[] ArgumentTypes => new DynamicTextFunctionArgumentType[]
        {
            DynamicTextFunctionArgumentType.Integer
        };

        public string[] ArgumentDescriptions => new string[]
        {
            "ID of the timer."
        };

        public IDynamicTextFunctionSubstitute GetSubstitute(object[] arguments)
        {
            Timer timer = TimerDatabase.Instance.GetTById((int)arguments[0]);
            return new Substitute(timer);
        }

        public class Substitute: IDynamicTextFunctionSubstitute
        {

            private Timer timer;

            public Substitute(Timer timer)
            {
                if (timer == null)
                {
                    CurrentValue = "?";
                    return;
                }
                this.timer = timer;
                timer.SecondsChanged += timerSecondsChangedHandler;
                CurrentValue = timer.Seconds.ToString();
            }

            private void timerSecondsChangedHandler(Timer timer, int oldValue, int newValue)
            {
                CurrentValue = newValue.ToString();
            }

            private string currentValue;

            public string CurrentValue
            {
                get { return currentValue; }
                private set
                {
                    if (value == currentValue)
                        return;
                    currentValue = value;
                    ValueChanged?.Invoke(this);
                }
            }

            public event DynamicTextFunctionSubstituteValueChanged ValueChanged;

        }

    }
}
