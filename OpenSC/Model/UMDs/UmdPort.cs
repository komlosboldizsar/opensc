using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{

    

    abstract public class UmdPort : ModelBase
    {

        public delegate void IdChangedDelegate(UmdPort port, int oldValue, int newValue);
        public event IdChangedDelegate IdChanged;

        private int id = 0;

        public override int ID
        {
            get { return id; }
            set
            {
                ValidateId(value);
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
            if (!UmdPortDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

        public delegate void NameChangedDelegate(UmdPort port, string oldName, string newName);
        public event NameChangedDelegate NameChanged;

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

        public delegate void InitializedChangedDelegate(UmdPort port, bool oldState, bool newState);
        public event InitializedChangedDelegate InitializedChanged;

        private bool initialized;

        public bool Initialized
        {
            get { return initialized; }
            protected set
            {
                if (value == initialized)
                    return;
                bool oldState = initialized;
                initialized = value;
                InitializedChanged?.Invoke(this, oldState, value);
                RaisePropertyChanged(nameof(Initialized));
            }
        }

        public abstract void Init();
        public abstract void DeInit();

        public override void Restored()
        { }

        protected override void afterUpdate()
        {
            base.afterUpdate();
            UmdPortDatabase.Instance.ItemUpdated(this);
        }

    }
}
