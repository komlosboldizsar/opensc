using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers
{

    public delegate void MixerIdChangingDelegate(Mixer mixer, int oldValue, int newValue);
    public delegate void MixerIdChangedDelegate(Mixer mixer, int oldValue, int newValue);

    public delegate void MixerNameChangingDelegate(Mixer mixer, string oldName, string newName);
    public delegate void MixerNameChangedDelegate(Mixer mixer, string oldName, string newName);

    public delegate void MixerOnProgramInputChangingDelegate(Mixer mixer, MixerInput oldInput, MixerInput newInput);
    public delegate void MixerOnProgramInputChangedDelegate(Mixer mixer, MixerInput oldInput, MixerInput newInput);
    public delegate void MixerOnProgramInputNameChangedDelegate(Mixer mixer, string newName);
    
    public delegate void MixerOnPreviewInputChangingDelegate(Mixer mixer, MixerInput oldInput, MixerInput newInput);
    public delegate void MixerOnPreviewInputChangedDelegate(Mixer mixer, MixerInput oldInput, MixerInput newInput);
    public delegate void MixerOnPreviewInputNameChangedDelegate(Mixer mixer, string newName);

    public delegate void MixerInputsChangedDelegate(Mixer mixer);

    public abstract class Mixer : ModelBase
    {

        public Mixer()
        { }

        public override void Restored()
        {
            restoreInputSources();
        }

        #region Property: ID
        public event MixerIdChangingDelegate IdChanging;
        public event MixerIdChangedDelegate IdChanged;
        public event ParameterlessChangeNotifierDelegate IdChangingPCN;
        public event ParameterlessChangeNotifierDelegate IdChangedPCN;

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
                IdChanging?.Invoke(this, oldValue, value);
                IdChangingPCN?.Invoke();
                id = value;
                IdChanged?.Invoke(this, oldValue, value);
                IdChangedPCN?.Invoke();
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
        public event MixerNameChangingDelegate NameChanging;
        public event MixerNameChangedDelegate NameChanged;
        public event ParameterlessChangeNotifierDelegate NameChangingPCN;
        public event ParameterlessChangeNotifierDelegate NameChangedPCN;

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
                NameChanging?.Invoke(this, oldName, value);
                NameChangingPCN?.Invoke();
                name = value;
                NameChanged?.Invoke(this, oldName, value);
                NameChangedPCN?.Invoke();
            }
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
        }
        #endregion

        #region Property: OnProgramInput, OnProgramInputName
        public event MixerOnProgramInputChangingDelegate OnProgramInputChanging;
        public event MixerOnProgramInputChangedDelegate OnProgramInputChanged;
        public event ParameterlessChangeNotifierDelegate OnProgramInputChangingPCN;
        public event ParameterlessChangeNotifierDelegate OnProgramInputChangedPCN;

        public event MixerOnProgramInputNameChangedDelegate OnProgramInputNameChanged;
        public event ParameterlessChangeNotifierDelegate OnProgramInputNameChangedPCN;

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

                OnProgramInputChanging?.Invoke(this, oldInput, value);
                OnProgramInputChangingPCN?.Invoke();

                onProgramInput = value;

                OnProgramInputChanged?.Invoke(this, oldInput, value);
                OnProgramInputChangedPCN?.Invoke();

                OnProgramInputNameChanged?.Invoke(this, onProgramInput?.Name);
                OnProgramInputNameChangedPCN?.Invoke();

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
            OnProgramInputNameChangedPCN?.Invoke();
        }
        #endregion

        #region Property: OnPreviewInput, OnPreviewInputName
        public event MixerOnPreviewInputChangingDelegate OnPreviewInputChanging;
        public event MixerOnPreviewInputChangedDelegate OnPreviewInputChanged;
        public event ParameterlessChangeNotifierDelegate OnPreviewInputChangingPCN;
        public event ParameterlessChangeNotifierDelegate OnPreviewInputChangedPCN;

        public event MixerOnPreviewInputNameChangedDelegate OnPreviewInputNameChanged;
        public event ParameterlessChangeNotifierDelegate OnPreviewInputNameChangedPCN;

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

                OnPreviewInputChanging?.Invoke(this, oldInput, value);
                OnPreviewInputChangingPCN?.Invoke();

                onPreviewInput = value;

                OnPreviewInputChanged?.Invoke(this, oldInput, value);
                OnPreviewInputChangedPCN?.Invoke();

                OnPreviewInputNameChanged?.Invoke(this, onPreviewInput?.Name);
                OnPreviewInputNameChangedPCN?.Invoke();

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
            OnPreviewInputNameChangedPCN?.Invoke();
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
            InputsChangedPCN?.Invoke();
        }
        private void restoreInputSources()
        {
            foreach (MixerInput input in inputs)
                input.RestoreSource();
        }

        public event MixerInputsChangedDelegate InputsChanged;
        public event ParameterlessChangeNotifierDelegate InputsChangedPCN;
        #endregion

    }

}
