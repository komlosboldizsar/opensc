using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public class PassthroughSignalTallyReceiver : ISignalTallyReceiver, ISignalTallySender
    {

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

        public string Label { get; set; }

        private Dictionary<List<ISignalTallySender>, List<ISignalTallySender>> recursionChains = new Dictionary<List<ISignalTallySender>, List<ISignalTallySender>>();

        public void Give(List<ISignalTallySender> recursionChain)
        {
            if (recursionChain.Contains(this))
                return; // endless loop
            if (recursionChains.ContainsKey(recursionChain))
                return;
            List<ISignalTallySender> extendedChain = new List<ISignalTallySender>(recursionChain);
            extendedChain.Add(this);
            recursionChains.Add(recursionChain, extendedChain);
            PreviousElement?.Give(extendedChain);
        }

        public void Revoke(List<ISignalTallySender> recursionChain)
        {
            if (recursionChain.Contains(this))
                return; // endless loop
            recursionChains.TryGetValue(recursionChain, out List<ISignalTallySender> extendedChain);
            if (extendedChain == null)
                return;
            PreviousElement?.Revoke(extendedChain);
            recursionChains.Remove(recursionChain);
        }

    }

}
