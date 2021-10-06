using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers.DynamicTextFunctions
{

    [DynamicTextFunction(nameof(TimerHhMmSs), "HH:MM::SS format of elapsed/remaining time of a timer.")]
    public class TimerHhMmSs : DynamicTextFunctionBase<TimerHhMmSs.Substitute>
    {

        [DynamicTextFunctionArgument(0, "ID of the timer.")]
        public class Arg0 : DynamicTextFunctionArgumentDatabaseItem<Timer>
        {
            public Arg0() : base(TimerDatabase.Instance)
            { }
        }

        [DynamicTextFunctionArgument(1, "Spaces around colons in formatted time string.")]
        public class Arg1 : DynamicTextFunctionArgumentInt
        {
            public Arg1() : base(0, 7)
            { }
        }

        public class Substitute : DynamicTextFunctionSubstituteBase
        {

            private Timer timer;
            private string colonReplacementWithSpaces;

            public override void Init(object[] argumentObjects)
            {
                colonReplacementWithSpaces = string.Format("{0}:{0}", new string(' ', (int)argumentObjects[1]));
                timer = argumentObjects[0] as Timer;
                if (timer == null)
                {
                    CurrentValue = "??:??:??".Replace(":", colonReplacementWithSpaces);
                    return;
                }
                timer.SecondsChanged += timerSecondsChangedHandler;
                updateValue();
            }

            private void timerSecondsChangedHandler(Timer timer, int oldValue, int newValue) => updateValue();

            private void updateValue()
            {
                string timeString = timer.TimeSpan.ToString(@"hh\:mm\:ss");
                CurrentValue = timeString.Replace(":", colonReplacementWithSpaces);
            }

        }

    }

}
