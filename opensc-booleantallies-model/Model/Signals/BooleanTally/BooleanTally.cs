using OpenSC.Model.General;
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
            restoreBoolean();
            updateToTally();
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = BooleanTallyDatabase.Instance;
        #endregion

        #region Property: FromBoolean
        public event PropertyChangedTwoValuesDelegate<BooleanTally, IBoolean> FromBooleanChanged;

        private string _fromBooleanUniqueId; // "Temp foreign key"

        [PersistAs("from_boolean")]
        private string fromBooleanUniqueId
        {
            get => fromBoolean?.Identifier;
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
                this.setProperty(ref fromBoolean, value, FromBooleanChanged, null, afterChangeDelegate);
            }
        }
        #endregion

        #region Property: ToSignal
        public event PropertyChangedTwoValuesDelegate<BooleanTally, ISignalSourceRegistered> ToSignalChanged;

        private ISignalSourceRegistered toSignal;

        [PersistAs("to_signal")]
        public ISignalSourceRegistered ToSignal
        {
            get => toSignal;
            set => this.setProperty(ref toSignal, value, ToSignalChanged, null, (ov, nv) => updateToTally());
        }
        #endregion

        #region Property: ToTallyColor
        public event PropertyChangedTwoValuesDelegate<BooleanTally, SignalTallyColor> ToTallyColorChanged;

        private SignalTallyColor toTallyColor;

        [PersistAs("to_tally_color")]
        public SignalTallyColor ToTallyColor
        {
            get => toTallyColor;
            set => this.setProperty(ref toTallyColor, value, ToTallyColorChanged, null, (ov, nv) => updateToTally());
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

        #region Boolean restoration
        private void restoreBoolean()
        {
            if (_fromBooleanUniqueId != null)
                FromBoolean = BooleanRegister.Instance[_fromBooleanUniqueId];
        }
        #endregion

        #region ISignalTallySender interface
        public string Label => string.Format("Boolean tally [(#{0}) {1}]", ID, Name);
        #endregion

        private List<ISignalTallySender> myRecursionChain;

    }

}
