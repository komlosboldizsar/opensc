using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{

    public class MacroTriggerRegister
    {

        public static MacroTriggerRegister Instance { get; } = new MacroTriggerRegister();

        private MacroTriggerRegister()
        { }

        private Dictionary<string, IMacroTrigger> registeredTriggers = new Dictionary<string, IMacroTrigger>();

        public IReadOnlyList<IMacroTrigger> RegisteredTriggers
        {
            get => registeredTriggers.Values.ToList();
        }

        public void RegisterTrigger(IMacroTrigger trigger)
        {
            string tiggerCode = trigger.Code;
            if (registeredTriggers.ContainsKey(tiggerCode))
                throw new Exception();
            registeredTriggers[tiggerCode] = trigger;
        }

        public IMacroTrigger GetTrigger(string code)
        {
            if (!registeredTriggers.TryGetValue(code, out IMacroTrigger trigger))
                return null;
            return trigger;
        }

        public interface IMacroTriggerCollection
        {
            IMacroTrigger[] TriggersToRegister { get; }
        }

        public void RegisterTriggerCollection(IMacroTriggerCollection collection)
        {
            foreach (IMacroTrigger trigger in collection.TriggersToRegister)
                RegisterTrigger(trigger);
        }

    }

}
