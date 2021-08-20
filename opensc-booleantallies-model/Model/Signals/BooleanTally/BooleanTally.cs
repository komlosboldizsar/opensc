using OpenSC.Model.Persistence;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace OpenSC.Model.Signals.BooleanTallies
{

    public class BooleanTally : ModelBase, ISignalTallySender
    {

        #region Persistence, instantiation
        public BooleanTally()
        {
            myRecursionChain = new List<ISignalTallySender>() { this };
        }

        public override void Removed()
        {
            base.Removed();
            Name = null;
        }

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            restoreBooleanAndSource();
            updateToTally();
        }
        #endregion

        #region ID validation
        protected override void validateIdForDatabase(int id)
        {
            if (!BooleanTallyDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Property: FromBoolean
        public event PropertyChangedTwoValuesDelegate<BooleanTally, IBoolean> FromBooleanChanged;

        private string _fromBooleanUniqueId; // "Temp foreign key"

        [PersistAs("from_boolean")]
        private string fromBooleanUniqueId
        {
            get => fromBoolean?.Name;
            set => _fromBooleanUniqueId = value;
        }

        private IBoolean fromBoolean;

        public IBoolean FromBoolean
        {
            get => fromBoolean;
            set
            {
                AfterChangePropertyDelegate<IBoolean> afterChangeDelegate = (ov, nv) => {
                    bool oldState = ov?.CurrentState ?? false;
                    bool newState = nv?.CurrentState ?? false;
                    if (!oldState && newState)
                        toTally?.Give(myRecursionChain);
                    if (oldState && !newState)
                        toTally?.Revoke(myRecursionChain);
                };
                setProperty(this, ref fromBoolean, value, FromBooleanChanged, null, afterChangeDelegate);
            }
        }
        #endregion

        #region Property: ToSignal
        public event PropertyChangedTwoValuesDelegate<BooleanTally, ISignalSourceRegistered> ToSignalChanged;

        private string _toSignalUniqueId; // "Temp foreign key"

        [PersistAs("to_signal")]
        private string toSignalUniqueId
        {
            get => toSignal?.SignalUniqueId;
            set => _toSignalUniqueId = value;
        }

        private ISignalSourceRegistered toSignal;

        public ISignalSourceRegistered ToSignal
        {
            get => toSignal;
            set => setProperty(this, ref toSignal, value, ToSignalChanged, null, (ov, nv) => updateToTally());
        }
        #endregion

        #region Property: ToTallyColor
        public event PropertyChangedTwoValuesDelegate<BooleanTally, SignalTallyColor> ToTallyColorChanged;

        [PersistAs("to_tally_color")]
        private SignalTallyColor toTallyColor;

        public SignalTallyColor ToTallyColor
        {
            get => toTallyColor;
            set => setProperty(this, ref toTallyColor, value, ToTallyColorChanged, null, (ov, nv) => updateToTally());
        }
        #endregion

        #region Property: ToTally
        private IBidirectionalSignalTally toTally;

        private IBidirectionalSignalTally ToTally
        {
            get => toTally;
            set
            {
                if (toTally != null)
                    toTally.Revoke(myRecursionChain);
                toTally = value;
                if (toTally != null)
                    if (fromBoolean?.CurrentState == true)
                        toTally.Give(myRecursionChain);
            }
        }

        private void updateToTally() => ToTally = toSignal?.GetTally(toTallyColor);
        #endregion

        #region Source and boolean restoration
        private void restoreBooleanAndSource()
        {
            if (_fromBooleanUniqueId != null)
                FromBoolean = BooleanRegister.Instance[_fromBooleanUniqueId];
            if (_toSignalUniqueId != null)
                ToSignal = SignalRegister.Instance.GetSignalByUniqueId(_toSignalUniqueId);
        }
        #endregion

        #region ISignalTallySender interface
        public string Label => string.Format("Boolean tally [(#{0}) {1}]", ID, Name);
        #endregion

        private List<ISignalTallySender> myRecursionChain;

    }

}
