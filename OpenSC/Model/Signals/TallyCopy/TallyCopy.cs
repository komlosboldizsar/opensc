using OpenSC.Model.Persistence;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace OpenSC.Model.Signals.TallyCopying
{

    public class TallyCopy : ModelBase, ISignalTallySender
    {

        #region Persistence, instantiation
        public TallyCopy()
        { }

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
            restoreSources();
            updateToTally();
            updateFromTally();
        }
        #endregion

        #region Property: ID
        public event PropertyChangedTwoValuesDelegate<TallyCopy, int> IdChanged;

        public int id = 0;

        public override int ID
        {
            get => id;
            set
            {
                ValidateId(value);
                setProperty(this, ref id, value, IdChanged);
            }
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
        }
        #endregion

        #region Property: Name
        public event PropertyChangedTwoValuesDelegate<TallyCopy, string> NameChanged;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get => name;
            set => setProperty(this, ref name, value, NameChanged);
        }
        #endregion

        #region Property: FromSignal
        public event PropertyChangedTwoValuesDelegate<TallyCopy, ISignalSourceRegistered> FromSignalChanged;

        private string _fromSignalUniqueId; // "Temp foreign key"

        [PersistAs("from_signal")]
        private string fromSignalUniqueId
        {
            get => fromSignal?.SignalUniqueId;
            set => _fromSignalUniqueId = value;
        }

        private ISignalSourceRegistered fromSignal;

        public ISignalSourceRegistered FromSignal
        {
            get => fromSignal;
            set => setProperty(this, ref fromSignal, value, FromSignalChanged, null, (ov, nv) => updateFromTally());
        }
        #endregion

        #region Property: FromTallyColor
        public event PropertyChangedTwoValuesDelegate<TallyCopy, SignalTallyColor> FromTallyColorChanged;

        [PersistAs("from_tally_color")]
        private SignalTallyColor fromTallyColor;

        public SignalTallyColor FromTallyColor
        {
            get => fromTallyColor;
            set => setProperty(this, ref fromTallyColor, value, FromTallyColorChanged, null, (ov, nv) => updateFromTally());
        }
        #endregion

        #region Property: FromTally
        private IBidirectionalSignalTally fromTally;

        private IBidirectionalSignalTally FromTally
        {
            get => fromTally;
            set
            {
                if (fromTally != null)
                {
                    fromTally.Got -= fromSideGotTally;
                    fromTally.Revoked -= fromSideRevokedTally;
                }
                if (toTally != null)
                {
                    foreach (List<ISignalTallySender> extendedRecursionChain in recursionChains.Values)
                        toTally.Revoke(extendedRecursionChain);
                }
                fromTally = value;
                if (fromTally != null)
                {
                    foreach (List<ISignalTallySender> recursionChain in fromTally.CurrentRecursionChains)
                        fromSideGotTally(fromTally, recursionChain);
                    fromTally.Got += fromSideGotTally;
                    fromTally.Revoked += fromSideRevokedTally;
                }
            }
        }

        private void updateFromTally()
            => FromTally = fromSignal?.GetTally(fromTallyColor);
        #endregion

        #region Property: ToSignal
        public event PropertyChangedTwoValuesDelegate<TallyCopy, ISignalSourceRegistered> ToSignalChanged;

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
        public event PropertyChangedTwoValuesDelegate<TallyCopy, SignalTallyColor> ToTallyColorChanged;

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
                    foreach (List<ISignalTallySender> extendedRecursionChain in recursionChains.Values)
                        toTally.Revoke(extendedRecursionChain);
                toTally = value;
                if (toTally != null)
                    foreach (List<ISignalTallySender> extendedRecursionChain in recursionChains.Values)
                        toTally.Give(extendedRecursionChain);
            }
        }

        private void updateToTally()
            => ToTally = toSignal?.GetTally(toTallyColor);
        #endregion

        #region Source restoration
        private void restoreSources()
        {
            if (_fromSignalUniqueId != null)
                FromSignal = SignalRegister.Instance.GetSignalByUniqueId(_fromSignalUniqueId);
            if (_toSignalUniqueId != null)
                ToSignal = SignalRegister.Instance.GetSignalByUniqueId(_toSignalUniqueId);
        }
        #endregion

        #region Handle tally events
        private void fromSideRevokedTally(ISignalTallyReceiver receiver, List<ISignalTallySender> recursionChain)
        {
            if (recursionChain.Contains(this))
                return; // endless loop
            List<ISignalTallySender> extendedChain = null;
            foreach (KeyValuePair<List<ISignalTallySender>, List<ISignalTallySender>> chainKp in recursionChains)
            {
                if (recursionChain.SequenceEqual(chainKp.Key))
                {
                    extendedChain = chainKp.Value;
                    break;
                }
            }
            if (extendedChain == null)
                return;
            toTally.Revoke(extendedChain);
        }

        private void fromSideGotTally(ISignalTallyReceiver receiver, List<ISignalTallySender> recursionChain)
        {
            if (toTally == null)
                return;
            if (recursionChain.Contains(this))
                return; // endless loop
            if (recursionChains.ContainsKey(recursionChain))
                return;
            List<ISignalTallySender> extendedChain = recursionChain.ExtendRecursionChainT(this);
            recursionChains.Add(recursionChain, extendedChain);
            toTally.Give(extendedChain);
        }
        #endregion

        #region Recursion chains
        private void extendRecursionChain(ISignalTallyReceiver originalReceiver, List<ISignalTallySender> recursionChain)
            => recursionChain.ExtendRecursionChainT<ISignalTallySender>(this);

        private Dictionary<List<ISignalTallySender>, List<ISignalTallySender>> recursionChains = new Dictionary<List<ISignalTallySender>, List<ISignalTallySender>>();
        #endregion

        #region ISignalTallySender interface
        public string Label => string.Format("Tally copy [(#{0}) {1}]", ID, Name);
        #endregion

    }

}
