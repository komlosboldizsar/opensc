using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.GpioInterfaces
{

    public abstract class GpioInterface : ModelBase
    {

        public const string LOG_TAG = "GpioInterface";

        #region Instantiation, persistence, restoration
        public GpioInterface()
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
            Inputs.Foreach(i => i.RemovedFromGpioInterface(this));
            inputs.Clear();
            Outputs.Foreach(o => o.RemovedFromGpioInterface(this));
            outputs.Clear();
        }

        protected override void afterUpdate()
        {
            base.afterUpdate();
            GpioInterfaceDatabase.Instance.ItemUpdated(this);
            queryAllInputStates();
            sendAllOutputStates();
        }

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            queryAllInputStates();
            sendAllOutputStates();
        }

        public override void RestoreCustomRelations()
        {
            base.RestoreCustomRelations();
            restoreOutputDrivers();
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = GpioInterfaceDatabase.Instance;
        #endregion

        #region Inputs
        private ObservableList<GpioInterfaceInput> inputs = new ObservableList<GpioInterfaceInput>();
        public ObservableList<GpioInterfaceInput> Inputs => inputs;

        [PersistAs("inputs")]
        [PersistAs(null, 1)]
        [PersistDetailed]
        [PolymorphField(nameof(InputTypesDictionaryGetter))]
        private GpioInterfaceInput[] _inputs // for persistence
        {
            get => inputs.ToArray();
            set
            {
                inputs.Clear();
                if (value != null)
                    inputs.AddRange(value);
                inputs.Foreach(i => i.AssignParentGpioInterface(this));
            }
        }

        public void AddInput()
        {
            int index = (inputs.Count > 0) ? (inputs.Max(ri => ri.Index) + 1) : 0;
            inputs.Add(CreateInput(string.Format("Input #{0}", index + 1), index));
        }

        public void RemoveInput(GpioInterfaceInput input)
        {
            inputs.Remove(input);
            input.RemovedFromGpioInterface(this);
        }

        public GpioInterfaceInput GetInput(int index) => inputs.FirstOrDefault(ri => (ri.Index == index));

        private void inputsChangedHandler(IEnumerable<IObservableEnumerable<GpioInterfaceInput>.ItemWithPosition> affectedItemsWithPositions)
        {
            InputsChanged?.Invoke(this);
            RaisePropertyChanged(nameof(Inputs));
        }

        public PropertyChangedNoValueDelegate<GpioInterface> InputsChanged;

        public abstract GpioInterfaceInput CreateInput(string name, int index);

        private static readonly Dictionary<Type, string> INPUT_TYPES = new Dictionary<Type, string>()
        {
            {  typeof(GpioInterfaceInput), "standard" }
        };

        protected virtual Dictionary<Type, string> InputTypesDictionaryGetter() => INPUT_TYPES;

        protected virtual void queryAllInputStates() => inputs.Foreach(i => i.QueryState());
        #endregion

        #region Outputs
        private ObservableList<GpioInterfaceOutput> outputs = new ObservableList<GpioInterfaceOutput>();
        public ObservableList<GpioInterfaceOutput> Outputs => outputs;

        [PersistAs("outputs")]
        [PersistAs(null, 1)]
        [PersistDetailed]
        [PolymorphField(nameof(OutputTypesDictionaryGetter))]
        private GpioInterfaceOutput[] _outputs // for persistence
        {
            get => outputs.ToArray();
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

        public void RemoveOutput(GpioInterfaceOutput output)
        {
            outputs.Remove(output);
            output.RemovedFromGpioInterface(this);
        }

        public GpioInterfaceOutput GetOutput(int index) => outputs.FirstOrDefault(ro => (ro.Index == index));

        private void outputsChangedHandler(IEnumerable<IObservableEnumerable<GpioInterfaceOutput>.ItemWithPosition> affectedItemsWithPositions)
        {
            OutputsChanged?.Invoke(this);
            RaisePropertyChanged(nameof(Outputs));
        }

        public event PropertyChangedNoValueDelegate<GpioInterface> OutputsChanged;

        private void restoreOutputDrivers() => outputs.Foreach(o => o.RestoreDriver());

        public abstract GpioInterfaceOutput CreateOutput(string name, int index);

        private static readonly Dictionary<Type, string> OUTPUT_TYPES = new Dictionary<Type, string>()
        {
            {  typeof(GpioInterfaceOutput), "standard" }
        };

        protected virtual Dictionary<Type, string> OutputTypesDictionaryGetter() => OUTPUT_TYPES;

        protected virtual void sendAllOutputStates() => outputs.Foreach(o => o.sendState());
        #endregion

        #region Property: State
        public event PropertyChangedTwoValuesDelegate<GpioInterface, GpioInterfaceState> StateChanged;

        private GpioInterfaceState state = GpioInterfaceState.Unknown;

        public GpioInterfaceState State
        {
            get => state;
            protected set => this.setProperty(ref state, value, StateChanged);
        }
        #endregion

        #region Property: StateString
        public event PropertyChangedTwoValuesDelegate<GpioInterface, string> StateStringChanged;

        private string stateString = "?";

        public string StateString
        {
            get => stateString;
            protected set => this.setProperty(ref stateString, value, StateStringChanged);
        }
        #endregion

    }

}
