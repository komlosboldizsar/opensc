using OpenSC.Model.Persistence;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{

    public delegate void UmdIdChangingDelegate(UMD timer, int oldValue, int newValue);
    public delegate void UmdIdChangedDelegate(UMD timer, int oldValue, int newValue);

    public delegate void UmdNameChangingDelegate(UMD timer, string oldName, string newName);
    public delegate void UmdNameChangedDelegate(UMD timer, string oldName, string newValue);

    public delegate void UmdTextChanging(UMD umd, string oldText, string newText);
    public delegate void UmdTextChanged(UMD umd, string oldText, string newText);

    public delegate void UmdUseStaticTextChanging(UMD umd, bool oldState, bool newState);
    public delegate void UmdUseStaticTextChanged(UMD umd, bool oldState, bool newState);

    public delegate void UmdTallyChanging(UMD umd, int index, bool oldState, bool newState);
    public delegate void UmdTallyChanged(UMD umd, int index, bool oldState, bool newState);

    public abstract class UMD : ModelBase
    {

        public abstract IUMDType Type { get; }
        public abstract Color[] TallyColors { get; }

        protected abstract void update();

        public override void Restored()
        {
            restoreTallySources();
        }

        public UMD()
        { }

        public event UmdTextChanging CurrentTextChanging;
        public event UmdTextChanged CurrentTextChanged;
        public event ParameterlessChangeNotifierDelegate CurrentTextChangingPCN;
        public event ParameterlessChangeNotifierDelegate CurrentTextChangedPCN;

        protected string currentText;

        public string CurrentText
        {
            get { return currentText; }
            set
            {
                string oldValue = currentText;
                CurrentTextChanging?.Invoke(this, oldValue, value);
                CurrentTextChangingPCN?.Invoke();
                currentText = value;
                update();
                CurrentTextChanged?.Invoke(this, oldValue, value);
                CurrentTextChangedPCN?.Invoke();
            }
        }

        private string dynamicText;
        
        protected string DynamicText
        {
            get { return dynamicText; }
            set
            {
                dynamicText = value;
                if (!useStaticText)
                    CurrentText = value;
            }
        }

        public event UmdTextChanging StaticTextChanging;
        public event UmdTextChanged StaticTextChanged;
        public event ParameterlessChangeNotifierDelegate StaticTextChangingPCN;
        public event ParameterlessChangeNotifierDelegate StaticTextChangedPCN;

        [PersistAs("static_text")]
        private string staticText;

        public string StaticText
        {
            get { return staticText; }
            set
            {
                string oldValue = staticText;
                StaticTextChanging?.Invoke(this, oldValue, value);
                StaticTextChangingPCN?.Invoke();
                staticText = value;
                if (useStaticText)
                    CurrentText = value;
                StaticTextChanged?.Invoke(this, oldValue, value);
                StaticTextChangedPCN?.Invoke();
            }
        }

        public event UmdUseStaticTextChanging UseStaticTextChanging;
        public event UmdUseStaticTextChanged UseStaticTextChanged;
        public event ParameterlessChangeNotifierDelegate UseStaticTextChangingPCN;
        public event ParameterlessChangeNotifierDelegate UseStaticTextChangedPCN;

        [PersistAs("use_static_text")]
        private bool useStaticText = false;

        public bool UseStaticText
        {
            get { return useStaticText; }
            set
            {
                bool oldValue = useStaticText;
                if (oldValue == value)
                    return;
                UseStaticTextChanging?.Invoke(this, oldValue, value);
                UseStaticTextChangingPCN?.Invoke();
                useStaticText = value;
                CurrentText = useStaticText ? staticText : dynamicText;
                UseStaticTextChanged?.Invoke(this, oldValue, value);
                UseStaticTextChangedPCN?.Invoke();
            }
        }

        #region Tallies
        public event UmdTallyChanging TallyChanging;
        public event UmdTallyChanged TallyChanged;
        public event ParameterlessChangeNotifierDelegate TallyChangingPCN;
        public event ParameterlessChangeNotifierDelegate TallyChangedPCN;

        public const int MAX_TALLIES = 8;

        [PersistAs("tally_sources")]
        private string[] _tallySources = new string[MAX_TALLIES];

        private IBoolean[] tallySources = new IBoolean[MAX_TALLIES];

        public void SetTallySource(int index, IBoolean source)
        {
            if ((index < 0) || (index >= MAX_TALLIES) || (index >= Type.TallyCount))
                throw new ArgumentOutOfRangeException(nameof(index));
            if (source == tallySources[index])
                return;
            if(tallySources[index] != null)
                tallySources[index].StateChanged -= tallyStateChangedHandler;
            tallySources[index] = source;
            _tallySources[index] = source?.Name;
            if (tallySources[index] != null)
            {
                tallySources[index].StateChanged += tallyStateChangedHandler;
                updateTally(index, source.CurrentState);
            }
            else
            {
                updateTally(index, false);
            }
        }

        public IBoolean GetTallySource(int index)
        {
            if ((index < 0) || (index >= MAX_TALLIES) || (index >= Type.TallyCount))
                throw new ArgumentOutOfRangeException(nameof(index));
            return tallySources[index];
        }

        private void restoreTallySources()
        {
            string[] restoredTallySourceNames = _tallySources;
            for (int i = 0; i < Type.TallyCount; i++) {
                string sourceName = restoredTallySourceNames[i];
                IBoolean tallySource = BooleanRegister.Instance[sourceName];
                SetTallySource(i, tallySource);
            }
        }   

        private void tallyStateChangedHandler(IBoolean boolean, bool newState)
        {
            for (int i = 0; i < MAX_TALLIES; i++)
                if (tallySources[i] == boolean)
                    updateTally(i, newState);
        }

        private bool[] tallyStates = new bool[MAX_TALLIES];

        public bool[] TallyStates
        {
            get => tallyStates;
        }

        private void updateTally(int index, bool state)
        {

            if (tallyStates[index] == state)
                return;

            TallyChanging?.Invoke(this, index, !state, state);
            TallyChangingPCN?.Invoke();

            tallyStates[index] = state;
            tallyChanged(index, state);

            TallyChanged?.Invoke(this, index, !state, state);
            TallyChangedPCN?.Invoke();

        }

        private bool GetTallyState(int index)
        {
            if ((index < 0) || (index >= MAX_TALLIES) || (index >= Type.TallyCount))
                throw new ArgumentOutOfRangeException(nameof(index));
            return tallyStates[index];
        }

        protected virtual void tallyChanged(int index, bool state)
        { }
        #endregion


        public event UmdIdChangingDelegate IdChanging;
        public event UmdIdChangedDelegate IdChanged;
        public event ParameterlessChangeNotifierDelegate IdChangingPCN;
        public event ParameterlessChangeNotifierDelegate IdChangedPCN;

        private int id = 0;

        public override int ID
        {
            get { return id; }
            set
            {
                ValidateId(value);
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
            if (!UmdDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

        public event UmdNameChangingDelegate NameChanging;
        public event UmdNameChangedDelegate NameChanged;
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
                string oldValue = name;
                NameChanging?.Invoke(this, oldValue, value);
                NameChangingPCN?.Invoke();
                name = value;
                NameChanged?.Invoke(this, oldValue, value);
                NameChangedPCN?.Invoke();
            }

        }

        public void ValidateName(string name)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentException();
        }

    }
}
