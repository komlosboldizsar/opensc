using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers.DynamicTextFunctions
{

    [DynamicTextFunction(nameof(TimerTotalMinutes), "Total elapsed/remaining minutes of a timer.")]
    public class TimerTotalMinutes : DynamicTextFunctionBase<TimerTotalMinutes.Substitute>
    {

        [DynamicTextFunctionArgument(0, "ID of the timer.")]
        public class Arg0 : DynamicTextFunctionArgumentDatabaseItem<Timer>
        {
            public Arg0() : base(TimerDatabase.Instance)
            { }
        }

        public class Substitute: DynamicTextFunctionSubstituteBase
        {

            public override void Init(object[] argumentObjects)
            {
                Timer timer = argumentObjects[0] as Timer;
                if (timer == null)
                {
                    CurrentValue = "?";
                    return;
                }
                timer.SecondsChanged += timerSecondsChangedHandler;
                CurrentValue = (timer.Seconds % 60).ToString();
            }

            private void timerSecondsChangedHandler(Timer timer, int oldValue, int newValue)
                => CurrentValue = (newValue % 60).ToString();

        }

    }

}
