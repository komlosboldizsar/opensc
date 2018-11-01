using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{


    public delegate void SignalIdChangingDelegate(Signal signal, int oldValue, int newValue);
    public delegate void SignalIdChangedDelegate(Signal signal, int oldValue, int newValue);

    public delegate void SignalNameChangingDelegate(Signal signal, string oldName, string newName);
    public delegate void SignalNameChangedDelegate(Signal signal, string oldName, string newName);

    public delegate void SignalCategoryChangingDelegate(Signal signal, SignalCategory oldCategory, SignalCategory newCategory);
    public delegate void SignalCategoryChangedDelegate(Signal signal, SignalCategory oldCategory, SignalCategory newCategory);

    public delegate void SignalTallyChangingDelegate(Signal signal, bool oldState, bool newState);
    public delegate void SignalTallyChangedDelegate(Signal signal, bool oldState, bool newState);

    public class Signal : IModel
    {

        public virtual void Restored()
        { }

        public event SignalIdChangingDelegate IdChanging;
        public event SignalIdChangedDelegate IdChanged;
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
            if (!SignalDatabases.Signals.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }


        public event SignalNameChangingDelegate NameChanging;
        public event SignalNameChangedDelegate NameChanged;
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


        public event SignalCategoryChangingDelegate CategoryChanging;
        public event SignalCategoryChangedDelegate CategoryChanged;
        public event ParameterlessChangeNotifierDelegate CategoryChangingPCN;
        public event ParameterlessChangeNotifierDelegate CategoryChangedPCN;

        [PersistAs("category")]
        private SignalCategory category;

        [TempForeignKey(SignalDatabases.DBNAME_CATEGORIES, nameof(category))]
        private int _category;

        public SignalCategory Category
        {
            get { return category; }
            set
            {
                if (value == category)
                    return;
                SignalCategory oldCategory = category;
                CategoryChanging?.Invoke(this, oldCategory, value);
                CategoryChangingPCN?.Invoke();
                category = value;
                CategoryChanged?.Invoke(this, oldCategory, value);
                CategoryChangedPCN?.Invoke();
            }
        }

        public event SignalTallyChangingDelegate RedTallyChanging;
        public event SignalTallyChangedDelegate RedTallyChanged;
        public event ParameterlessChangeNotifierDelegate RedTallyChangingPCN;
        public event ParameterlessChangeNotifierDelegate RedTallyChangedPCN;

        private bool redTally;

        public bool RedTally
        {
            get { return redTally; }
            private set
            {
                if (value == redTally)
                    return;
                bool oldState = redTally;
                RedTallyChanging?.Invoke(this, oldState, value);
                RedTallyChangingPCN?.Invoke();
                redTally = value;
                RedTallyChanged?.Invoke(this, oldState, value);
                RedTallyChangedPCN?.Invoke();
            }
        }

        public event SignalTallyChangingDelegate GreenTallyChanging;
        public event SignalTallyChangedDelegate GreenTallyChanged;
        public event ParameterlessChangeNotifierDelegate GreenTallyChangingPCN;
        public event ParameterlessChangeNotifierDelegate GreenTallyChangedPCN;

        private bool greenTally;

        public bool GreenTally
        {
            get { return greenTally; }
            private set
            {
                if (value == greenTally)
                    return;
                bool oldState = greenTally;
                GreenTallyChanging?.Invoke(this, oldState, value);
                GreenTallyChangingPCN?.Invoke();
                greenTally = value;
                GreenTallyChanged?.Invoke(this, oldState, value);
                GreenTallyChangedPCN?.Invoke();
            }
        }

    }

}
