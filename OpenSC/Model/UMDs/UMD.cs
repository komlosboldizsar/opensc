using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{

    public delegate void UmdIdChangingDelegate(UMD timer, int oldValue, int newValue);
    public delegate void UmdIdChangedDelegate(UMD timer, int oldValue, int newValue);

    public delegate void UmdNameChangingDelegate(UMD timer, string oldName, string newName);
    public delegate void UmdNameChangedDelegate(UMD timer, string oldName, string newValue);

    public delegate void UmdTextChanging(UMD umd, string oldText, string newText);
    public delegate void UmdTextChanged(UMD umd, string oldText, string newText);

    public delegate void UmdUseStaticTextChanging(UMD umd, bool oldState, bool newState);
    public delegate void UmdUseStaticTextChanged(UMD umd, bool oldState, bool newState);

    public delegate void UmdTallyChanging(UMD umd, int index, bool oldState, bool newState);
    public delegate void UmdTallyChanged(UMD umd, int index, bool oldState, bool newState);

    public abstract class UMD: IModel
    {

        public abstract IUMDType Type { get; }
        public abstract void SetTally(int index, bool state);
        public abstract bool[] TallyStates { get; }
        public abstract Color[] TallyColors { get; }

        protected abstract void update();

        public virtual void Restored()
        { }

        public UMD()
        { }

        public event UmdTextChanging CurrentTextChanging;
        public event UmdTextChanged CurrentTextChanged;
        public event ParameterlessChangeNotifierDelegate CurrentTextChangingPCN;
        public event ParameterlessChangeNotifierDelegate CurrentTextChangedPCN;

        protected string currentText;

        public string CurrentText
        {
            get { return currentText; }
            set
            {
                string oldValue = currentText;
                CurrentTextChanging?.Invoke(this, oldValue, value);
                CurrentTextChangingPCN?.Invoke();
                currentText = value;
                update();
                CurrentTextChanged?.Invoke(this, oldValue, value);
                CurrentTextChangedPCN?.Invoke();
            }
        }

        private string dynamicText;
        
        protected string DynamicText
        {
            get { return dynamicText; }
            set
            {
                dynamicText = value;
                if (!useStaticText)
                    CurrentText = value;
            }
        }

        public event UmdTextChanging StaticTextChanging;
        public event UmdTextChanged StaticTextChanged;
        public event ParameterlessChangeNotifierDelegate StaticTextChangingPCN;
        public event ParameterlessChangeNotifierDelegate StaticTextChangedPCN;

        [PersistAs("static_text")]
        private string staticText;

        public string StaticText
        {
            get { return staticText; }
            set
            {
                string oldValue = staticText;
                StaticTextChanging?.Invoke(this, oldValue, value);
                StaticTextChangingPCN?.Invoke();
                staticText = value;
                if (useStaticText)
                    CurrentText = value;
                StaticTextChanged?.Invoke(this, oldValue, value);
                StaticTextChangedPCN?.Invoke();
            }
        }

        public event UmdUseStaticTextChanging UseStaticTextChanging;
        public event UmdUseStaticTextChanged UseStaticTextChanged;
        public event ParameterlessChangeNotifierDelegate UseStaticTextChangingPCN;
        public event ParameterlessChangeNotifierDelegate UseStaticTextChangedPCN;

        [PersistAs("use_static_text")]
        private bool useStaticText = false;

        public bool UseStaticText
        {
            get { return useStaticText; }
            set
            {
                bool oldValue = useStaticText;
                if (oldValue == value)
                    return;
                UseStaticTextChanging?.Invoke(this, oldValue, value);
                UseStaticTextChangingPCN?.Invoke();
                useStaticText = value;
                CurrentText = useStaticText ? staticText : dynamicText;
                UseStaticTextChanged?.Invoke(this, oldValue, value);
                UseStaticTextChangedPCN?.Invoke();
            }
        }

        public abstract event UmdTallyChanging TallyChanging;
        public abstract event UmdTallyChanged TallyChanged;
        public abstract event ParameterlessChangeNotifierDelegate TallyChangingPCN;
        public abstract event ParameterlessChangeNotifierDelegate TallyChangedPCN;

        // ...

        public event UmdIdChangingDelegate IdChanging;
        public event UmdIdChangedDelegate IdChanged;
        public event ParameterlessChangeNotifierDelegate IdChangingPCN;
        public event ParameterlessChangeNotifierDelegate IdChangedPCN;

        private int id = 0;

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
            if (!UmdDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

        public event UmdNameChangingDelegate NameChanging;
        public event UmdNameChangedDelegate NameChanged;
        public event ParameterlessChangeNotifierDelegate NameChangingPCN;
        public event ParameterlessChangeNotifierDelegate NameChangedPCN;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                ValidateName(value);
                string oldValue = name;
                NameChanging?.Invoke(this, oldValue, value);
                NameChangingPCN?.Invoke();
                name = value;
                NameChanged?.Invoke(this, oldValue, value);
                NameChangedPCN?.Invoke();
            }

        }

        public void ValidateName(string name)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentException();
        }

    }
}
