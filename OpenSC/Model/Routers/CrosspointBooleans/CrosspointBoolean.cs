using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.Routers.Triggers;
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
            IdChanged = null;
            NameChanged = null;
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

        #region Property: ID
        public delegate void IdChangedDelegate(CrosspointBoolean crosspointBoolean, int oldValue, int newValue);
        public event IdChangedDelegate IdChanged;

        public int id = 0;

        public override int ID
        {
            get { return id; }
            set
            {
                ValidateId(value);
                if (value == id)
                    return;
                int oldValue = id;
                id = value;
                IdChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(ID));
            }
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            if (!CrosspointBooleanDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Name
        public delegate void NameChangedDelegate(CrosspointBoolean crosspointBoolean, string oldName, string newName);
        public event NameChangedDelegate NameChanged;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                ValidateName(value);
                if (value == name)
                    return;
                string oldName = name;
                name = value;
                NameChanged?.Invoke(this, oldName, value);
                RaisePropertyChanged(nameof(Name));
            }
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
        }
        #endregion

        #region Property: WatchedRouter

        public delegate void WatchedRouterChangedDelegate(CrosspointBoolean crosspointBoolean, Router oldValue, Router newValue);
        public event WatchedRouterChangedDelegate WatchedRouterChanged;

        private Router watchedRouter;

        public Router WatchedRouter
        {
            get => watchedRouter;
            set
            {
                if (value == watchedRouter)
                    return;
                Router oldValue = watchedRouter;
                watchedRouter = value;
                WatchedRouterChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(WatchedRouter));
            }
        }

        private void updateWatchedRouter()
        {
            Router wir = watchedInput?.Router;
            Router wor = watchedOutput?.Router;
            WatchedRouter = (wir == wor) ? wor : null;
        }
        #endregion

        #region Property: WatchedInput
        public delegate void WatchedInputChangedDelegate(CrosspointBoolean crosspointBoolean, RouterInput oldValue, RouterInput newValue);
        public event WatchedInputChangedDelegate WatchedInputChanged;

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
            get { return watchedInput; }
            set
            {
                if (value == watchedInput)
                    return;
                RouterInput oldValue = watchedInput;
                watchedInput = value;
                updateWatchedRouter();
                updateIsValid();
                WatchedInputChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(WatchedInput));
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
        public delegate void WatchedOutputChangedDelegate(CrosspointBoolean crosspointBoolean, RouterOutput oldValue, RouterOutput newValue);
        public event WatchedOutputChangedDelegate WatchedOutputChanged;

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
            get { return watchedOutput; }
            set
            {
                if (value == watchedOutput)
                    return;
                RouterOutput oldValue = watchedOutput;
                watchedOutput = value;
                updateWatchedRouter();
                updateIsValid();
                WatchedOutputChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(WatchedOutput));
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

        #region Property: WatchedRouter
        public delegate void IsValidChangedDelegate(CrosspointBoolean crosspointBoolean, bool oldValue, bool newValue);
        public event IsValidChangedDelegate IsValidChanged;

        private bool isValid;

        public bool IsValid
        {
            get => isValid;
            set
            {
                if (value == isValid)
                    return;
                bool oldValue = isValid;
                isValid = value;
                IsValidChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(IsValidChanged));
            }
        }

        private void updateIsValid()
        {
            IsValid = ((watchedRouter != null) && (watchedInput != null) && (watchedOutput != null));
        }
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

            private void watchedRouterIdChanged(Router router, int oldId, int newId)
                => updateNameAndDescription();

            private void watchedRouterNameChanged(Router router, string oldName, string newName)
                => updateNameAndDescription();

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
                => updateNameAndDescription();

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
                => updateNameAndDescription();

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
                Description = descriptionFirstPart + string.Format("crosspoint [I: [(#{1}) {2}], O: [(#{3}) {4}]] of router [(#{5}) {6}] is active.",
                    parent.WatchedRouter.ID,
                    parent.WatchedRouter.Name,
                    parent.WatchedInput.Index,
                    parent.WatchedInput.Name,
                    parent.WatchedOutput.Index,
                    parent.WatchedOutput.Name);
            }

            private void updateState()
                => CurrentState = (parent.WatchedOutput?.CurrentInput == parent.WatchedInput);

        }
        #endregion

    }

}
