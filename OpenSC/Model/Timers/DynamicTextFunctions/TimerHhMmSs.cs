using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers.DynamicTextFunctions
{
    public class TimerHhMmSs : IDynamicTextFunction
    {
        public string FunctionName => nameof(TimerHhMmSs);

        public string Description => "HH:MM::SS format of elapsed/remaining time of a timer.";

        public int ParameterCount => 2;

        public DynamicTextFunctionArgumentType[] ArgumentTypes => new DynamicTextFunctionArgumentType[]
        {
            DynamicTextFunctionArgumentType.Integer,
            DynamicTextFunctionArgumentType.Integer
        };

        public string[] ArgumentDescriptions => new string[]
        {
            "ID of the timer.",
            "Spaces around colons in formatted time string."
        };

        public IDynamicTextFunctionSubstitute GetSubstitute(object[] arguments)
        {
            Timer timer = TimerDatabase.Instance.GetTById((int)arguments[0]);
            return new Substitute(timer, (int)arguments[1]);
        }

        public class Substitute: DynamicTextFunctionSubstituteBase
        {

            private readonly Timer timer;
            private readonly int spacesAround;
            private readonly string colonReplacementWithSpaces;

            public Substitute(Timer timer, int spacesAround)
            {

                spacesAround = (spacesAround >= 0) ? spacesAround : 0;
                colonReplacementWithSpaces = string.Format("{0}:{0}", new string(' ', spacesAround));

                if (timer == null)
                {
                    CurrentValue = "??:??:??".Replace(":", colonReplacementWithSpaces);
                    return;
                }
                this.timer = timer;

                timer.SecondsChanged += timerSecondsChangedHandler;
                updateValue();

            }

            private void timerSecondsChangedHandler(Timer timer, int oldValue, int newValue)
            {
                updateValue();
            }

            private void updateValue()
            {
                string timeString = timer.TimeSpan.ToString(@"hh\:mm\:ss");
                CurrentValue = timeString.Replace(":", colonReplacementWithSpaces);
            }
            
        }

    }
}
