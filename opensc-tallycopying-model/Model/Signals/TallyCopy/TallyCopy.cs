using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace OpenSC.Model.Signals.TallyCopying
{

    public partial class TallyCopy : ModelBase, ISignalTallySender
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
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(updateFromTally))]
        [PersistAs("from")]
        private ISignalSourceRegistered fromSignal;
        #endregion

        #region Property: FromTallyColor
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(updateFromTally))]
        [PersistAs("from/@color")]
        private SignalTallyColor fromTallyColor;
        #endregion

        #region Property: FromTally
        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_fromTally_beforeChange))]
        [AutoProperty.AfterChange(nameof(_fromTally_afterChange))]
        private IBidirectionalSignalTally fromTally;

        private void _fromTally_beforeChange(IBidirectionalSignalTally oldValue, IBidirectionalSignalTally newValue, BeforeChangePropertyArgs args)
        {
            if (oldValue != null)
            {
                oldValue.Got -= fromSideGotTally;
                oldValue.Revoked -= fromSideRevokedTally;
            }
            if (toTally != null)
            {
                foreach (List<ISignalTallySender> extendedRecursionChain in recursionChains.Values)
                    toTally.Revoke(extendedRecursionChain);
            }
        }

        private void _fromTally_afterChange(IBidirectionalSignalTally oldValue, IBidirectionalSignalTally newValue)
        {
            if (newValue != null)
            {
                foreach (List<ISignalTallySender> recursionChain in newValue.CurrentRecursionChains)
                    fromSideGotTally(newValue, recursionChain);
                newValue.Got += fromSideGotTally;
                newValue.Revoked += fromSideRevokedTally;
            }
        }

        private void updateFromTally() => FromTally = fromSignal?.GetTally(fromTallyColor);
        #endregion

        #region Property: ToSignal
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(updateToTally))]
        [PersistAs("to")]
        private ISignalSourceRegistered toSignal;
        #endregion

        #region Property: ToTallyColor
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(updateToTally))]
        [PersistAs("to/@color")]
        private SignalTallyColor toTallyColor;
        #endregion

        #region Property: ToTally
        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_toTally_beforeChange))]
        [AutoProperty.AfterChange(nameof(_toTally_afterChange))]
        private IBidirectionalSignalTally toTally;

        private void _toTally_beforeChange(IBidirectionalSignalTally oldValue, IBidirectionalSignalTally newValue, BeforeChangePropertyArgs args)
        {
            if (oldValue != null)
                foreach (List<ISignalTallySender> extendedRecursionChain in recursionChains.Values)
                    oldValue.Revoke(extendedRecursionChain);
        }

        private void _toTally_afterChange(IBidirectionalSignalTally oldValue, IBidirectionalSignalTally newValue)
        {
            if (newValue != null)
                foreach (List<ISignalTallySender> extendedRecursionChain in recursionChains.Values)
                    newValue.Give(extendedRecursionChain);
        }

        private void updateToTally() => ToTally = toSignal?.GetTally(toTallyColor);
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
