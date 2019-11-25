using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{

    public class MacroTriggerDefaultCallImplementations
    {

        public abstract class SimpleArgumentMatch : MacroTriggerBase
        {
            public SimpleArgumentMatch(string code, string name, string description)
                : base(code, name, description)
            { }

            public override void Call(params object[] arguments)
            {
                foreach (MacroTriggerWithArguments triggerWA in registeredTriggersWithArguments)
                    if (argumentsPredicate(arguments, triggerWA.ArgumentValues))
                        triggerWA.Fire(this);
            }

            protected abstract bool argumentsPredicate(object[] callArguments, object[] instanceArguments);

        }

        public abstract class AllArgumentsMatchStrict : SimpleArgumentMatch
        {
            public AllArgumentsMatchStrict(string code, string name, string description)
                : base(code, name, description)
            { }

            protected override bool argumentsPredicate(object[] callArguments, object[] instanceArguments)
            {
                if (callArguments.Length != instanceArguments.Length)
                    return false;
                for (int i = 0; i < instanceArguments.Length; i++)
                    if (callArguments[i] != instanceArguments[i])
                        return false;
                return true;
            }

        }

    }

}
