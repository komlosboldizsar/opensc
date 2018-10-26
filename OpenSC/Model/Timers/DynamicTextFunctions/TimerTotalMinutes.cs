using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers.DynamicTextFunctions
{
    public class TimerTotalMinutes : IDynamicTextFunction
    {
        public string FunctionName => nameof(TimerTotalMinutes);

        public string Description => "Total elapsed/remaining minutes of a timer.";

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
                this.timer = timer;
                timer.SecondsChanged += timerSecondsChangedHandler;
                CurrentValue = (timer.Seconds % 60).ToString();
            }

            private void timerSecondsChangedHandler(Timer timer, int oldValue, int newValue)
            {
                CurrentValue = (newValue % 60).ToString();
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
