using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Mirrors
{

    public partial class RouterMirror : ModelBase
    {

        public const string LOG_TAG = "RouterMirror";

        #region Persistence, instantiation
        public RouterMirror()
        { }

        public override void Removed()
        {
            base.Removed();
            ClearInputAssociations();
            ClearOutputAssociations();
        }
        #endregion

        #region Restoration
        public override void TotallyRestored()
        {
            base.TotallyRestored();
            totallyRestored = true;
            inputAssociations.Foreach(ia => ia.TotallyRestored());
            outputAssociations.Foreach(oa => oa.TotallyRestored());
            if (synchronizeFromOnTotallyRestored != null)
                Synchronize((RouterMirrorSide)synchronizeFromOnTotallyRestored);
        }

        private bool totallyRestored = false;
        private RouterMirrorSide? synchronizeFromOnTotallyRestored = null;
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = RouterMirrorDatabase.Instance;
        #endregion

        #region Property: Routers
        private void _routerX_beforeChange(Router oldValue, PropertyChangedTwoValuesDelegate<Router, RouterState> stateChangedHandler, ref RouterState stateVariable)
        {
            if (oldValue != null)
                oldValue.StateChanged -= stateChangedHandler;
            stateVariable = RouterState.Unknown;
        }

        private void _routerX_afterChange(Router oldValue, Router newValue, PropertyChangedTwoValuesDelegate<Router, RouterState> stateChangedHandler, ref RouterState stateVariable)
        {
            if (newValue != null)
            {
                newValue.StateChanged += stateChangedHandler;
                stateVariable = newValue.State;
            }
            if (oldValue != null)
            {
                ClearInputAssociations();
                ClearOutputAssociations();
            }
        }

        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_routerA_beforeChange))]
        [AutoProperty.AfterChange(nameof(_routerA_afterChange))]
        [PersistAs("router_a")]
        private Router routerA;

        private void _routerA_beforeChange(Router oldValue, Router newValue, BeforeChangePropertyArgs args)
            => _routerX_beforeChange(oldValue, routerAstateChangedHandler, ref routerAstate);

        private void _routerA_afterChange(Router oldValue, Router newValue)
            => _routerX_afterChange(oldValue, newValue, routerAstateChangedHandler, ref routerAstate);

        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_routerB_beforeChange))]
        [AutoProperty.AfterChange(nameof(_routerB_afterChange))]
        [PersistAs("router_b")]
        private Router routerB;

        private void _routerB_beforeChange(Router oldValue, Router newValue, BeforeChangePropertyArgs args)
            => _routerX_beforeChange(oldValue, routerBstateChangedHandler, ref routerBstate);

        private void _routerB_afterChange(Router oldValue, Router newValue)
            => _routerX_afterChange(oldValue, newValue, routerBstateChangedHandler, ref routerBstate);
        #endregion

        #region Property: SynchronizationMode
        [AutoProperty]
        [PersistAs("synchronization_mode")]
        private RouterMirrorSynchronizationMode synchronizationMode;
        #endregion

        #region Input associations
        private ObservableList<RouterMirrorInputAssociation> inputAssociations = new ObservableList<RouterMirrorInputAssociation>();
        public ObservableList<RouterMirrorInputAssociation> InputAssociations => inputAssociations;

        [PersistAs("input_associations")]
        [PersistAs("association", 1)]
        private RouterMirrorInputAssociation[] _inputAssociations // for persistence
        {
            get => inputAssociations.ToArray();
            set
            {
                inputAssociations.Clear();
                if (value != null)
                    inputAssociations.AddRange(value);
                inputAssociations.Foreach(i => i.AssignParent(this));
            }
        }

        public void ClearInputAssociations()
        {
            inputAssociations.Foreach(ia => ia.RemovedFromParent());
            inputAssociations.Clear();
        }

        public void AddInputAssociation(RouterInput inputA, RouterInput inputB)
            => inputAssociations.Add(new RouterMirrorInputAssociation(this, inputA, inputB));

        private RouterInput getOtherSideInput(RouterInput thisSideInput, RouterMirrorSide thisSide)
        {
            switch (thisSide)
            {
                case RouterMirrorSide.SideA:
                    return inputAssociations.FirstOrDefault(ia => (ia.ItemA == thisSideInput))?.ItemB;
                case RouterMirrorSide.SideB:
                    return inputAssociations.FirstOrDefault(ia => (ia.ItemB == thisSideInput))?.ItemA;
            }
            return null;
        }
        #endregion

        #region Output associations
        private ObservableList<RouterMirrorOutputAssociation> outputAssociations = new ObservableList<RouterMirrorOutputAssociation>();
        public ObservableList<RouterMirrorOutputAssociation> OutputAssociations => outputAssociations;

        [PersistAs("output_associations")]
        [PersistAs("association", 1)]
        private RouterMirrorOutputAssociation[] _outputAssociations // for persistence
        {
            get => outputAssociations.ToArray();
            set
            {
                outputAssociations.Clear();
                if (value != null)
                    outputAssociations.AddRange(value);
                outputAssociations.Foreach(i => i.AssignParent(this));
            }
        }

        public void ClearOutputAssociations()
        {
            outputAssociations.Foreach(oa => oa.RemovedFromParent());
            outputAssociations.Clear();
        }

        public void AddOutputAssociation(RouterOutput outputA, RouterOutput outputB)
            => outputAssociations.Add(new RouterMirrorOutputAssociation(this, outputA, outputB));

        private RouterOutput getOtherSideOutput(RouterOutput thisSideOutput, RouterMirrorSide thisSide)
        {
            switch (thisSide)
            {
                case RouterMirrorSide.SideA:
                    return outputAssociations.FirstOrDefault(oa => (oa.ItemA == thisSideOutput))?.ItemB;
                case RouterMirrorSide.SideB:
                    return outputAssociations.FirstOrDefault(oa => (oa.ItemB == thisSideOutput))?.ItemA;
            }
            return null;
        }
        #endregion

        #region Mirroring, synchronization
        private void updateOutput(RouterOutput sourceOutput, RouterInput sourceInput, RouterMirrorSide sourceSide)
        {
            RouterOutput otherSideOutput = getOtherSideOutput(sourceOutput, sourceSide);
            if (otherSideOutput == null)
                return;
            RouterInput otherSideNewInput = getOtherSideInput(sourceInput, sourceSide);
            if (otherSideNewInput == null)
                return;
            RouterInput otherSideCurrentInput = otherSideOutput.CurrentInput;
            if (otherSideCurrentInput != otherSideNewInput)
                otherSideOutput.RequestCrosspointUpdate(otherSideNewInput);
        }

        internal void OutputChanged(RouterOutput output, RouterInput newInput, RouterMirrorSide side)
        {
            if ((routerAstate != RouterState.Ok) || (routerBstate != RouterState.Ok))
                return;
            updateOutput(output, newInput, side);
        }

        public void Synchronize(RouterMirrorSide sourceSide)
        {
            foreach (RouterMirrorOutputAssociation oa in OutputAssociations)
            {
                RouterOutput sourceOutpt = (sourceSide == RouterMirrorSide.SideA) ? oa.ItemA : oa.ItemB;
                updateOutput(sourceOutpt, sourceOutpt.CurrentInput, sourceSide);
            }
        }
        #endregion

        #region Router states
        private RouterState routerAstate = RouterState.Unknown;
        private RouterState routerBstate = RouterState.Unknown;

        private void routerAstateChangedHandler(Router router, RouterState oldState, RouterState newState)
        {
            routerAstate = newState;
            routerStateChanged(RouterMirrorSide.SideA);
        }

        private void routerBstateChangedHandler(Router router, RouterState oldState, RouterState newState)
        {
            routerBstate = newState;
            routerStateChanged(RouterMirrorSide.SideB);
        }

        private void routerStateChanged(RouterMirrorSide side)
        {
            if ((routerAstate != RouterState.Ok) || (routerBstate != RouterState.Ok))
                return;
            RouterMirrorSide? synchronizeFrom = null;
            switch (synchronizationMode)
            {
                case RouterMirrorSynchronizationMode.Never:
                    synchronizeFrom = null;
                    break;
                case RouterMirrorSynchronizationMode.FromSideA:
                    synchronizeFrom = RouterMirrorSide.SideA;
                    break;
                case RouterMirrorSynchronizationMode.FromSideB:
                    synchronizeFrom = RouterMirrorSide.SideB;
                    break;
                case RouterMirrorSynchronizationMode.FromFirstConnected:
                    synchronizeFrom = (side == RouterMirrorSide.SideA) ? RouterMirrorSide.SideB : RouterMirrorSide.SideA;
                    break;
                case RouterMirrorSynchronizationMode.FromLastConnected:
                    synchronizeFrom = side;
                    break;
            }
            if (synchronizeFrom == null)
                return;
            if (!totallyRestored)
            {
                synchronizeFromOnTotallyRestored = synchronizeFrom;
                return;
            }
            Synchronize((RouterMirrorSide)synchronizeFrom);
        }
        #endregion

    }

}
