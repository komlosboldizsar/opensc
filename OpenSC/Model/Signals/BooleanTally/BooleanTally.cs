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
            IdChanged = null;
            NameChanged = null;
        }

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            restoreBooleanAndSource();
            updateToTally();
        }
        #endregion

        #region Property: ID
        public delegate void IdChangedDelegate(BooleanTally booleanTally, int oldValue, int newValue);
        public event IdChangedDelegate IdChanged;

        public int id = 0;

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
        }
        #endregion

        #region Property: Name
        public delegate void NameChangedDelegate(BooleanTally booleanTally, string oldName, string newName);
        public event NameChangedDelegate NameChanged;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (value == name)
                    return;
                string oldName = name;
                name = value;
                NameChanged?.Invoke(this, oldName, value);
                RaisePropertyChanged(nameof(Name));
            }
        }
        #endregion

        #region Property: FromBoolean
        public delegate void FromBooleanChangedDelegate(BooleanTally booleanTally, IBoolean oldValue, IBoolean newValue);
        public event FromBooleanChangedDelegate FromBooleanChanged;

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
            get { return fromBoolean; }
            set
            {
                if (value == fromBoolean)
                    return;
                IBoolean oldFromBoolean = fromBoolean;
                fromBoolean = value;
                bool oldState = oldFromBoolean?.CurrentState ?? false;
                bool newState = fromBoolean?.CurrentState ?? false;
                if (!oldState && newState)
                    toTally?.Give(myRecursionChain);
                if (oldState && !newState)
                    toTally?.Revoke(myRecursionChain);
                FromBooleanChanged?.Invoke(this, oldFromBoolean, value);
                RaisePropertyChanged(nameof(FromBoolean));
            }
        }
        #endregion

        #region Property: ToSignal
        public delegate void ToSignalChangedDelegate(BooleanTally booleanTally, ISignalSourceRegistered oldValue, ISignalSourceRegistered newValue);
        public event ToSignalChangedDelegate ToSignalChanged;

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
            get { return toSignal; }
            set
            {
                if (value == toSignal)
                    return;
                ISignalSourceRegistered oldToSignal = toSignal;
                toSignal = value;
                updateToTally();
                ToSignalChanged?.Invoke(this, oldToSignal, value);
                RaisePropertyChanged(nameof(ToSignal));
            }
        }
        #endregion

        #region Property: ToTallyColor
        public delegate void ToTallyColorChangedDelegate(BooleanTally booleanTally, SignalTallyColor oldValue, SignalTallyColor newValue);
        public event ToTallyColorChangedDelegate ToTallyColorChanged;

        [PersistAs("to_tally_color")]
        private SignalTallyColor toTallyColor;

        public SignalTallyColor ToTallyColor
        {
            get { return toTallyColor; }
            set
            {
                if (value == toTallyColor)
                    return;
                SignalTallyColor oldToTallyColor = toTallyColor;
                toTallyColor = value;
                updateToTally();
                ToTallyColorChanged?.Invoke(this, oldToTallyColor, value);
                RaisePropertyChanged(nameof(ToTallyColor));
            }
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

        private void updateToTally()
            => ToTally = toSignal?.GetTally(toTallyColor);
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
