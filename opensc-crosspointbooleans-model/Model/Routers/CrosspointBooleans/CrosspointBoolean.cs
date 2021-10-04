using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.CrosspointBooleans
{

    public class CrosspointBoolean : ModelBase
    {

        public const string LOG_TAG = "CrosspointBoolean";

        #region Persistence, instantiation
        public CrosspointBoolean()
        { }

        public override void Removed()
        {
            base.Removed();
            WatchedInputChanged = null;
            WatchedOutputChanged = null;
        }
        #endregion

        #region Restoration
        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            cpActiveBoolean = new CrosspointActiveBoolean(this);
        }

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            restoreWatchedInput();
            restoreWatchedOutput();
        }
        #endregion

        #region ID validation
        protected override void validateIdForDatabase(int id)
        {
            if (!CrosspointBooleanDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = CrosspointBooleanDatabase.Instance;
        #endregion

        #region Property: WatchedRouter
        public event PropertyChangedTwoValuesDelegate<CrosspointBoolean, Router> WatchedRouterChanged;

        private Router watchedRouter;

        public Router WatchedRouter
        {
            get => watchedRouter;
            set => setProperty(this, ref watchedRouter, value, WatchedRouterChanged);
        }

        private void updateWatchedRouter()
        {
            Router wir = watchedInput?.Router;
            Router wor = watchedOutput?.Router;
            WatchedRouter = (wir == wor) ? wor : null;
        }
        #endregion

        #region Property: WatchedInput
        public event PropertyChangedTwoValuesDelegate<CrosspointBoolean, RouterInput> WatchedInputChanged;

        private string __watchedInputId; // "Temp foreign key"

        [PersistAs("watched_input")]
        private string _watchedInputId
        {
            get => (watchedInput != null) ? string.Format("router.{0}.input.{1}", watchedInput.Router.ID, watchedInput.Index) : null;
            set { __watchedInputId = value; }
        }

        private RouterInput watchedInput;

        public RouterInput WatchedInput
        {
            get => watchedInput;
            set
            {
                setProperty(this, ref watchedInput, value, WatchedInputChanged, null,
                    (ov, nv) => {
                        updateWatchedRouter();
                        updateIsValid();
                    });
            }
        }

        private void restoreWatchedInput()
        {
            string[] watchedInputIdParts = __watchedInputId?.Split('.');
            if (watchedInputIdParts?.Length != 4)
                return;
            if ((watchedInputIdParts[0] != "router") || (watchedInputIdParts[2] != "input"))
                return;
            if (!int.TryParse(watchedInputIdParts[1], out int storedInputRouterId))
                return;
            if (!int.TryParse(watchedInputIdParts[3], out int storedInputIndex))
                return;
            WatchedInput = RouterDatabase.Instance.GetTById(storedInputRouterId)?.GetInput(storedInputIndex);
        }
        #endregion

        #region Property: WatchedOutput
        public event PropertyChangedTwoValuesDelegate<CrosspointBoolean, RouterOutput> WatchedOutputChanged;

        private string __watchedOutputId; // "Temp foreign key"

        [PersistAs("watched_output")]
        private string _watchedOutputId
        {
            get => (watchedOutput != null) ? string.Format("router.{0}.output.{1}", watchedOutput.Router.ID, watchedInput.Index) : null;
            set { __watchedOutputId = value; }
        }

        private RouterOutput watchedOutput;

        public RouterOutput WatchedOutput
        {
            get => watchedOutput;
            set
            {
                setProperty(this, ref watchedOutput, value, WatchedOutputChanged, null,
                    (ov, nv)=> {
                        updateWatchedRouter();
                        updateIsValid();
                    });
            }
        }

        public Color Color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool CurrentState => throw new NotImplementedException();

        private void restoreWatchedOutput()
        {
            string[] watchedOutputIdParts = __watchedOutputId?.Split('.');
            if (watchedOutputIdParts?.Length != 4)
                return;
            if ((watchedOutputIdParts[0] != "router") || (watchedOutputIdParts[2] != "output"))
                return;
            if (!int.TryParse(watchedOutputIdParts[1], out int storedOutputRouterId))
                return;
            if (!int.TryParse(watchedOutputIdParts[3], out int storedOutputIndex))
                return;
            WatchedOutput = RouterDatabase.Instance.GetTById(storedOutputRouterId)?.GetOutput(storedOutputIndex);
        }
        #endregion

        #region Property: IsValid
        public event PropertyChangedTwoValuesDelegate<CrosspointBoolean, bool> IsValidChanged;

        private bool isValid;

        public bool IsValid
        {
            get => isValid;
            set => setProperty(this, ref isValid, value, IsValidChanged);
        }

        private void updateIsValid()
            => IsValid = ((watchedRouter != null) && (watchedInput != null) && (watchedOutput != null));
        #endregion

        #region CrosspointActiveBoolean class, IBoolean implementation
        private CrosspointActiveBoolean cpActiveBoolean;

        public class CrosspointActiveBoolean : BooleanBase
        {

            private static readonly Color DEFAULT_COLOR = Color.Cyan;

            private CrosspointBoolean parent;

            public CrosspointActiveBoolean(CrosspointBoolean parent)
            {
                Color = DEFAULT_COLOR;
                this.parent = parent;
                parent.IdChanged += parentIdChanged;
                parent.NameChanged += parentNameChanged;
                parent.WatchedRouterChanged += watchedRouterChanged;
                parent.WatchedInputChanged += watchedInputChanged;
                parent.WatchedOutputChanged += watchedOutputChanged;
                parent.ModelRemoved += parentRemoved;
                updateNameAndDescription();
                updateState();
                register();
            }

            private void parentRemoved(IModel model)
                => unregister();

            private void parentIdChanged(IModel crosspointBoolean, int oldValue, int newValue)
            {
                updateNameAndDescription();
                register();
            }

            private void parentNameChanged(IModel crosspointBoolean, string oldName, string newName)
                => updateDescription();

            private void watchedRouterChanged(CrosspointBoolean crosspointBoolean, Router oldValue, Router newValue)
            {
                if (oldValue != null)
                {
                    oldValue.IdChanged -= watchedRouterIdChanged;
                    oldValue.NameChanged -= watchedRouterNameChanged;
                }
                if (newValue != null)
                {
                    newValue.IdChanged += watchedRouterIdChanged;
                    newValue.NameChanged += watchedRouterNameChanged;
                }
                updateNameAndDescription();
            }

            private void watchedRouterIdChanged(IModel router, int oldId, int newId)
                => updateNameAndDescription();

            private void watchedRouterNameChanged(IModel router, string oldName, string newName)
                => updateDescription();

            private void watchedInputChanged(CrosspointBoolean crosspointBoolean, RouterInput oldValue, RouterInput newValue)
            {
                if (oldValue != null)
                {
                    oldValue.IndexChanged -= watchedInputIndexChanged;
                    oldValue.NameChanged -= watchedInputNameChanged;
                }
                if (newValue != null)
                {
                    newValue.IndexChanged += watchedInputIndexChanged;
                    newValue.NameChanged += watchedInputNameChanged;
                }
                updateNameAndDescription();
                updateState();
            }

            private void watchedInputIndexChanged(RouterInput input, int oldIndex, int newIndex)
                => updateNameAndDescription();

            private void watchedInputNameChanged(RouterInput input, string oldName, string newName)
                => updateDescription();

            private void watchedOutputChanged(CrosspointBoolean crosspointBoolean, RouterOutput oldValue, RouterOutput newValue)
            {
                if (oldValue != null)
                {
                    oldValue.IndexChanged -= watchedOutputIndexChanged;
                    oldValue.NameChanged -= watchedOutputNameChanged;
                    oldValue.CurrentInputChanged -= watchedOutputCurrentInputChanged;
                }
                if (newValue != null)
                {
                    newValue.IndexChanged += watchedOutputIndexChanged;
                    newValue.NameChanged += watchedOutputNameChanged;
                    newValue.CurrentInputChanged += watchedOutputCurrentInputChanged;
                }
                updateNameAndDescription();
                updateState();
            }

            private void watchedOutputIndexChanged(RouterOutput output, int oldIndex, int newIndex)
                => updateNameAndDescription();

            private void watchedOutputNameChanged(RouterOutput output, string oldName, string newName)
                => updateDescription();

            private void watchedOutputCurrentInputChanged(RouterOutput output, RouterInput newInput)
                => updateState();

            private void updateNameAndDescription()
            {
                updateName();
                updateDescription();
            }

            private void updateName()
                => Name = string.Format("crosspointboolean.{0}", parent.ID);

            internal void updateDescription()
            {
                string descriptionFirstPart = string.Format("Crosspoint boolean [(#{0}) {1}]: ", parent.ID, parent.Name);
                if (!parent.IsValid)
                {
                    Description = descriptionFirstPart + "invalid settings";
                    return;
                }
                Description = descriptionFirstPart + string.Format("crosspoint [I: [(#{0}) {1}], O: [(#{2}) {3}]] of router [(#{4}) {5}] is active.",
                    parent.WatchedInput.Index,
                    parent.WatchedInput.Name,
                    parent.WatchedOutput.Index,
                    parent.WatchedOutput.Name,
                    parent.WatchedRouter.ID,
                    parent.WatchedRouter.Name);
            }

            private void updateState()
                => CurrentState = (parent.WatchedOutput?.CurrentInput == parent.WatchedInput);

        }
        #endregion

    }

}
