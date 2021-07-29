using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.Routers.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{
    
    public abstract class Router : ModelBase
    {

        public const string LOG_TAG = "Router";

        #region Persistence, instantiation
        public Router()
        {
            inputs.ItemsChanged += inputsChangedHandler;
            outputs.ItemsChanged += outputsChangedHandler;
        }

        public override void Removed()
        {

            base.Removed();

            IdChanged = null;
            NameChanged = null;
            InputsChanged = null;
            OutputsChanged = null;
            StateChanged = null;
            StateStringChanged = null;

            Inputs.ForEach(i => i.RemovedFromRouter(this));
            inputs.Clear();

            Outputs.ForEach(o => o.RemovedFromRouter(this));
            outputs.Clear();

        }

        protected override void afterUpdate()
        {
            base.afterUpdate();
            RouterDatabase.Instance.ItemUpdated(this);
        }
        #endregion

        #region Restoration
        public override void Restored()
        {
            notifyIOsRestored();
            queryAllCrosspoints();
        }

        private void notifyIOsRestored()
        {
            inputs.ForEach(i => i.Restored());
            outputs.ForEach(o => o.Restored());
        }
        #endregion

        #region Property: ID
        public delegate void IdChangedDelegate(Router router, int oldValue, int newValue);
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
            if (!RouterDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Name
        public delegate void NameChangedDelegate(Router router, string oldName, string newName);
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
        #endregion

        #region Inputs
        private ObservableList<RouterInput> inputs = new ObservableList<RouterInput>();
        public ObservableList<RouterInput> Inputs => inputs;

        [PersistAs("inputs")]
        [PersistAs(null, 1)]
        private RouterInput[] _inputs // for persistence
        {
            get { return inputs.ToArray(); }
            set
            {
                inputs.Clear();
                if(value != null)
                    inputs.AddRange(value);
                inputs.ForEach(i => i.AssignParentRouter(this));
                updateInputIndices();
            }
        }

        public void AddInput()
        {
            int index = inputs.Count;
            inputs.Add(new RouterInput(string.Format("Input #{0}", index + 1), this, index));
        }

        public void RemoveInput(RouterInput input)
        {
            inputs.Remove(input);
            input.RemovedFromRouter(this);
            updateInputIndices();
        }

        public RouterInput GetInput(int index)
        {
            if ((index < 1) || (index > inputs.Count))
                throw new ArgumentException();
            return inputs[index - 1];
        }

        private void updateInputIndices()
        {
            int idx = 1;
            foreach (RouterInput input in inputs)
                input.SetIndexFromRouter(this, idx++);
        }

        private void inputsChangedHandler()
        {
            InputsChanged?.Invoke(this);
            RaisePropertyChanged(nameof(Inputs));
        }

        public delegate void InputsChangedDelegate(Router router);
        public event InputsChangedDelegate InputsChanged;

        public void RestoreInputSources() => inputs.ForEach(i => i.RestoreSource());
        #endregion

        #region Outputs
        private ObservableList<RouterOutput> outputs = new ObservableList<RouterOutput>();
        public ObservableList<RouterOutput> Outputs => outputs;

        [PersistAs("outputs")]
        [PersistAs(null, 1)]
        private RouterOutput[] _outputs // for persistence
        {
            get { return outputs.ToArray(); }
            set
            {
                outputs.Clear();
                if(value != null)
                    outputs.AddRange(value);
                outputs.ForEach(o => o.AssignParentRouter(this));
                updateOutputIndices();
            }
        }

        public void AddOutput()
        {
            int index = outputs.Count;
            outputs.Add(new RouterOutput(string.Format("Output #{0}", index + 1), this, index));
        }

        public void RemoveOutput(RouterOutput output)
        {
            outputs.Remove(output);
            output.RemovedFromRouter(this);
            updateOutputIndices();
        }

        public RouterOutput GetOutput(int index)
        {
            if ((index < 1) || (index > outputs.Count))
                throw new ArgumentException();
            return outputs[index - 1];
        }

        private void updateOutputIndices()
        {
            int idx = 1;
            foreach (RouterOutput output in outputs)
                output.SetIndexFromRouter(this, idx++);
        }

        private void outputsChangedHandler()
        {
            OutputsChanged?.Invoke(this);
            RaisePropertyChanged(nameof(Outputs));
        }

        public delegate void OutputsChangedDelegate(Router router);
        public event OutputsChangedDelegate OutputsChanged;
        #endregion

        #region Crosspoint update
        public void RequestCrosspointUpdate(RouterOutput output, RouterInput input)
        {

            if (!outputs.Contains(output))
                throw new ArgumentException();

            string logMessage = string.Format("Router crosspoint update request. Router: [(#{0}) #1], destination: {2}, source: {3}.",
                ID,
                Name,
                output.Index,
                input.Index);
            LogDispatcher.I(LOG_TAG, logMessage);

            requestCrosspointUpdateImpl(output, input);

        }

        protected abstract void requestCrosspointUpdateImpl(RouterOutput output, RouterInput input);
        protected abstract void queryAllCrosspoints();

        protected void notifyCrosspointChanged(RouterOutput output, RouterInput input)
        {
            if (!outputs.Contains(output))
                throw new ArgumentException();
            output.AssignSource(input);
            RouterMacroTriggers.RouterCrosspointChanged.Call(this);
        }

        protected void notifyCrosspointChanged(int outputIndex, int inputIndex)
        {
            if ((outputIndex < 0) || (outputIndex >= outputs.Count))
                throw new ArgumentException();
            if ((inputIndex < 0) || (inputIndex >= inputs.Count))
                throw new ArgumentException();
            notifyCrosspointChanged(outputs[outputIndex], inputs[inputIndex]);
        }
        #endregion

        #region Property: State
        public delegate void StateChangedDelegate(Router router, RouterState oldState, RouterState newState);
        public event StateChangedDelegate StateChanged;

        private RouterState state = RouterState.Unknown;

        public RouterState State
        {
            get { return state; }
            protected set
            {
                if (value == state)
                    return;
                RouterState oldState = state;
                state = value;
                StateChanged?.Invoke(this, oldState, value);
                RaisePropertyChanged(nameof(State));
            }
        }
        #endregion

        #region Property: StateString
        public delegate void StateStringChangedDelegate(Router router, string oldStateString, string newStateString);
        public event StateStringChangedDelegate StateStringChanged;

        private string stateString = "?";

        public string StateString
        {
            get { return stateString; }
            protected set
            {
                if (value == stateString)
                    return;
                string oldStateString = stateString;
                stateString = value;
                StateStringChanged?.Invoke(this, oldStateString, value);
                RaisePropertyChanged(nameof(StateString));
            }
        }
        #endregion

    }

}
