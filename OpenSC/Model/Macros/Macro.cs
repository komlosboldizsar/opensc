using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{

    public class Macro : ModelBase
    {

        public static readonly Setting<int> StackDepthSetting = new IntSetting(
            "macros.stackdepth",
            "Macros",
            "Stack depth",
            "Determines how deep the macro call stack can be when calling macros by macros or macros by triggers that were fired by macros.",
            10,
            1,
            32
        );

        public override void Restored()
        {
            base.Restored();
            foreach (MacroCommandWithArguments command in commands)
            {
                command.Restored();
            }
            foreach (MacroTriggerWithArguments trigger in triggers)
            {
                trigger.Restored();
                trigger.Macro = this;
            }
        }

        public delegate void IdChangedDelegate(Macro text, int oldValue, int newValue);
        public event IdChangedDelegate IdChanged;

        public int id = 0;

        public override int ID
        {
            get { return id; }
            set
            {
                ValidateId(value);
                if (value == id)
                    return;
                int oldValue = id;
                id = value;
                IdChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(ID));
            }
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            if (!MacroDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

        public delegate void NameChangedDelegate(Macro text, string oldName, string newName);
        public event NameChangedDelegate NameChanged;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                ValidateName(value);
                if (value == name)
                    return;
                string oldName = name;
                name = value;
                NameChanged?.Invoke(this, oldName, value);
                RaisePropertyChanged(nameof(Name));
            }
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
        }

        #region Commands
        private ObservableList<MacroCommandWithArguments> commands = new ObservableList<MacroCommandWithArguments>();

        public ObservableList<MacroCommandWithArguments> Commands
        {
            get { return commands; }
        }

        [PersistAs("commands")]
        [PersistAs("command", 1)]
        private MacroCommandWithArguments[] _commands
        {
            get { return commands.ToArray(); }
            set
            {
                commands.Clear();
                if (value != null)
                    commands.AddRange(value);
            }
        }
        #endregion

        #region Triggers
        private ObservableList<MacroTriggerWithArguments> triggers = new ObservableList<MacroTriggerWithArguments>();

        public ObservableList<MacroTriggerWithArguments> Triggers
        {
            get { return triggers; }
        }

        [PersistAs("triggers")]
        [PersistAs("trigger", 1)]
        private MacroTriggerWithArguments[] _triggers
        {
            get { return triggers.ToArray(); }
            set
            {
                triggers.Clear();
                if (value != null)
                    triggers.AddRange(value);
            }
        }

        public void AddTrigger(MacroTriggerWithArguments trigger)
        {
            if (!triggers.Contains(trigger))
                triggers.Add(trigger);
            trigger.Macro = this;
        }

        public void AddTriggerRange(IEnumerable<MacroTriggerWithArguments> triggers)
        {
            foreach (MacroTriggerWithArguments trigger in triggers)
                AddTrigger(trigger);
        }

        public void RemoveTrigger(MacroTriggerWithArguments trigger)
        {
            triggers.Remove(trigger);
            trigger.Macro = null;
        }

        public void RemoveAllTriggers()
        {
            foreach (MacroTriggerWithArguments trigger in triggers)
                trigger.Macro = null;
            triggers.Clear();
        }
        #endregion

        public void Run()
        {
            foreach (MacroCommandWithArguments commandWA in commands)
                commandWA.Run();
        }

        public void Triggered()
            => Run(); // TODO: Some log

    }

}
