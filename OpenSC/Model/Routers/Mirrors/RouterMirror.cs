using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.Routers.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Mirrors
{

    public class RouterMirror : ModelBase
    {

        public const string LOG_TAG = "RouterMirror";

        #region Persistence, instantiation
        public RouterMirror()
        { }

        public override void Removed()
        {
            base.Removed();
            IdChanged = null;
            NameChanged = null;
            ClearInputAssociations();
            ClearOutputAssociations();
        }
        #endregion

        #region Restoration
        public override void RestoredBasicRelations()
        {
            base.RestoredBasicRelations();
            if (routerA != null)
            {
                routerA.StateChanged += routerAstateChangedHandler;
                routerAstate = routerA.State;
            }
            if (routerB != null)
            {
                routerB.StateChanged += routerBstateChangedHandler;
                routerBstate = routerB.State;
            }
        }

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            totallyRestored = true;
            inputAssociations.ForEach(ia => ia.TotallyRestored());
            outputAssociations.ForEach(oa => oa.TotallyRestored());
            if (synchronizeFromOnTotallyRestored != null)
                Synchronize((RouterMirrorSide)synchronizeFromOnTotallyRestored);
        }

        private bool totallyRestored = false;
        private RouterMirrorSide? synchronizeFromOnTotallyRestored = null;
        #endregion

        #region Property: ID
        public delegate void IdChangedDelegate(RouterMirror routerMirror, int oldValue, int newValue);
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
            if (!RouterMirrorDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Name
        public delegate void NameChangedDelegate(RouterMirror routerMirror, string oldName, string newName);
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
                string oldValue = name;
                name = value;
                NameChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(Name));
            }
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Routers
        [PersistAs("router_a")]
        private Router routerA;

#pragma warning disable CS0169
        [TempForeignKey(RouterDatabase.DBNAME, nameof(routerA))]
        private int _routerAid;
#pragma warning restore CS0169

        public Router RouterA
        {
            get => routerA;
            set
            {
                if (value == routerA)
                    return;
                if (routerA != null)
                {
                    routerA.StateChanged -= routerAstateChangedHandler;
                    routerAstate = RouterState.Unknown;
                }
                Router oldValue = routerA;
                routerA = value;
                if (routerA != null)
                {
                    routerA.StateChanged += routerAstateChangedHandler;
                    routerAstate = routerA.State;
                }
                RouterAChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(RouterA));
                ClearInputAssociations();
                ClearOutputAssociations();
            }
        }

        public delegate void RouterAChangedDelegate(RouterMirror routerMirror, Router oldValue, Router newValue);
        public event RouterAChangedDelegate RouterAChanged;

        [PersistAs("router_b")]
        private Router routerB;

#pragma warning disable CS0169
        [TempForeignKey(RouterDatabase.DBNAME, nameof(routerB))]
        private int _routerBid;
#pragma warning restore CS0169

        public Router RouterB
        {
            get => routerB;
            set
            {
                if (value == routerB)
                    return;
                if (routerB != null)
                {
                    routerB.StateChanged -= routerBstateChangedHandler;
                    routerBstate = RouterState.Unknown;
                }
                Router oldValue = routerB;
                routerB = value;
                if (routerB != null)
                {
                    routerB.StateChanged += routerBstateChangedHandler;
                    routerBstate = RouterState.Unknown;
                }
                RouterBChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(RouterB));
                ClearInputAssociations();
                ClearOutputAssociations();
            }
        }

        public delegate void RouterBChangedDelegate(RouterMirror routerMirror, Router oldValue, Router newValue);
        public event RouterAChangedDelegate RouterBChanged;
        #endregion

        #region Property: SynchronizationMode
        public delegate void SynchronizationModeDelegate(RouterMirror routerMirror, RouterMirrorSynchronizationMode oldMode, RouterMirrorSynchronizationMode newMode);
        public event SynchronizationModeDelegate SynchronizationModeChanged;

        [PersistAs("synchronization_mode")]
        private RouterMirrorSynchronizationMode synchronizationMode;

        public RouterMirrorSynchronizationMode SynchronizationMode
        {
            get { return synchronizationMode; }
            set
            {
                if (value == synchronizationMode)
                    return;
                RouterMirrorSynchronizationMode oldValue = synchronizationMode;
                synchronizationMode = value;
                SynchronizationModeChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(SynchronizationMode));
            }
        }
        #endregion

        #region Input associations
        private ObservableList<RouterMirrorInputAssociation> inputAssociations = new ObservableList<RouterMirrorInputAssociation>();
        public ObservableList<RouterMirrorInputAssociation> InputAssociations => inputAssociations;

        [PersistAs("input_associations")]
        [PersistAs(null, 1)]
        private RouterMirrorInputAssociation[] _inputAssociations // for persistence
        {
            get { return inputAssociations.ToArray(); }
            set
            {
                inputAssociations.Clear();
                if (value != null)
                    inputAssociations.AddRange(value);
                inputAssociations.ForEach(i => i.AssignParent(this));
            }
        }

        public void ClearInputAssociations()
        {
            inputAssociations.ForEach(ia => ia.RemovedFromParent());
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
        [PersistAs(null, 1)]
        private RouterMirrorOutputAssociation[] _outputAssociations // for persistence
        {
            get { return outputAssociations.ToArray(); }
            set
            {
                outputAssociations.Clear();
                if (value != null)
                    outputAssociations.AddRange(value);
                outputAssociations.ForEach(i => i.AssignParent(this));
            }
        }

        public void ClearOutputAssociations()
        {
            outputAssociations.ForEach(oa => oa.RemovedFromParent());
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
