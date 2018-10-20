using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.VTRs
{

    public delegate void VtrIdChangingDelegate(Vtr vtr, int oldValue, int newValue);
    public delegate void VtrIdChangedDelegate(Vtr vtr, int oldValue, int newValue);

    public delegate void VtrNameChangingDelegate(Vtr vtr, string oldName, string newName);
    public delegate void VtrNameChangedDelegate(Vtr vtr, string oldName, string newName);

    public delegate void VtrStateChangingDelegate(Vtr vtr, VtrState oldState, VtrState newState);
    public delegate void VtrStateChangedDelegate(Vtr vtr, VtrState oldState, VtrState newState);

    public delegate void VtrSecondsFullChangingDelegate(Vtr vtr, int oldValue, int newValue);
    public delegate void VtrSecondsFullChangedDelegate(Vtr vtr, int oldValue, int newValue);

    public delegate void VtrSecondsElapsedChangingDelegate(Vtr vtr, int oldValue, int newValue);
    public delegate void VtrSecondsElapsedChangedDelegate(Vtr vtr, int oldValue, int newValue);

    public delegate void VtrSecondsRemainingChangingDelegate(Vtr vtr, int oldValue, int newValue);
    public delegate void VtrSecondsRemainingChangedDelegate(Vtr vtr, int oldValue, int newValue);

    public class Vtr: IModel
    {

        public virtual void Restored()
        { }

        public event VtrIdChangingDelegate IdChanging;
        public event VtrIdChangedDelegate IdChanged;
        public event ParameterlessChangeNotifierDelegate IdChangingPCN;
        public event ParameterlessChangeNotifierDelegate IdChangedPCN;

        public int id = 0;

        public int ID
        {
            get { return id; }
            set
            {
                ValidateId(value);
                int oldValue = id;
                IdChanging?.Invoke(this, oldValue, value);
                IdChangingPCN?.Invoke();
                id = value;
                IdChanged?.Invoke(this, oldValue, value);
                IdChangedPCN?.Invoke();
            }
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            if (!VtrDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }


        public event VtrNameChangingDelegate NameChanging;
        public event VtrNameChangedDelegate NameChanged;
        public event ParameterlessChangeNotifierDelegate NameChangingPCN;
        public event ParameterlessChangeNotifierDelegate NameChangedPCN;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (value == name)
                    return;
                string oldName = name;
                NameChanging?.Invoke(this, oldName, value);
                NameChangingPCN?.Invoke();
                name = value;
                NameChanged?.Invoke(this, oldName, value);
                NameChangedPCN?.Invoke();
            }
        }

        public event VtrStateChangingDelegate StateChanging;
        public event VtrStateChangedDelegate StateChanged;
        public event ParameterlessChangeNotifierDelegate StateChangingPCN;
        public event ParameterlessChangeNotifierDelegate StateChangedPCN;

        private VtrState state;

        public VtrState State
        {
            get { return state; }
            protected set
            {
                if (value == state)
                    return;
                VtrState oldState = state;
                StateChanging?.Invoke(this, oldState, value);
                StateChangingPCN?.Invoke();
                state = value;
                StateChanged?.Invoke(this, oldState, value);
                StateChangedPCN?.Invoke();
            }
        }

        public event VtrSecondsFullChangingDelegate SecondsFullChanging;
        public event VtrSecondsFullChangedDelegate SecondsFullChanged;
        public event ParameterlessChangeNotifierDelegate SecondsFullChangingPCN;
        public event ParameterlessChangeNotifierDelegate SecondsFullChangedPCN;

        private int secondsFull;

        public int SecondsFull
        {
            get { return secondsFull; }
            protected set
            {
                if (value == secondsFull)
                    return;
                int oldValue = secondsFull;
                SecondsFullChanging?.Invoke(this, oldValue, value);
                SecondsFullChangingPCN?.Invoke();
                secondsFull = value;
                SecondsFullChanged?.Invoke(this, oldValue, value);
                SecondsFullChangedPCN?.Invoke();
            }
        }

        public event VtrSecondsElapsedChangingDelegate SecondsElapsedChanging;
        public event VtrSecondsElapsedChangedDelegate SecondsElapsedChanged;
        public event ParameterlessChangeNotifierDelegate SecondsElapsedChangingPCN;
        public event ParameterlessChangeNotifierDelegate SecondsElapsedChangedPCN;

        private int secondsElapsed;

        public int SecondsElapsed
        {
            get { return secondsElapsed; }
            protected set
            {
                if (value == secondsElapsed)
                    return;
                int oldValue = secondsElapsed;
                SecondsElapsedChanging?.Invoke(this, oldValue, value);
                SecondsElapsedChangingPCN?.Invoke();
                secondsElapsed = value;
                SecondsElapsedChanged?.Invoke(this, oldValue, value);
                SecondsElapsedChangedPCN?.Invoke();
            }
        }

        public event VtrSecondsRemainingChangingDelegate SecondsRemainingChanging;
        public event VtrSecondsRemainingChangedDelegate SecondsRemainingChanged;
        public event ParameterlessChangeNotifierDelegate SecondsRemainingChangingPCN;
        public event ParameterlessChangeNotifierDelegate SecondsRemainingChangedPCN;

        private int secondsRemaining;

        public int SecondsRemaining
        {
            get { return secondsRemaining; }
            protected set
            {
                if (value == secondsRemaining)
                    return;
                int oldValue = secondsRemaining;
                SecondsRemainingChanging?.Invoke(this, oldValue, value);
                SecondsRemainingChangingPCN?.Invoke();
                secondsRemaining = value;
                SecondsRemainingChanged?.Invoke(this, oldValue, value);
                SecondsRemainingChangedPCN?.Invoke();
            }
        }

    }
}
