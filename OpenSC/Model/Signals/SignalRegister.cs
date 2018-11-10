using OpenSC.Model.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public class SignalRegister : IObservableList<ISignal>
    {

        #region Singleton
        public static SignalRegister Instance { get; } = new SignalRegister();

        private SignalRegister()
        { }
        #endregion

        private List<ISignal> registeredSignals = new List<ISignal>();

        public ISignal this[int index] => registeredSignals[index];

        public int Count => registeredSignals.Count;

        public event ObservableListItemAddedDelegate ItemAdded;
        public event ObservableListItemRemovedDelegate ItemRemoved;
        public event ObservableListItemsChangedDelegate ItemsChanged;

        public IEnumerator<ISignal> GetEnumerator()
            => registeredSignals.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => registeredSignals.GetEnumerator();

        public void RegisterSignal(ISignal signal)
        {
            if (registeredSignals.Contains(signal))
                return;
            registeredSignals.Add(signal);
            ItemAdded?.Invoke();
            ItemsChanged?.Invoke();
        }

        public void UnregisterSignal(ISignal signal)
        {
            if (!registeredSignals.Contains(signal))
                return;
            registeredSignals.Remove(signal);
            ItemRemoved?.Invoke();
            ItemsChanged?.Invoke();
        }

        public ISignal GetSignalByUniqueId(string uniqueId)
        {
            foreach (ISignal signal in registeredSignals)
                if (signal.SignalUniqueId == uniqueId)
                    return signal;
            return null;
        }
        
    }

}
