using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public abstract class Router : ModelBase
    {

        public const string LOG_TAG = "Router";

        #region Persistence, instantiation
        public Router()
        {
            inputs.ItemsAdded += inputsChangedHandler;
            inputs.ItemsRemoved += inputsChangedHandler;
            outputs.ItemsAdded += outputsChangedHandler;
            outputs.ItemsRemoved += outputsChangedHandler;
        }

        public override void Removed()
        {
            base.Removed();
            InputsChanged = null;
            OutputsChanged = null;
            StateChanged = null;
            StateStringChanged = null;
            Inputs.Foreach(i => i.RemovedFromRouter(this));
            inputs.Clear();
            Outputs.Foreach(o => o.RemovedFromRouter(this));
            outputs.Clear();
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
            restoreInputSources();
        }

        private void notifyIOsRestored()
        {
            inputs.Foreach(i => i.Restored());
            outputs.Foreach(o => o.Restored());
        }

        private void notifyIOsTotallyRestored()
        {
            inputs.Foreach(i => i.TotallyRestored());
            outputs.Foreach(o => o.TotallyRestored());
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = RouterDatabase.Instance;
        #endregion

        #region Inputs
        private ObservableList<RouterInput> inputs = new ObservableList<RouterInput>();
        public ObservableList<RouterInput> Inputs => inputs;

        [PersistAs("inputs")]
        [PersistAs(null, 1)]
        [PersistDetailed]
        [PolymorphField(nameof(InputTypesDictionaryGetter))]
        private RouterInput[] _inputs // for persistence
        {
            get { return inputs.ToArray(); }
            set
            {
                inputs.Clear();
                if (value != null)
                    inputs.AddRange(value);
                inputs.Foreach(i => i.AssignParentRouter(this));
            }
        }

        public void AddInput()
        {
            int index = (inputs.Count > 0) ? (inputs.Max(ri => ri.Index) + 1) : 0;
            inputs.Add(CreateInput(string.Format("Input #{0}", index + 1), index));
        }

        public void RemoveInput(RouterInput input)
        {
            inputs.Remove(input);
            input.RemovedFromRouter(this);
        }

        public RouterInput GetInput(int index) => inputs.FirstOrDefault(ri => (ri.Index == index));

        private void inputsChangedHandler(IEnumerable<IObservableEnumerable<RouterInput>.ItemWithPosition> affectedItemsWithPositions)
        {
            InputsChanged?.Invoke(this);
            RaisePropertyChanged(nameof(Inputs));
        }

        public PropertyChangedNoValueDelegate<Router> InputsChanged;

        private void restoreInputSources() => inputs.Foreach(i => i.RestoreSource());

        public abstract RouterInput CreateInput(string name, int index);

        private static readonly Dictionary<Type, string> INPUT_TYPES = new Dictionary<Type, string>()
        {
            {  typeof(RouterInput), "standard" }
        };

        protected virtual Dictionary<Type, string> InputTypesDictionaryGetter() => INPUT_TYPES;
        #endregion

        #region Outputs
        private ObservableList<RouterOutput> outputs = new ObservableList<RouterOutput>();
        public ObservableList<RouterOutput> Outputs => outputs;

        [PersistAs("outputs")]
        [PersistAs(null, 1)]
        [PersistDetailed]
        [PolymorphField(nameof(OutputTypesDictionaryGetter))]
        private RouterOutput[] _outputs // for persistence
        {
            get { return outputs.ToArray(); }
            set
            {
                outputs.Clear();
                if (value != null)
                    outputs.AddRange(value);
                outputs.Foreach(o => o.AssignParentRouter(this));
            }
        }

        public void AddOutput()
        {
            int index = (outputs.Count > 0) ? (outputs.Max(ro => ro.Index) + 1) : 0;
            outputs.Add(CreateOutput(string.Format("Output #{0}", index + 1), index));
        }

        public void RemoveOutput(RouterOutput output)
        {
            outputs.Remove(output);
            output.RemovedFromRouter(this);
        }

        public RouterOutput GetOutput(int index) => outputs.FirstOrDefault(ro => (ro.Index == index));

        private void outputsChangedHandler(IEnumerable<IObservableEnumerable<RouterOutput>.ItemWithPosition> affectedItemsWithPositions)
        {
            OutputsChanged?.Invoke(this);
            RaisePropertyChanged(nameof(Outputs));
        }

        public event PropertyChangedNoValueDelegate<Router> OutputsChanged;

        public abstract RouterOutput CreateOutput(string name, int index);

        private static readonly Dictionary<Type, string> OUTPUT_TYPES = new Dictionary<Type, string>()
        {
            {  typeof(RouterOutput), "standard" }
        };

        protected virtual Dictionary<Type, string> OutputTypesDictionaryGetter() => OUTPUT_TYPES;
        #endregion

        protected abstract void queryAllStates();

        #region Crosspoint update
        public void RequestCrosspointUpdate(RouterOutput output, RouterInput input)
        {
            if (!outputs.Contains(output))
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
                if (!outputs.Contains(crosspoint.Output))
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
            if (!outputs.Contains(output))
                throw new ArgumentException();
            output.AssignSource(input);
            CrosspointChanged?.Invoke(this, output, input);
        }

        protected void notifyCrosspointChanged(int outputIndex, int inputIndex)
        {
            RouterOutput output = outputs.FirstOrDefault(ro => (ro.Index == outputIndex));
            if (output == null)
                throw new ArgumentException();
            RouterInput input = inputs.FirstOrDefault(ri => (ri.Index == inputIndex));
            if (output == null)
                throw new ArgumentException();
            notifyCrosspointChanged(output, input);
        }
        #endregion

        #region Locks and protects update
        public void RequestLockOperation(RouterOutput output, RouterOutputLockType lockType, RouterOutputLockOperationType lockOperationType)
        {
            if (!outputs.Contains(output))
                throw new ArgumentException();
            string logMessage = $"Router output {lockType.GetDoString(lockOperationType, false)} request. Router: [{this}], destination: [{output}].";
            LogDispatcher.I(LOG_TAG, logMessage);
            requestLockOperationImpl(output, lockType, lockOperationType);
        }

        protected abstract void requestLockOperationImpl(RouterOutput output, RouterOutputLockType lockType, RouterOutputLockOperationType lockOperationType);

        protected void notifyLockChanged(RouterOutput output, RouterOutputLockType lockType, RouterOutputLockState lockState)
        {
            if (!outputs.Contains(output))
                throw new ArgumentException();
            output.GetLock(lockType).StateUpdateFromRouter(lockState);
        }

        protected void notifyLockChanged(int outputIndex, RouterOutputLockType lockType, RouterOutputLockState lockState)
        {
            RouterOutput output = outputs.FirstOrDefault(ro => (ro.Index == outputIndex));
            notifyLockChanged(outputs[outputIndex], lockType, lockState);
        }
        #endregion

        #region Property: State
        public event PropertyChangedTwoValuesDelegate<Router, RouterState> StateChanged;

        private RouterState state = RouterState.Unknown;

        public RouterState State
        {
            get => state;
            protected set => this.setProperty(ref state, value, StateChanged);
        }
        #endregion

        #region Property: StateString
        public event PropertyChangedTwoValuesDelegate<Router, string> StateStringChanged;

        private string stateString = "?";

        public string StateString
        {
            get => stateString;
            protected set => this.setProperty(ref stateString, value, StateStringChanged);
        }
        #endregion

        #region Names
        #region Info properties: inputs
        public virtual bool CanSetRemoteInputNames { get; } = false;
        public virtual bool CanGetRemoteInputNames { get; } = false;
        public virtual bool CanGetRemoteInputNameChangeNotifications { get; } = false;
        #endregion

        #region Property: ExportInputNamesOnLocalUpdate
        public event PropertyChangedTwoValuesDelegate<Router, bool> ExportInputNamesOnLocalUpdateChanged;

        [PersistAs("export_input_names")]
        private bool exportInputNamesOnLocalUpdate;

        public bool ExportInputNamesOnLocalUpdate
        {
            get => exportInputNamesOnLocalUpdate;
            set => this.setProperty(ref exportInputNamesOnLocalUpdate, value, ExportInputNamesOnLocalUpdateChanged);
        }
        #endregion

        #region Property: ImportInputNamesOnRemoteChange
        public event PropertyChangedTwoValuesDelegate<Router, bool> ImportInputNamesOnRemoteUpdateChanged;

        [PersistAs("import_input_names")]
        private bool importInputNamesOnRemoteUpdate;

        public bool ImportInputNamesOnRemoteUpdate
        {
            get => importInputNamesOnRemoteUpdate;
            set => this.setProperty(ref importInputNamesOnRemoteUpdate, value, ImportInputNamesOnRemoteUpdateChanged);
        }
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
            if (inputs.Contains(input))
                throw new ArgumentException();
            if (importInputNamesOnRemoteUpdate)
                input.Name = newName;
        }

        protected void notifyRemoteInputNameChanged(int inputIndex, string newName)
        {
            RouterInput input = inputs.FirstOrDefault(ri => (ri.Index == inputIndex));
            if (input == null)
                throw new ArgumentException();
            notifyRemoteInputNameChanged(input, newName);
        }

        #region Info properties: outputs
        public virtual bool CanSetRemoteOutputNames { get; } = false;
        public virtual bool CanGetRemoteOutputNames { get; } = false;
        public virtual bool CanGetRemoteOutputNameChangeNotifications { get; } = false;
        #endregion

        #region Property: ExportOutputNamesOnLocalUpdate
        public event PropertyChangedTwoValuesDelegate<Router, bool> ExportOutputNamesOnLocalUpdateChanged;

        [PersistAs("export_output_names")]
        private bool exportOutputNamesOnLocalUpdate;

        public bool ExportOutputNamesOnLocalUpdate
        {
            get => exportOutputNamesOnLocalUpdate;
            set => this.setProperty(ref exportOutputNamesOnLocalUpdate, value, ExportOutputNamesOnLocalUpdateChanged);
        }
        #endregion

        #region Property: ImportOutputNamesOnRemoteUpdate
        public event PropertyChangedTwoValuesDelegate<Router, bool> importOutputNamesOnRemoteUpdateChanged;

        [PersistAs("import_output_names")]
        private bool importOutputNamesOnRemoteUpdate;

        public bool ImportOutputNamesOnRemoteUpdate
        {
            get => importOutputNamesOnRemoteUpdate;
            set => this.setProperty(ref importOutputNamesOnRemoteUpdate, value, importOutputNamesOnRemoteUpdateChanged);
        }
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
            if (outputs.Contains(output))
                throw new ArgumentException();
            if (importOutputNamesOnRemoteUpdate)
                output.Name = newName;
        }

        protected void notifyRemoteOutputNameChanged(int outputIndex, string newName)
        {
            RouterOutput output = outputs.FirstOrDefault(ro => (ro.Index == outputIndex));
            if (output == null)
                throw new ArgumentException();
            notifyRemoteOutputNameChanged(output, newName);
        }
        #endregion

    }

}
