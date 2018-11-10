using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{
    
    public abstract class Router : ModelBase
    {

        public Router()
        {
            inputs.ItemsChanged += inputsChangedHandler;
            outputs.ItemsChanged += outputsChangedHandler;
        }

        public override void Restored()
        {
            restoreInputSources();
            updateCrosspointRouterAssociations();
            notifyInputsOutputsRestored();
            updateAllCrosspoints();
        }

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

        private ObservableList<RouterInput> inputs = new ObservableList<RouterInput>();

        public ObservableList<RouterInput> Inputs
        {
            get { return inputs; }
        }

        [PersistAs("inputs")]
        [PersistAs("input", 1)]
        private RouterInput[] _inputs
        {
            get { return inputs.ToArray(); }
            set
            {
                inputs.Clear();
                if(value != null)
                    inputs.AddRange(value);
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

        private void updateInputIndices()
        {
            for (int i = 0; i < inputs.Count; i++)
                inputs[i].Index = i;
        }

        private void inputsChangedHandler()
        {
            InputsChanged?.Invoke(this);
            RaisePropertyChanged(nameof(Inputs));
        }

        public delegate void InputsChangedDelegate(Router router);
        public event InputsChangedDelegate InputsChanged;

        private ObservableList<RouterOutput> outputs = new ObservableList<RouterOutput>();

        public ObservableList<RouterOutput> Outputs
        {
            get { return outputs; }
        }

        [PersistAs("outputs")]
        [PersistAs("output", 1)]
        private RouterOutput[] _outputs
        {
            get { return outputs.ToArray(); }
            set
            {
                outputs.Clear();
                if(value != null)
                    outputs.AddRange(value);
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

        private void updateOutputIndices()
        {
            for (int i = 0; i < outputs.Count; i++)
                outputs[i].Index = i;
        }

        private void outputsChangedHandler()
        {
            OutputsChanged?.Invoke(this);
            RaisePropertyChanged(nameof(Outputs));
        }

        public delegate void OutputsChangedDelegate(Router router);
        public event OutputsChangedDelegate OutputsChanged;

        public bool UpdateCrosspoint(RouterOutput output, RouterInput input)
        {
            if (!outputs.Contains(output))
                return false;
            return setCrosspoint(output, input);
        }

        protected abstract bool setCrosspoint(RouterOutput output, RouterInput input);
        protected abstract void updateAllCrosspoints();

        private void updateCrosspointRouterAssociations()
        {
            foreach (RouterInput input in inputs)
                input.Router = this;
            foreach (RouterOutput output in outputs)
                output.Router = this;
        }

        private void restoreInputSources()
        {
            foreach (RouterInput input in inputs)
                input.RestoreSource();
        }

        private void notifyInputsOutputsRestored()
        {
            foreach (RouterInput input in inputs)
                input.Restored();
            foreach (RouterOutput output in outputs)
                output.Restored();
        }

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

        protected override void afterUpdate()
        {
            base.afterUpdate();
            RouterDatabase.Instance.ItemUpdated(this);
        }

    }

}
