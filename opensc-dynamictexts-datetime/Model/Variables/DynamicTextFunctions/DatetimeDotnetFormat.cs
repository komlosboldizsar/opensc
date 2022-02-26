using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace OpenSC.Model.Variables.DynamicTextFunctions
{

    [DynamicTextFunction(nameof(DatetimeDotnetFormat), "Display date and time with the help of .NET's format strings.")]
    public class DatetimeDotnetFormat : DynamicTextFunctionBase<DatetimeDotnetFormat.Substitute>
    {

        [DynamicTextFunctionArgument(0, "Format string for displaying date and time. See: https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings")]
        public class Arg0 : DynamicTextFunctionArgumentString
        {
            public Arg0() : base()
            { }
        }

        public class Substitute : DynamicTextFunctionSubstituteBase
        {

            string formatString;
            Timer timer;

            public override void Init(object[] argumentObjects)
            {

                formatString = argumentObjects[0] as string;
                if (formatString == null)
                {
                    CurrentValue = "?";
                    return;
                }

                timer = new Timer(1000);
                timer.Elapsed += timerElapsedHandler;
                timer.AutoReset = true;
                timer.Enabled = true;
                updateTime();

            }

            private void timerElapsedHandler(object sender, ElapsedEventArgs e) => updateTime();

            private void updateTime()
            {
                DateTime now = DateTime.Now;
                CurrentValue = now.ToString(formatString);
                int millisecondsToNextEvent = 1000 - now.Millisecond;
                if (millisecondsToNextEvent > 500)
                    millisecondsToNextEvent = 500;
                timer.Interval = millisecondsToNextEvent;
            }

        }

    }

}
