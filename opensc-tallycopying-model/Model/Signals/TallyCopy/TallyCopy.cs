using OpenSC.Model.General;
using OpenSC.Model.Persistence;
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
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = TallyCopyDatabase.Instance;
        #endregion

        #region Property: FromSignal
        public event PropertyChangedTwoValuesDelegate<TallyCopy, ISignalSourceRegistered> FromSignalChanged;

        private ISignalSourceRegistered fromSignal;

        [PersistAs("from_signal")]
        public ISignalSourceRegistered FromSignal
        {
            get => fromSignal;
            set => this.setProperty(ref fromSignal, value, FromSignalChanged, null, (ov, nv) => updateFromTally());
        }
        #endregion

        #region Property: FromTallyColor
        public event PropertyChangedTwoValuesDelegate<TallyCopy, SignalTallyColor> FromTallyColorChanged;

        private SignalTallyColor fromTallyColor;

        [PersistAs("from_tally_color")]
        public SignalTallyColor FromTallyColor
        {
            get => fromTallyColor;
            set => this.setProperty(ref fromTallyColor, value, FromTallyColorChanged, null, (ov, nv) => updateFromTally());
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

        private ISignalSourceRegistered toSignal;

        [PersistAs("to_signal")]
        public ISignalSourceRegistered ToSignal
        {
            get => toSignal;
            set => this.setProperty(ref toSignal, value, ToSignalChanged, null, (ov, nv) => updateToTally());
        }
        #endregion

        #region Property: ToTallyColor
        public event PropertyChangedTwoValuesDelegate<TallyCopy, SignalTallyColor> ToTallyColorChanged;

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
