using Microsoft.CodeAnalysis;
using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public abstract partial class Router : ModelBase
    {

        public const string LOG_TAG = "Router";

        #region Persistence, instantiation
        public Router()
        {
            Inputs = createInputCollection();
            Outputs = createOutputCollection();
        }

        public override void Removed()
        {
            base.Removed();
            StateChanged = null;
            StateStringChanged = null;
            Inputs.ParentRemoved();
            Outputs.ParentRemoved();
        }

        protected override void afterUpdate()
        {
            base.afterUpdate();
            RouterDatabase.Instance.ItemUpdated(this);
        }
        #endregion

        #region Restoration
        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            notifyIOsRestored();
        }

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            notifyIOsTotallyRestored();
            queryAllStates();
        }

        public override void RestoreCustomRelations()
        {
            base.RestoreCustomRelations();
            Inputs.RestoreSources();
        }

        private void notifyIOsRestored()
            => Outputs.Foreach(o => o.Restored());

        private void notifyIOsTotallyRestored()
            => Outputs.Foreach(o => o.TotallyRestored());
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = RouterDatabase.Instance;
        #endregion

        #region Inputs
        [PersistAs("inputs")]
        [PersistAs("input", 1, "index")]
        [PersistDetailed]
        [PersistSubclass(nameof(getInputCollectionType))]
        public readonly RouterInputCollection Inputs;

        protected virtual Type getInputCollectionType() => typeof(RouterInputCollection);
        protected virtual RouterInputCollection createInputCollection() => new(this);
        #endregion

        #region Outputs
        [PersistAs("outputs")]
        [PersistAs("output", 1, "index")]
        [PersistDetailed]
        [PersistSubclass(nameof(getOutputCollectionType))]
        public readonly RouterOutputCollection Outputs;

        protected virtual Type getOutputCollectionType() => typeof(RouterOutputCollection);
        protected virtual RouterOutputCollection createOutputCollection() => new(this);
        #endregion

        protected abstract void queryAllStates();

        #region Crosspoint update
        public void RequestCrosspointUpdate(RouterOutput output, RouterInput input)
        {
            if (!Outputs.Contains(output))
                throw new ArgumentException();
            string logMessage = $"Router single crosspoint update request. Router: [{this}], destination: {output}, source: {input}.";
            LogDispatcher.I(LOG_TAG, logMessage);
            requestCrosspointUpdateImpl(output, input);
        }

        public void RequestCrosspointUpdates(IEnumerable<RouterCrosspoint> crosspoints)
        {
            List<string> logMessageDetailsPieces = new List<string>();
            foreach (RouterCrosspoint crosspoint in crosspoints)
            {
                logMessageDetailsPieces.Add($"[destination: {crosspoint.Output}, source: {crosspoint.Input}]");
                if (!Outputs.Contains(crosspoint.Output))
                    throw new ArgumentException();
            }
            string logMessageDetails = string.Join(", ", logMessageDetailsPieces);
            string logMessage = $"Router multi crosspoint update request. Router: [{this}], crosspoints: [{logMessageDetails}].";
            LogDispatcher.I(LOG_TAG, logMessage);
            requestCrosspointUpdatesImpl(crosspoints);
        }

        protected abstract void requestCrosspointUpdateImpl(RouterOutput output, RouterInput input);
        protected abstract void requestCrosspointUpdatesImpl(IEnumerable<RouterCrosspoint> crosspoints);

        public delegate void CrosspointChangedDelegate(Router router, RouterOutput output, RouterInput newInput);
        public event CrosspointChangedDelegate CrosspointChanged;

        protected void notifyCrosspointChanged(RouterOutput output, RouterInput input)
        {
            if (!Outputs.Contains(output))
                throw new ArgumentException();
            output.AssignSource(input);
            CrosspointChanged?.Invoke(this, output, input);
        }

        protected void notifyCrosspointChanged(int outputIndex, int inputIndex)
        {
            RouterOutput output = Outputs[outputIndex];
            if (output == null)
                throw new ArgumentException();
            RouterInput input = Inputs[inputIndex];
            if (output == null)
                throw new ArgumentException();
            notifyCrosspointChanged(output, input);
        }
        #endregion

        #region Locks and protects update
        public void RequestLockOperation(RouterOutput output, RouterOutputLockType lockType, RouterOutputLockOperationType lockOperationType)
        {
            if (!Outputs.Contains(output))
                throw new ArgumentException();
            string logMessage = $"Router output {lockType.GetDoString(lockOperationType, false)} request. Router: [{this}], destination: [{output}].";
            LogDispatcher.I(LOG_TAG, logMessage);
            requestLockOperationImpl(output, lockType, lockOperationType);
        }

        protected abstract void requestLockOperationImpl(RouterOutput output, RouterOutputLockType lockType, RouterOutputLockOperationType lockOperationType);

        protected void notifyLockChanged(RouterOutput output, RouterOutputLockType lockType, RouterOutputLockState lockState)
        {
            if (!Outputs.Contains(output))
                throw new ArgumentException();
            output.GetLock(lockType).StateUpdateFromRouter(lockState);
        }

        protected void notifyLockChanged(int outputIndex, RouterOutputLockType lockType, RouterOutputLockState lockState)
        {
            RouterOutput output = Outputs[outputIndex];
            if (output != null)
                notifyLockChanged(output, lockType, lockState);
        }
        #endregion

        #region Property: State
        [AutoProperty(SetterAccessibility = Accessibility.Protected)]
        private RouterState state = RouterState.Unknown;
        #endregion

        #region Property: StateString
        [AutoProperty(SetterAccessibility = Accessibility.Protected)]
        private string stateString = "?";
        #endregion

        #region Names
        #region Info properties: inputs
        public virtual bool CanSetRemoteInputNames { get; } = false;
        public virtual bool CanGetRemoteInputNames { get; } = false;
        public virtual bool CanGetRemoteInputNameChangeNotifications { get; } = false;
        #endregion

        #region Property: ExportInputNamesOnLocalUpdate
        [AutoProperty]
        [PersistAs("inputs/@export_names")]
        private bool exportInputNamesOnLocalUpdate;
        #endregion

        #region Property: ImportInputNamesOnRemoteChange
        [AutoProperty]
        [PersistAs("inputs/@import_names")]
        private bool importInputNamesOnRemoteUpdate;
        #endregion

        public void DoImportInputNames()
        {
            if (!CanGetRemoteInputNames)
                throw new InvalidOperationException();
            Dictionary<RouterInput, string> inputNameDictionary = getRemoteInputNames(Inputs);
            foreach (KeyValuePair<RouterInput, string> inputNameKVP in inputNameDictionary)
                inputNameKVP.Key.Name = inputNameKVP.Value;
        }

        public void DoExportInputNames()
        {
            if (!CanSetRemoteInputNames)
                throw new InvalidOperationException();
            Dictionary<RouterInput, string> inputNameDictionary = new();
            foreach (RouterInput input in Inputs)
                inputNameDictionary.Add(input, input.Name);
            setRemoteInputNames(inputNameDictionary);
        }

        protected virtual string getRemoteInputName(RouterInput input) => throw new NotImplementedException();
        protected virtual Dictionary<RouterInput, string> getRemoteInputNames(IEnumerable<RouterInput> inputs) => throw new NotImplementedException();
        protected virtual void setRemoteInputName(RouterInput input, string name) => throw new NotImplementedException();
        protected virtual void setRemoteInputNames(Dictionary<RouterInput, string> names) => throw new NotImplementedException();

        protected void notifyRemoteInputNameChanged(RouterInput input, string newName)
        {
            if (!Inputs.Contains(input))
                throw new ArgumentException();
            if (importInputNamesOnRemoteUpdate)
                input.Name = newName;
        }

        protected void notifyRemoteInputNameChanged(int inputIndex, string newName)
        {
            RouterInput input = Inputs[inputIndex];
            if (input == null)
                throw new ArgumentException();
            notifyRemoteInputNameChanged(input, newName);
        }

        internal protected void NotifyLocalInputNameChanged(RouterInput input)
        {
            if (exportInputNamesOnLocalUpdate)
                setRemoteInputName(input, input.Name);
        }

        #region Info properties: outputs
        public virtual bool CanSetRemoteOutputNames { get; } = false;
        public virtual bool CanGetRemoteOutputNames { get; } = false;
        public virtual bool CanGetRemoteOutputNameChangeNotifications { get; } = false;
        #endregion

        #region Property: ExportOutputNamesOnLocalUpdate
        [AutoProperty]
        [PersistAs("outputs/@export_names")]
        private bool exportOutputNamesOnLocalUpdate;
        #endregion

        #region Property: ImportOutputNamesOnRemoteUpdate
        [AutoProperty]
        [PersistAs("outputs/@import_names")]
        private bool importOutputNamesOnRemoteUpdate;
        #endregion

        public void DoImportOutputNames()
        {
            if (!CanGetRemoteOutputNames)
                throw new InvalidOperationException();
            Dictionary<RouterOutput, string> outputNameDictionary = getRemoteOutputNames(Outputs);
            foreach (KeyValuePair<RouterOutput, string> outputNameKVP in outputNameDictionary)
                outputNameKVP.Key.Name = outputNameKVP.Value;
        }

        public void DoExportOutputNames()
        {
            if (!CanSetRemoteOutputNames)
                throw new InvalidOperationException();
            Dictionary<RouterOutput, string> outputNameDictionary = new();
            foreach (RouterOutput output in Outputs)
                outputNameDictionary.Add(output, output.Name);
            setRemoteOutputNames(outputNameDictionary);
        }

        protected virtual string getRemoteOutputName(RouterOutput output) => throw new NotImplementedException();
        protected virtual Dictionary<RouterOutput, string> getRemoteOutputNames(IEnumerable<RouterOutput> outputs) => throw new NotImplementedException();
        protected virtual void setRemoteOutputName(RouterOutput output, string name) => throw new NotImplementedException();
        protected virtual void setRemoteOutputNames(Dictionary<RouterOutput, string> names) => throw new NotImplementedException();

        protected void notifyRemoteOutputNameChanged(RouterOutput output, string newName)
        {
            if (!Outputs.Contains(output))
                throw new ArgumentException();
            if (importOutputNamesOnRemoteUpdate)
                output.Name = newName;
        }

        protected void notifyRemoteOutputNameChanged(int outputIndex, string newName)
        {
            RouterOutput output = Outputs[outputIndex];
            if (output == null)
                throw new ArgumentException();
            notifyRemoteOutputNameChanged(output, newName);
        }

        internal protected void NotifyLocalOutputNameChanged(RouterOutput output)
        {
            if (exportOutputNamesOnLocalUpdate)
                setRemoteOutputName(output, output.Name);
        }
        #endregion

    }

}
