using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Signals;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public class RouterOutput : ISignal, INotifyPropertyChanged
    {

        public RouterOutput()
        { }

        public RouterOutput(string name, Router router, int index)
        {
            this.name = name;
            this.Router = router;
            this.Index = index;
            createBooleans();
            registerAsSignal();
        }

        public void Restored()
        {
            createBooleans();
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException();
                if (value == name)
                    return;
                string oldName = name;
                name = value;
                NameChanged?.Invoke(this, oldName, value);
                PropertyChanged?.Invoke(nameof(Name));
                SignalLabelChanged?.Invoke(this, getSignalLabel());
                PropertyChanged?.Invoke(nameof(ISignal.SignalLabel));
            }
        }

        public delegate void NameChangedDelegate(RouterOutput output, string oldName, string newName);
        public event NameChangedDelegate NameChanged;

        public Router Router { get; internal set; }

        public void RemovedFromRouter(Router router)
        {
            if (router != Router)
                return;
            removeBooleans();
            unregisterAsSignal();
        }

        private int index;

        public int Index
        {
            get { return index; }
            internal set
            {
                if (value == index)
                    return;
                int oldIndex = index;
                index = value;
                IndexChanged?.Invoke(this, oldIndex, value);
                PropertyChanged?.Invoke(nameof(Index));
                SignalLabelChanged?.Invoke(this, getSignalLabel());
                PropertyChanged?.Invoke(nameof(ISignal.SignalLabel));
            }
        }


        public delegate void IndexChangedDelegate(RouterOutput output, int oldIndex, int newIndex);
        public event IndexChangedDelegate IndexChanged;

        private RouterInput crosspoint;

        public RouterInput Crosspoint
        {
            get { return crosspoint; }
            internal set
            {

                unsubscribeCrosspointEvents();
                crosspoint = value;

                string logMessage = string.Format("Router crosspoint updated. Router ID: {0}, destination: {1}, source: {2}.",
                    Router.ID,
                    Index,
                    value.Index);
                LogDispatcher.I(Router.LOG_TAG, logMessage);

                fireChangeEventsAtCrosspointChange();
                CrosspointChanged?.Invoke(this, value);
                subscribeCrosspointEvents();

            }
        }

        public delegate void CrosspointChangedDelegate(RouterOutput output, RouterInput newInput);
        public event CrosspointChangedDelegate CrosspointChanged;

        private void subscribeCrosspointEvents()
        {
            if (crosspoint == null)
                return;
            crosspoint.SourceNameChanged += crosspointSourceNameChangedHandler;
            crosspoint.RedTallyChanged += crosspointRedTallyChangedHandler;
            crosspoint.GreenTallyChanged += crosspointGreenTallyChangedHandler;
        }

        private void unsubscribeCrosspointEvents()
        {
            if (crosspoint == null)
                return;
            crosspoint.SourceNameChanged -= crosspointSourceNameChangedHandler;
            crosspoint.RedTallyChanged -= crosspointRedTallyChangedHandler;
            crosspoint.GreenTallyChanged -= crosspointGreenTallyChangedHandler;
        }

        private void fireChangeEventsAtCrosspointChange()
        {
            if(crosspoint == null)
            {
                SourceSignalNameChanged?.Invoke(this, null);
                RedTallyChanged?.Invoke(this, false, false);
                GreenTallyChanged?.Invoke(this, false, false);
            }
            else
            {
                SourceSignalNameChanged?.Invoke(this, crosspoint.SourceSignalName);
                RedTallyChanged?.Invoke(this, false, crosspoint.RedTally);
                GreenTallyChanged?.Invoke(this, false, crosspoint.GreenTally);
            }
        }

        private void crosspointSourceNameChangedHandler(RouterInput input, string newName)
        {
            SourceSignalNameChanged?.Invoke(this, newName);
        }

        private void crosspointRedTallyChangedHandler(RouterInput input, bool newState)
        {
            RedTallyChanged?.Invoke(this, false, newState);
        }

        private void crosspointGreenTallyChangedHandler(RouterInput input, bool newState)
        {
            GreenTallyChanged?.Invoke(this, false, newState);
        }

        public string InputName
        {
            get => crosspoint?.Name;
        }

        #region Property: SourceSignalName
        public string SourceSignalName
        {
            get => GetSourceSignalName();
        }

        public string GetSourceSignalName(List<object> recursionChain = null)
        {
            if (crosspoint == null)
                return null;
            if (recursionChain == null)
                recursionChain = new List<object>();
            if (recursionChain.Contains(this))
                return "(cyclic tieline)";
            recursionChain.Add(this);
            return crosspoint.GetSourceSignalName(recursionChain);
        }

        public event SourceSignalNameChangedDelegate SourceSignalNameChanged;
        #endregion

        #region Property: SignalLabel
        string ISignal.SignalLabel
            => getSignalLabel();

        private string getSignalLabel()
            => string.Format("[#{2}) {3}] output of router [(#{0}) {1}]", Router.ID, Router.Name, (Index + 1), Name);

        public event SignalLabelChangedDelegate SignalLabelChanged;
        #endregion

        #region Tallies
        public bool RedTally =>
            GetRedTally();

        public bool GreenTally =>
            GetGreenTally();

        public string SignalUniqueId
            => string.Format("router.{0}.output.{1}", Router.ID, (Index + 1));

        public bool GetRedTally(List<object> recursionChain = null)
        {
            if (crosspoint == null)
                return false;
            if (recursionChain == null)
                recursionChain = new List<object>();
            if (recursionChain.Contains(this))
                return false;
            recursionChain.Add(this);
            return crosspoint.GetRedTally(recursionChain);
        }

        public bool GetGreenTally(List<object> recursionChain = null)
        {
            if (crosspoint == null)
                return false;
            if (recursionChain == null)
                recursionChain = new List<object>();
            if (recursionChain.Contains(this))
                return false;
            recursionChain.Add(this);
            return crosspoint.GetGreenTally(recursionChain);
        }

        public event SignalTallyChangedDelegate RedTallyChanged;
        public event SignalTallyChangedDelegate GreenTallyChanged;
        #endregion

        #region Tally booleans
        private IBoolean redTallyBoolean = null;
        private IBoolean greenTallyBoolean = null;

        private void createBooleans()
        {
            redTallyBoolean = new TallyBoolean(this, TallyBoolean.TallyColor.Red);
            greenTallyBoolean = new TallyBoolean(this, TallyBoolean.TallyColor.Green);
            BooleanRegister.Instance.RegisterBoolean(redTallyBoolean);
            BooleanRegister.Instance.RegisterBoolean(greenTallyBoolean);
        }

        private void removeBooleans()
        {
            if (redTallyBoolean != null)
            {
                BooleanRegister.Instance.UnregisterBoolean(redTallyBoolean);
                redTallyBoolean = null;
            }
            if (greenTallyBoolean != null)
            {
                BooleanRegister.Instance.UnregisterBoolean(greenTallyBoolean);
                greenTallyBoolean = null;
            }
        }

        public void IsTalliedFrom(ISignalTallySource source, SignalTallyType type, bool isTallied)
        {
            throw new NotImplementedException();
        }

        private class TallyBoolean : BooleanBase
        {

            private RouterOutput output;

            private TallyColor color;

            public TallyBoolean(RouterOutput output, TallyColor color) :
                base(getName(output, color), getColor(color), getDescription(output, color))
            {
                this.output = output;
                this.color = color;
                output.IndexChanged += indexChangedHandler;
                output.NameChanged += nameChangedHandler;
                output.Router.IdChanged += routerIdChangedHandler;
                output.Router.NameChanged += routerNameChangedHandler;
                switch (color)
                {
                    case TallyColor.Red:
                        CurrentState = output.RedTally;
                        output.RedTallyChanged += tallyChangedHandler;
                        break;
                    case TallyColor.Green:
                        CurrentState = output.GreenTally;
                        output.GreenTallyChanged += tallyChangedHandler;
                        break;
                }
            }

            public void Update()
            {
                Name = getName(output, color);
                Description = getDescription(output, color);
            }

            private void tallyChangedHandler(ISignal output, bool oldState, bool newState)
            {
                CurrentState = newState;
            }

            private void indexChangedHandler(RouterOutput output, int oldIndex, int newIndex)
            {
                Name = getName(output, color);
                Description = getDescription(output, color);
            }

            private void nameChangedHandler(RouterOutput output, string oldName, string newName)
            {
                Description = getDescription(output, color);
            }
            private void routerIdChangedHandler(Router router, int oldValue, int newValue)
            {
                Name = getName(output, color);
                Description = getDescription(output, color);
            }

            private void routerNameChangedHandler(Router router, string oldName, string newName)
            {
                Description = getDescription(output, color);
            }

            private static string getName(RouterOutput output, TallyColor color)
                => string.Format("router.{0}.output.{1}.{2}tally", output.Router.ID, (output.Index + 1), getColorString(color));

            private static Color getColor(TallyColor color)
            {
                switch (color)
                {
                    case TallyColor.Red:
                        return Color.Red;
                    case TallyColor.Green:
                        return Color.Green;
                }
                return Color.White;
            }

            private static string getDescription(RouterOutput output, TallyColor color)
                => string.Format("The signal switched to the [(#{2}) {3}] output of router [(#{0}) {1}] has {4} tally.",
                    output.Router.ID, output.Router.Name,
                    (output.Index + 1), output.Name,
                    getColorString(color));

            private static string getColorString(TallyColor color)
            {
                switch (color)
                {
                    case TallyColor.Red:
                        return "red";
                    case TallyColor.Green:
                        return "green";
                }
                return "unknown";
            }

            public enum TallyColor
            {
                Red,
                Green
            }

        }
        #endregion

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedDelegate PropertyChanged;
        #endregion

        #region Signals
        private void registerAsSignal()
        {
            SignalRegister.Instance.RegisterSignal(this);
        }

        private void unregisterAsSignal()
        {
            SignalRegister.Instance.UnregisterSignal(this);
        }
        #endregion

    }

}
