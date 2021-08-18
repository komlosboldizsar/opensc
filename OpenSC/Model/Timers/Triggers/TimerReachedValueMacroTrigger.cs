using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers.Triggers
{

    class TimerReachedValueMacroTrigger : MacroTriggerDefaultCallImplementations.AllArgumentsMatchStrict
    {

        public TimerReachedValueMacroTrigger()
            : base("Timers.TimerReachedValue",
                  "Timer reached time value",
                  "Compare the time value of a timer to a specified value, and fire trigger when they are equal.",
                  humanReadable)
        {
            addArgument("Timer",
                "The timer to observe.",
                typeof(Timer),
                (prev) => TimerDatabase.Instance.ToArray(),
                timer => ((Timer)timer).Name);
            addArgument("Value",
                "Time value the timers value is compared to.",
                typeof(int),
                null,
                null);
        }

        private static readonly object[] ARRAY_EMPTY = new object[] { };

        protected override string getArgumentKey(int index, object value)
        {

            if (index == 0)
            {
                Timer timer = value as Timer;
                if (timer == null)
                    return "-1";
                return timer.ID.ToString();
            }

            if (index == 1)
            {
                if (value == null)
                    return "-1";
                if (!int.TryParse(value.ToString(), out int intValue))
                    return "-1";
                return intValue.ToString();
            }

            throw new ArgumentException();

        }


        public override object[] GetArgumentsByKeys(string[] keys)
        {

            if (keys.Length != Arguments.Length)
                return null;

            if (!int.TryParse(keys[0], out int timerId))
                return null;
            if (!int.TryParse(keys[1], out int timeValue))
                return null;

            Timer timer = TimerDatabase.Instance.GetTById(timerId);
            if (timer == null)
                return null;

            return new object[]
            {
                timer,
                timeValue
            };

        }

        private const string HUMAN_READABLE_ERROR = "???";

        private static string humanReadable(object[] args)
        {
            if (args.Length < 2)
                return HUMAN_READABLE_ERROR;

            Timer timer = args[0] as Timer;
            if (timer == null)
                return HUMAN_READABLE_ERROR;

            int timeValue = (int)args[1];
            if (timeValue < 0)
                return HUMAN_READABLE_ERROR;

            return string.Format("Value of timer #{0} ({1}) reaches {2}.", timer.ID, timer.Name, timeValue);

        }

    }

}
