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
    
    public abstract class UMD : ModelBase
    {

        public abstract IUMDType Type { get; }
        public abstract Color[] TallyColors { get; }

        protected abstract void update();

        public override void Restored()
        {
            restoreTallySources();
        }

        public override void Removed()
        {
            base.Removed();
            IdChanged = null;
            NameChanged = null;
            StaticTextChanged = null;
            UseStaticTextChanged = null;
            for (int i = 0; i < MAX_TALLIES; i++)
                SetTallySource(i, null);
        }

        public UMD()
        { }

        public delegate void CurrentTextChangedDelegate(UMD umd, string oldText, string newText);
        public event CurrentTextChangedDelegate CurrentTextChanged;

        protected string currentText;

        public string CurrentText
        {
            get { return currentText; }
            set
            {
                string oldValue = currentText;
                currentText = value;
                update();
                CurrentTextChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(CurrentText));
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

        public delegate void StaticTextChangedDelegate(UMD umd, string oldText, string newText);
        public event StaticTextChangedDelegate StaticTextChanged;

        [PersistAs("static_text")]
        private string staticText;

        public string StaticText
        {
            get { return staticText; }
            set
            {
                string oldValue = staticText;
                staticText = value;
                if (useStaticText)
                    CurrentText = value;
                StaticTextChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(StaticText));
            }
        }

        public delegate void UseStaticTextChangedDelegate(UMD umd, bool oldState, bool newState);
        public event UseStaticTextChangedDelegate UseStaticTextChanged;

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
                useStaticText = value;
                CurrentText = useStaticText ? staticText : dynamicText;
                UseStaticTextChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(UseStaticText));
            }
        }

        #region Tallies
        public delegate void TallyChangedDelegate(UMD umd, int index, bool oldState, bool newState);
        public event TallyChangedDelegate TallyChanged;

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

            tallyStates[index] = state;
            tallyChanged(index, state);

            TallyChanged?.Invoke(this, index, !state, state);
            RaisePropertyChanged(nameof(TallyStates));

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

        public delegate void IdChangedDelegate(UMD timer, int oldValue, int newValue);
        public event IdChangedDelegate IdChanged;

        private int id = 0;

        public override int ID
        {
            get { return id; }
            set
            {
                ValidateId(value);
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
            if (!UmdDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }


        public delegate void NameChangedDelegate(UMD timer, string oldName, string newValue);
        public event NameChangedDelegate NameChanged;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                ValidateName(value);
                string oldValue = name;
                name = value;
                NameChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(Name));
            }

        }

        public void ValidateName(string name)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentException();
        }

        protected override void afterUpdate()
        {
            base.afterUpdate();
            UmdDatabase.Instance.ItemUpdated(this);
        }

    }
}
