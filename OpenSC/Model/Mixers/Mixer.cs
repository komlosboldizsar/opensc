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

    public delegate void MixerInputsChangedDelegate(Mixer mixer);

    public abstract class Mixer : IModel
    {

        public Mixer()
        { }

        public virtual void Restored()
        {
            restoreInputSources();
        }

        #region Property: ID
        public event MixerIdChangingDelegate IdChanging;
        public event MixerIdChangedDelegate IdChanged;
        public event ParameterlessChangeNotifierDelegate IdChangingPCN;
        public event ParameterlessChangeNotifierDelegate IdChangedPCN;

        public int id = 0;

        public int ID
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
