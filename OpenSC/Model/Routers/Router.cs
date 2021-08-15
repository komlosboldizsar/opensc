using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.Routers.Triggers;
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
            inputs.ItemsChanged += inputsChangedHandler;
            outputs.ItemsChanged += outputsChangedHandler;
        }

        public override void Removed()
        {
            base.Removed();
            NameChanged = null;
            InputsChanged = null;
            OutputsChanged = null;
            StateChanged = null;
            StateStringChanged = null;
            Inputs.ForEach(i => i.RemovedFromRouter(this));
            inputs.Clear();
            Outputs.ForEach(o => o.RemovedFromRouter(this));
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
            inputs.ForEach(i => i.Restored());
            outputs.ForEach(o => o.Restored());
        }

        private void notifyIOsTotallyRestored()
        {
            inputs.ForEach(i => i.TotallyRestored());
            outputs.ForEach(o => o.TotallyRestored());
        }
        #endregion

        #region ID validation
        protected override void validateIdForDatabase(int id)
        {
            if (!RouterDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Name
        public event PropertyChangedTwoValuesDelegate<Router, string> NameChanged;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get => name;
            set => setProperty(this, ref name, value, NameChanged, validator: ValidateName);
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
        }
        #endregion

        #region Inputs
        private ObservableList<RouterInput> inputs = new ObservableList<RouterInput>();
        public ObservableList<RouterInput> Inputs => inputs;

        [PersistAs("inputs")]
        [PersistAs(null, 1)]
        [PolymorphField(nameof(InputTypesDictionaryGetter))]
        private RouterInput[] _inputs // for persistence
        {
            get { return inputs.ToArray(); }
            set
            {
                inputs.Clear();
                if (value != null)
                    inputs.AddRange(value);
                inputs.ForEach(i => i.AssignParentRouter(this));
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

        private void inputsChangedHandler()
        {
            InputsChanged?.Invoke(this);
            RaisePropertyChanged(nameof(Inputs));
        }

        public PropertyChangedNoValueDelegate<Router> InputsChanged;

        private void restoreInputSources() => inputs.ForEach(i => i.RestoreSource());

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
        [PolymorphField(nameof(OutputTypesDictionaryGetter))]
        private RouterOutput[] _outputs // for persistence
        {
            get { return outputs.ToArray(); }
            set
            {
                outputs.Clear();
                if (value != null)
                    outputs.AddRange(value);
                outputs.ForEach(o => o.AssignParentRouter(this));
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

        private void outputsChangedHandler()
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
            string logMessage = string.Format("Router crosspoint update request. Router: [(#{0}) #1], destination: {2}, source: {3}.",
                ID, Name, output.Index, input.Index);
            LogDispatcher.I(LOG_TAG, logMessage);
            requestCrosspointUpdateImpl(output, input);
        }

        protected abstract void requestCrosspointUpdateImpl(RouterOutput output, RouterInput input);

        protected void notifyCrosspointChanged(RouterOutput output, RouterInput input)
        {
            if (!outputs.Contains(output))
                throw new ArgumentException();
            output.AssignSource(input);
            RouterMacroTriggers.RouterCrosspointChanged.Call(this);
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
            string logMessage = string.Format("Router output {0} request. Router: [(#{1}) #2], destination: {3}.",
                translateLockOperation(lockType, lockOperationType), ID, Name, output.Index);
            LogDispatcher.I(LOG_TAG, logMessage);
            requestLockOperationImpl(output, lockType, lockOperationType);
        }

        private static Dictionary<RouterOutputLockType, string> LOCK_TYPE_TRANSLATIONS = new Dictionary<RouterOutputLockType, string>()
        {
            { RouterOutputLockType.Lock, "lock" },
            { RouterOutputLockType.Protect, "protect" },
        };

        private static Dictionary<RouterOutputLockOperationType, string> LOCK_OPERATION_TYPE_TRANSLATIONS = new Dictionary<RouterOutputLockOperationType, string>()
        {
            { RouterOutputLockOperationType.Lock, "{0}" },
            { RouterOutputLockOperationType.Unlock, "un{0}" },
            { RouterOutputLockOperationType.ForceUnlock, "force un{0}" },
        };

        private static string translateLockOperation(RouterOutputLockType lockType, RouterOutputLockOperationType lockOperationType)
            => string.Format(LOCK_OPERATION_TYPE_TRANSLATIONS[lockOperationType], LOCK_TYPE_TRANSLATIONS[lockType]);

        protected abstract void requestLockOperationImpl(RouterOutput output, RouterOutputLockType lockType, RouterOutputLockOperationType lockOperationType);

        protected void notifyLockChanged(RouterOutput output, RouterOutputLockType lockType, RouterOutputLockState lockState)
        {
            if (!outputs.Contains(output))
                throw new ArgumentException();
            switch (lockType)
            {
                case RouterOutputLockType.Lock:
                    output.LockStateUpdateFromRouter(lockState);
                    break;
                case RouterOutputLockType.Protect:
                    output.ProtectStateUpdateFromRouter(lockState);
                    break;
            }
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
            protected set => setProperty(this, ref state, value, StateChanged);
        }
        #endregion

        #region Property: StateString
        public event PropertyChangedTwoValuesDelegate<Router, string> StateStringChanged;

        private string stateString = "?";

        public string StateString
        {
            get => stateString;
            protected set => setProperty(this, ref stateString, value, StateStringChanged);
        }
        #endregion

    }

}
