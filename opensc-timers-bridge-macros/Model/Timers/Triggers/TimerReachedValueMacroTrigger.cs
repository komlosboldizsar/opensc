using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers.Triggers
{

    [MacroTrigger("Timers.TimerReachedValue", "Timer reached time value", "Compare the time value of a timer to a specified value, and fire trigger when they are equal.")]
    class TimerReachedValueMacroTrigger : MacroTriggerBase<TimerReachedValueMacroTrigger.ActivationData>
    {

        [MacroTriggerArgument(0, "Timer", "The timer to observe.")]
        public class Arg0 : MacroTriggerArgumentDatabaseItem<Timer>
        {
            public Arg0() : base (TimerDatabase.Instance)
            { }
        }

        [MacroTriggerArgument(1, "Value", "Time value the timers value is compared to.")]
        public class Arg1 : MacroTriggerArgumentInt
        {
            public Arg1() : base()
            { }
        }

        internal class ActivationData : MacroTriggerWithArgumentsActivationData
        {
            public Timer Timer { get; private set; }
            public PropertyChangedTwoValuesDelegate<Timer, int> TimerSecondsChangedHandler { get; private set; }
            public ActivationData(Timer timer, PropertyChangedTwoValuesDelegate<Timer, int> timerSecondsChangedHandler)
            {
                Timer = timer;
                TimerSecondsChangedHandler = timerSecondsChangedHandler;
            }
        }

        protected override void _activate(MacroTriggerWithArguments triggerWithArguments)
        {
            object[] argumentObjects = triggerWithArguments.ArgumentObjects;
            Timer timer = argumentObjects[0] as Timer;
            if (timer == null)
                return;
            int timeValue = (int)argumentObjects[1];
            if (timeValue < 0)
                return;
            PropertyChangedTwoValuesDelegate<Timer, int> timerSecondsChangedHandler = (i, ov, nv) => {
                if (nv == timeValue)
                    triggerWithArguments.Fire();
            };
            timer.SecondsChanged += timerSecondsChangedHandler;
            ActivationData activationData = new ActivationData(timer, timerSecondsChangedHandler);
            triggerWithArguments.Activated(activationData);
        }

        protected override void _deactivate(MacroTriggerWithArguments triggerWithArguments, ActivationData activationData)
        {
            activationData.Timer.SecondsChanged -= activationData.TimerSecondsChangedHandler;
            triggerWithArguments.Deactivated();
        }

        protected override string _humanReadable(object[] argumentObjects)
        {
            Timer timer = argumentObjects[0] as Timer;
            if (timer == null)
                return null;
            int timeValue = (int)argumentObjects[1];
            if (timeValue < 0)
                return null;
            return string.Format("Value of timer [{0}] reaches {1}.", timer.ToString(), timeValue);
        }

    }

}
