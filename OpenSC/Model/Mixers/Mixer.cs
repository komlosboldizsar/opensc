using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers
{

    public abstract class Mixer : ModelBase
    {

        public Mixer()
        { }

        public override void Restored()
        {
            restoreInputs();
        }

        #region Property: ID
        public delegate void IdChangedDelegate(Mixer mixer, int oldValue, int newValue);
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
            if (!MixerDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Name
        public delegate void NameChangedDelegate(Mixer mixer, string oldName, string newName);
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

        #region Property: OnProgramInput, OnProgramInputName
        public delegate void OnProgramInputChangedDelegate(Mixer mixer, MixerInput oldInput, MixerInput newInput);
        public event OnProgramInputChangedDelegate OnProgramInputChanged;

        public delegate void OnProgramInputNameChangedDelegate(Mixer mixer, string newName);
        public event OnProgramInputNameChangedDelegate OnProgramInputNameChanged;

        private MixerInput onProgramInput;

        public MixerInput OnProgramInput
        {
            get { return onProgramInput; }
            protected set
            {

                if (value == onProgramInput)
                    return;
                MixerInput oldInput = onProgramInput;

                if (onProgramInput != null)
                    onProgramInput.NameChanged -= onProgramInputNameChangedHandler;

                onProgramInput = value;

                OnProgramInputChanged?.Invoke(this, oldInput, value);
                RaisePropertyChanged(nameof(OnProgramInputName));

                OnProgramInputNameChanged?.Invoke(this, onProgramInput?.Name);
                RaisePropertyChanged(nameof(OnProgramInput));

                if (onProgramInput != null)
                    onProgramInput.NameChanged += onProgramInputNameChangedHandler;

            }
        }

        public string OnProgramInputName
        {
            get => onProgramInput?.Name;
        }

        private void onProgramInputNameChangedHandler(MixerInput input, string oldName, string newName)
        {
            OnProgramInputNameChanged?.Invoke(this, newName);
        }
        #endregion

        #region Property: OnPreviewInput, OnPreviewInputName
        public delegate void OnPreviewInputChangedDelegate(Mixer mixer, MixerInput oldInput, MixerInput newInput);
        public event OnPreviewInputChangedDelegate OnPreviewInputChanged;

        public delegate void OnPreviewInputNameChangedDelegate(Mixer mixer, string newName);
        public event OnPreviewInputNameChangedDelegate OnPreviewInputNameChanged;

        private MixerInput onPreviewInput;

        public MixerInput OnPreviewInput
        {
            get { return onPreviewInput; }
            protected set
            {

                if (value == onPreviewInput)
                    return;
                MixerInput oldInput = onPreviewInput;

                if (onPreviewInput != null)
                    onPreviewInput.NameChanged -= onPreviewInputNameChangedHandler;

                onPreviewInput = value;

                OnPreviewInputChanged?.Invoke(this, oldInput, value);
                RaisePropertyChanged(nameof(OnPreviewInputName));

                OnPreviewInputNameChanged?.Invoke(this, onPreviewInput?.Name);
                RaisePropertyChanged(nameof(OnPreviewInput));

                if (onPreviewInput != null)
                    onPreviewInput.NameChanged += onPreviewInputNameChangedHandler;

            }
        }

        public string OnPreviewInputName
        {
            get => onPreviewInput?.Name;
        }

        private void onPreviewInputNameChangedHandler(MixerInput input, string oldName, string newName)
        {
            OnPreviewInputNameChanged?.Invoke(this, newName);
        }
        #endregion

        #region Property: State
        public delegate void StateChangedDelegate(Mixer mixer, MixerState oldState, MixerState newState);
        public event StateChangedDelegate StateChanged;
        
        private MixerState state = MixerState.Unknown;

        public MixerState State
        {
            get { return state; }
            protected set
            {
                if (value == state)
                    return;
                MixerState oldState = state;
                state = value;
                StateChanged?.Invoke(this, oldState, value);
                RaisePropertyChanged(nameof(State));
            }
        }
        #endregion

        #region Property: StateString
        public delegate void StateStringChangedDelegate(Mixer mixer, string oldStateString, string newStateString);
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

        #region Inputs
        private ObservableList<MixerInput> inputs = new ObservableList<MixerInput>();

        public ObservableList<MixerInput> Inputs
        {
            get { return inputs; }
        }

        [PersistAs("inputs")]
        [PersistAs("input", 1)]
        private MixerInput[] _inputs
        {
            get { return inputs.ToArray(); }
            set
            {
                inputs.Clear();
                if (value != null)
                    inputs.AddRange(value);
            }
        }

        public void AddInput()
        {
            int index = ((inputs.Count > 0) ? (inputs.Max(input => input.Index) + 1) : 1);
            inputs.Add(new MixerInput(string.Format("Input #{0}", index), this, index));
        }

        public void RemoveInput(MixerInput input)
        {
            inputs.Remove(input);
            input.RemovedFromMixer(this);
            if (input == onProgramInput)
                OnProgramInput = null;
            if (input == onPreviewInput)
                OnPreviewInput = null;
        }

        private void inputsChangedHandler()
        {
            InputsChanged?.Invoke(this);
            RaisePropertyChanged(nameof(Inputs));
        }
        private void restoreInputs()
        {
            foreach (MixerInput input in inputs)
                input.Mixer = this;
            foreach (MixerInput input in inputs)
                input.Restored();
        }

        public delegate void InputsChangedDelegate(Mixer mixer);
        public event InputsChangedDelegate InputsChanged;
        #endregion

        protected override void afterUpdate()
        {
            base.afterUpdate();
            MixerDatabase.Instance.ItemUpdated(this);
        }

    }

}
