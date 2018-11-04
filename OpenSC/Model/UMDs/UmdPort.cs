using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{

    public delegate void UmdPortIdChangingDelegate(UmdPort port, int oldValue, int newValue);
    public delegate void UmdPortIdChangedDelegate(UmdPort port, int oldValue, int newValue);

    public delegate void UmdPortNameChangingDelegate(UmdPort port, string oldName, string newName);
    public delegate void UmdPortNameChangedDelegate(UmdPort port, string oldName, string newName);

    public delegate void UmdPortInitializedChangingDelegate(UmdPort port, bool oldState, bool newState);
    public delegate void UmdPortInitializedChangedDelegate(UmdPort port, bool oldState, bool newState);

    abstract public class UmdPort : ModelBase
    {

        public event UmdPortIdChangingDelegate IdChanging;
        public event UmdPortIdChangedDelegate IdChanged;
        public event ParameterlessChangeNotifierDelegate IdChangingPCN;
        public event ParameterlessChangeNotifierDelegate IdChangedPCN;

        private int id = 0;

        public override int ID
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
            if (!UmdPortDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

        public event UmdPortNameChangingDelegate NameChanging;
        public event UmdPortNameChangedDelegate NameChanged;
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

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
        }


        public event UmdPortInitializedChangingDelegate InitializedChanging;
        public event UmdPortInitializedChangedDelegate InitializedChanged;
        public event ParameterlessChangeNotifierDelegate InitializedChangingPCN;
        public event ParameterlessChangeNotifierDelegate InitializedChangedPCN;

        private bool initialized;

        public bool Initialized
        {
            get { return initialized; }
            protected set
            {
                if (value == initialized)
                    return;
                bool oldState = initialized;
                InitializedChanging?.Invoke(this, oldState, value);
                InitializedChangingPCN?.Invoke();
                initialized = value;
                InitializedChanged?.Invoke(this, oldState, value);
                InitializedChangedPCN?.Invoke();
            }
        }

        public abstract void Init();
        public abstract void DeInit();

        public override void Restored()
        { }

    }
}
