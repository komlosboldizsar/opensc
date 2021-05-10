using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{
    class SignalForwarder : ISignalSource, ISignalDestination
    {

        #region Instantiation
        public SignalForwarder()
        {
            createTallies();
        }
        #endregion

        #region ISignalSource interface
        public event RegisteredSourceSignalNameChangedDelegate RegisteredSourceSignalNameChanged;
        public string RegisteredSourceSignalName => GetRegisteredSourceSignalName();

        public ISignalSourceRegistered GetRegisteredSourceSignal(List<object> recursionChain = null)
        {
            if (currentSource == null)
                return null;
            if (recursionChain == null)
                recursionChain = new List<object>();
            recursionChain.Add(this);
            return currentSource?.GetRegisteredSourceSignal(recursionChain);
        }

        public event RegisteredSourceSignalChangedDelegate RegisteredSourceSignalChanged;
        public ISignalSourceRegistered RegisteredSourceSignal => GetRegisteredSourceSignal();

        public string GetRegisteredSourceSignalName(List<object> recursionChain = null)
        {
            if (currentSource == null)
                return null;
            if (recursionChain == null)
                recursionChain = new List<object>();
            recursionChain.Add(this);
            return currentSource.GetRegisteredSourceSignalName(recursionChain);
        }

        public IBidirectionalSignalTally RedTally { get; protected set; }
        public IBidirectionalSignalTally YellowTally { get; protected set; }
        public IBidirectionalSignalTally GreenTally { get; protected set; }

        private void createTallies()
        {
            RedTally = new BidirectionalPassthroughSignalTally(this);
            YellowTally = new BidirectionalPassthroughSignalTally(this);
            GreenTally = new BidirectionalPassthroughSignalTally(this);
        }
        #endregion

        #region ISignalDestination interface
        private ISignalSource currentSource = null;

        public ISignalSource CurrentSource => currentSource;

        public void AssignSource(ISignalSource source)
        {

            ISignalSource oldSource = currentSource;
            if (currentSource != null)
            {
                currentSource.RegisteredSourceSignalChanged -= sourcesRegisteredSourceSignalChanged;
                currentSource.RegisteredSourceSignalNameChanged -= sourcesRegisteredSourceSignalNameChanged;
            }

            currentSource = source;
            if (currentSource != null)
            {
                currentSource.RegisteredSourceSignalChanged += sourcesRegisteredSourceSignalChanged;
                currentSource.RegisteredSourceSignalNameChanged += sourcesRegisteredSourceSignalNameChanged;
            }

            ISignalSourceRegistered currentRegisteredSourceSignal = currentSource?.RegisteredSourceSignal;
            ISignalSourceRegistered oldRegisteredSourceSignal = oldSource?.RegisteredSourceSignal;
            if (currentRegisteredSourceSignal != oldRegisteredSourceSignal)
                RegisteredSourceSignalChanged?.Invoke(this, currentRegisteredSourceSignal);

            string currentRegisteredSourceSignalName = currentRegisteredSourceSignal?.RegisteredSourceSignalName;
            string oldRegisteredSourceSignalName = oldRegisteredSourceSignal?.RegisteredSourceSignalName;
            if (currentRegisteredSourceSignalName?.Equals(currentRegisteredSourceSignalName) != true)
                RegisteredSourceSignalNameChanged?.Invoke(this, currentRegisteredSourceSignalName);

        }

        private void sourcesRegisteredSourceSignalChanged(ISignalSource signal, ISignalSourceRegistered registeredSignal)
            => RegisteredSourceSignalChanged?.Invoke(this, registeredSignal);

        private void sourcesRegisteredSourceSignalNameChanged(ISignalSource signal, string newName)
            => RegisteredSourceSignalNameChanged?.Invoke(this, newName);
        #endregion

    }

}
