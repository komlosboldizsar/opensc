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

        public override void RestoreCustomRelations()
        {
            base.RestoreCustomRelations();
            restoreTallySources();
        }

        public override void Removed()
        {
            base.Removed();
            NameChanged = null;
            StaticTextChanged = null;
            UseStaticTextChanged = null;
            for (int i = 0; i < MAX_TALLIES; i++)
                SetTallySource(i, null);
        }

        public UMD()
        { }

        #region Property: ID
        protected override void validateIdForDatabase(int id)
        {
            if (!UmdDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Name
        public event PropertyChangedTwoValuesDelegate<UMD, string> NameChanged;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get => name;
            set => setProperty(this, ref name, value, NameChanged, validator: ValidateName);
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException();
        }
        #endregion

        #region Property: CurrentText
        public event PropertyChangedTwoValuesDelegate<UMD, string> CurrentTextChanged;

        protected string currentText = "";

        public string CurrentText
        {
            get => currentText;
            set => setProperty(this, ref currentText, value, CurrentTextChanged, null, (ov, nv) => update());
        }
        #endregion

        #region Property: DynamicText
        public event PropertyChangedTwoValuesDelegate<UMD, string> DynamicTextChanged;

        private string dynamicText;
        
        protected string DynamicText
        {
            get => dynamicText;
            set
            {
                AfterChangePropertyDelegate<string> afterChangeDelegate = (ov, nv) =>
                {
                    if (!useStaticText)
                        CurrentText = nv;
                };
                setProperty(this, ref dynamicText, value, DynamicTextChanged);
            }
        }
        #endregion

        #region Property: StaticText
        public event PropertyChangedTwoValuesDelegate<UMD, string> StaticTextChanged;

        [PersistAs("static_text")]
        private string staticText;

        public string StaticText
        {
            get => staticText;
            set
            {
                AfterChangePropertyDelegate<string> afterChangeDelegate = (ov, nv) => {
                    if (useStaticText)
                        CurrentText = nv;
                };
                setProperty(this, ref staticText, value, StaticTextChanged);
            }
        }
        #endregion

        #region Property: UseStaticText
        public event PropertyChangedTwoValuesDelegate<UMD, bool> UseStaticTextChanged;

        [PersistAs("use_static_text")]
        private bool useStaticText = false;

        public bool UseStaticText
        {
            get => useStaticText;
            set => setProperty(this, ref useStaticText, value, UseStaticTextChanged, null,
                (ov, nv) => { CurrentText = nv ? staticText : dynamicText; });
        }
        #endregion

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

        private void tallyStateChangedHandler(IBoolean boolean, bool oldState, bool newState)
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

    }
}
