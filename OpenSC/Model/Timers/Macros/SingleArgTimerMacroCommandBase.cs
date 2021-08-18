using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers.Macros
{

    public abstract class SingleArgTimerMacroCommandBase : MacroCommandBase
    {

        public SingleArgTimerMacroCommandBase(string arg0description)
        {
            arguments = new IMacroCommandArgument[] { new Arg0(arg0description) };
        }

        private IMacroCommandArgument[] arguments;

        public override IMacroCommandArgument[] Arguments => arguments;

        public override void Run(object[] argumentValues)
        {

            if (argumentValues.Length != Arguments.Length)
                return;

            Timer timer = argumentValues[0] as Timer;
            if (timer == null)
                return;

            _run(timer);

        }

        protected abstract void _run(Timer timer);

        protected override string getArgumentKey(int index, object value)
        {

            if (index == 0) {
                Timer timer = value as Timer;
                if (timer == null)
                    return "-1";
                return timer.ID.ToString();
            }

            throw new ArgumentException();

        }

        public override object[] GetArgumentsByKeys(string[] keys)
        {

            if (keys.Length != Arguments.Length)
                return null;

            if (!int.TryParse(keys[0], out int timerId))
                return null;

            Timer timer = TimerDatabase.Instance.GetTById(timerId);
            if (timer == null)
                return null;

            return new object[] { timer };

        }

        private static readonly object[] ARRAY_EMPTY = new object[] { };

        public class Arg0 : IMacroCommandArgument
        {
            public string Name => "Timer";
            public string Description => description;
            private string description;
            public Type Type => typeof(Timer);
            public MacroArgumentKeyType KeyType => MacroArgumentKeyType.Integer;
            public object[] GetPossibilities(object[] previousArgumentValues)
                => TimerDatabase.Instance.ToArray();
            public string GetStringForPossibility(object item)
                => ((Timer)item).Name;

            public Arg0(string description)
            {
                this.description = description;
            }

        }

    }

}
