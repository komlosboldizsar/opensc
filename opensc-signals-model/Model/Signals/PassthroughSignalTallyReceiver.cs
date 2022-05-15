using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public class PassthroughSignalTallyReceiver : ISignalTallyReceiver, ISignalTallySender
    {

        public PassthroughSignalTallyReceiver(ISignalSource parentSignalSource, SignalTallyColor color)
        {
            ParentSignalSource = parentSignalSource;
            Color = color;
        }

        public ISignalSource ParentSignalSource { get; private init; }
        public SignalTallyColor Color { get; private init; }

        #region Property: PreviousElement
        private ISignalTallyReceiver previousElement = null;

        public ISignalTallyReceiver PreviousElement
        {
            get => previousElement;
            set
            {
                if (previousElement != null)
                    foreach (List<ISignalTallySender> extendedChain in recursionChains.Values)
                        previousElement.Revoke(extendedChain);
                previousElement = value;
                if (previousElement != null)
                    foreach (List<ISignalTallySender> extendedChain in recursionChains.Values)
                        previousElement.Give(extendedChain);
            }
        }
        #endregion

        #region Stored recursion chains
        private Dictionary<List<ISignalTallySender>, List<ISignalTallySender>> recursionChains = new Dictionary<List<ISignalTallySender>, List<ISignalTallySender>>();
        #endregion

        #region ISignalTallyReceiver implementation
        public void Give(List<ISignalTallySender> recursionChain)
        {
            if (recursionChain.Contains(this))
                return; // endless loop
            if (recursionChains.ContainsKey(recursionChain))
                return;
            List<ISignalTallySender> extendedChain = recursionChain.ExtendRecursionChainT(this);
            recursionChains.Add(recursionChain, extendedChain);
            PreviousElement?.Give(extendedChain);
            Got?.Invoke(this, recursionChain);
        }

        public void Revoke(List<ISignalTallySender> recursionChain)
        {
            if (recursionChain.Contains(this))
                return; // endless loop
            List<ISignalTallySender> baseChain = null;
            List<ISignalTallySender> extendedChain = null;
            foreach (KeyValuePair<List<ISignalTallySender>, List<ISignalTallySender>> chainKp in recursionChains)
            {
                if (recursionChain.SequenceEqual(chainKp.Key))
                {
                    baseChain = chainKp.Key;
                    extendedChain = chainKp.Value;
                    break;
                }
            }
            if (extendedChain == null)
                return;
            recursionChains.Remove(baseChain);
            PreviousElement?.Revoke(extendedChain);
            recursionChains.Remove(recursionChain);
            Revoked?.Invoke(this, recursionChain);
        }

        public event SignalTallyReceiverGotTally Got;
        public event SignalTallyReceiverRevokedTally Revoked;

        public List<List<ISignalTallySender>> CurrentRecursionChains => recursionChains.Keys.ToList();
        #endregion

        #region ISignalTallySender implementation
        public string Label { get; set; }
        #endregion

    }

}
