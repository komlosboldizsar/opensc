using OpenSC.Logger;
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

        public static readonly Setting<int> MaxStackDepthSetting = new IntSetting(
            "macros.stackdepth",
            "Macros",
            "Stack depth",
            "Determines how deep the macro call stack can be when calling macros by macros or macros by triggers that were fired by macros.",
            10,
            1,
            32
        );

        private const string LOG_TAG = "Macro";

        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            for (int i = commands.Count - 1; i >= 0; i--)
                if (commands[i] == null)
                    commands.RemoveAt(i);
            for (int i = triggers.Count - 1; i >= 0; i--)
                if (triggers[i] == null)
                    triggers.RemoveAt(i);
            foreach (MacroTriggerWithArguments trigger in triggers)
                trigger.Macro = this;
        }

        public override void RestoreCustomRelations()
        {
            base.RestoreCustomRelations();
            foreach (MacroCommandWithArguments command in commands)
                command.RestoreCustomRelations();
            foreach (MacroTriggerWithArguments trigger in triggers)
                trigger.RestoreCustomRelations();
        }

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            foreach (MacroTriggerWithArguments trigger in triggers)
                trigger.TotallyRestored();
        }

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = MacroDatabase.Instance;
        #endregion

        #region Commands
        private ObservableList<MacroCommandWithArguments> commands = new ObservableList<MacroCommandWithArguments>();
        public ObservableList<MacroCommandWithArguments> Commands => commands;

        [PersistAs("commands")]
        [PersistAs(null, 1)]
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
        public ObservableList<MacroTriggerWithArguments> Triggers => triggers;

        [PersistAs("triggers")]
        [PersistAs(null, 1)]
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

        public int CurrentStackDepth { get; private set; } = 0;

        public void Run()
        {
            string logMessage = string.Format("Macro #{0} ({1}) is executed externally or from another macro.", ID, Name);
            LogDispatcher.I(LOG_TAG, logMessage);
            _run();
        }

        public void _run()
        {
            if (CurrentStackDepth >= MaxStackDepthSetting.Value)
            {
                string logMessage = string.Format("Can't execute #{0} ({1}) macro, because stack is full ({2}).", ID, Name, CurrentStackDepth);
                LogDispatcher.W(LOG_TAG, logMessage);
                return;
            }

            CurrentStackDepth++;
            foreach (MacroCommandWithArguments commandWA in commands)
                commandWA.Run();
            CurrentStackDepth--;
        }

        public void Triggered(MacroTriggerWithArguments source)
        {
            string logMessage = string.Format("Macro #{0} ({1}) triggered. Event: {2}.", ID, Name, source.HumanReadable);
            LogDispatcher.I(LOG_TAG, logMessage);
            _run();
        }

    }

}
