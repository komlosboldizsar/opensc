using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.CrosspointBooleans
{

    public partial class CrosspointBoolean : ModelBase
    {

        public const string LOG_TAG = "CrosspointBoolean";

        #region Persistence, instantiation
        public CrosspointBoolean()
        {
            cpActiveBoolean = new CrosspointActiveBoolean(this);
        }

        public override void Removed()
        {
            base.Removed();
            WatchedInputChanged = null;
            WatchedOutputChanged = null;
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = CrosspointBooleanDatabase.Instance;
        #endregion

        #region Property: WatchedRouter
        [AutoProperty]
        private Router watchedRouter;

        private void updateWatchedRouter()
        {
            Router wor = watchedOutput?.Parent;
            WatchedRouter = (watchedInput?.Parent == wor) ? wor : null;
        }
        #endregion

        #region Property: WatchedInput
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(updateCalculated))]
        [PersistAs("watched_input")]
        private RouterInput watchedInput;
        #endregion

        #region Property: WatchedOutput
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(updateCalculated))]
        [PersistAs("watched_output")]
        private RouterOutput watchedOutput;
        #endregion

        #region Property: IsValid
        [AutoProperty]
        private bool isValid;

        private void updateIsValid() => IsValid = ((watchedRouter != null) && (watchedInput != null) && (watchedOutput != null));
        #endregion

        private void updateCalculated()
        {
            updateWatchedRouter();
            updateIsValid();
        }

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

            private void parentRemoved(IModel model) => unregister();

            private void parentIdChanged(IModel crosspointBoolean, int oldValue, int newValue)
            {
                updateNameAndDescription();
                register();
            }

            private void parentNameChanged(IModel crosspointBoolean, string oldName, string newName) => updateDescription();

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

            private void watchedRouterIdChanged(IModel router, int oldId, int newId) => updateNameAndDescription();
            private void watchedRouterNameChanged(IModel router, string oldName, string newName) => updateDescription();

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

            private void watchedInputIndexChanged(RouterInput input, int oldIndex, int newIndex) => updateNameAndDescription();
            private void watchedInputNameChanged(RouterInput input, string oldName, string newName) => updateDescription();

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

            private void watchedOutputIndexChanged(RouterOutput output, int oldIndex, int newIndex) => updateNameAndDescription();
            private void watchedOutputNameChanged(RouterOutput output, string oldName, string newName) => updateDescription();
            private void watchedOutputCurrentInputChanged(RouterOutput output, RouterInput newInput) => updateState();

            private void updateNameAndDescription()
            {
                updateName();
                updateDescription();
            }

            private void updateName()
                => Identifier = string.Format("crosspointboolean.{0}", parent.ID);

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

            private void updateState() => CurrentState = (parent.WatchedOutput?.CurrentInput == parent.WatchedInput);

        }
        #endregion

    }

}
