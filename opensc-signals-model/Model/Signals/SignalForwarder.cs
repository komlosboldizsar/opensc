﻿using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{
    public abstract class SignalForwarder : ObjectBase, ISignalSource, ISignalDestination
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

        public string GetRegisteredSourceSignalName(List<object> recursionChain = null)
        {
            if (currentSource == null)
                return null;
            if (recursionChain?.Contains(this) == true)
                return null;
            return currentSource.GetRegisteredSourceSignalName(recursionChain.ExtendRecursionChain(this));
        }

        public event RegisteredSourceSignalChangedDelegate RegisteredSourceSignalChanged;
        public ISignalSourceRegistered RegisteredSourceSignal => GetRegisteredSourceSignal();

        public ISignalSourceRegistered GetRegisteredSourceSignal(List<object> recursionChain = null)
        {
            if (currentSource == null)
                return null;
            if (recursionChain?.Contains(this) == true)
                return null;
            return currentSource?.GetRegisteredSourceSignal(recursionChain.ExtendRecursionChain(this));
        }

        public IBidirectionalSignalTally RedTally { get; protected set; }
        public IBidirectionalSignalTally YellowTally { get; protected set; }
        public IBidirectionalSignalTally GreenTally { get; protected set; }

        private void createTallies()
        {
            RedTally = new BidirectionalPassthroughSignalTally(this, SignalTallyColor.Red);
            YellowTally = new BidirectionalPassthroughSignalTally(this, SignalTallyColor.Yellow);
            GreenTally = new BidirectionalPassthroughSignalTally(this, SignalTallyColor.Green);
        }
        #endregion

        #region ISignalDestination interface
        private ISignalSource currentSource = null;

        public ISignalSource CurrentSource => currentSource;

        public event CurrentSourceChangedDelegate CurrentSourceChanged;

        public virtual void AssignSource(ISignalSource source)
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

            CurrentSourceChanged?.Invoke(this, source);

            ISignalSourceRegistered currentRegisteredSourceSignal = currentSource?.RegisteredSourceSignal;
            ISignalSourceRegistered oldRegisteredSourceSignal = oldSource?.RegisteredSourceSignal;
            if (currentRegisteredSourceSignal != oldRegisteredSourceSignal)
                RegisteredSourceSignalChanged?.Invoke(this, currentRegisteredSourceSignal, RecursionChainHelpers.CreateRecursionChain(this));

            string currentRegisteredSourceSignalName = currentRegisteredSourceSignal?.RegisteredSourceSignalName;
            string oldRegisteredSourceSignalName = oldRegisteredSourceSignal?.RegisteredSourceSignalName;
            if (!string.Equals(currentRegisteredSourceSignalName, oldRegisteredSourceSignalName))
                RegisteredSourceSignalNameChanged?.Invoke(this, currentRegisteredSourceSignalName, RecursionChainHelpers.CreateRecursionChain(this));

            ((BidirectionalPassthroughSignalTally)RedTally).PreviousElement = source?.RedTally;
            ((BidirectionalPassthroughSignalTally)YellowTally).PreviousElement = source?.YellowTally;
            ((BidirectionalPassthroughSignalTally)GreenTally).PreviousElement = source?.GreenTally;

        }

        private void sourcesRegisteredSourceSignalChanged(ISignalSource signal, ISignalSourceRegistered registeredSignal, List<object> recursionChain = null)
        {
            if (recursionChain?.Contains(this) == true)
                return;
            RegisteredSourceSignalChanged?.Invoke(this, registeredSignal, recursionChain.ExtendRecursionChain(this));
        }

        private void sourcesRegisteredSourceSignalNameChanged(ISignalSource signal, string newName, List<object> recursionChain = null)
        {
            if (recursionChain?.Contains(this) == true)
                return;
            RegisteredSourceSignalNameChanged?.Invoke(this, newName, recursionChain.ExtendRecursionChain(this));
        }
        #endregion

        #region ISystemObject interface (abstract)
        public abstract string GlobalID { get; }
        public abstract event PropertyChangedTwoValuesDelegate<ISystemObject, string> GlobalIdChanged;
        #endregion

    }

}
