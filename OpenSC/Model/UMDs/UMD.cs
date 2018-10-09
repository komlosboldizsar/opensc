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

    public abstract class UMD
    {

        public abstract IUMDType Type { get; }
        public abstract string Text { get; set; }
        public abstract void SetTally(int index, bool state);
        public abstract bool[] TallyStates { get; }
        public abstract Color[] TallyColors { get; }

        public event UmdTextChanging CurrentTextChanging;
        public event UmdTextChanged CurrentTextChanged;

        private string currentText = "fasz";

        public string CurrentText
        {
            get { return currentText; }
            set
            {
                string oldValue = currentText;
                CurrentTextChanging?.Invoke(this, oldValue, value);
                currentText = value;
                // Send to UMD
                CurrentTextChanged?.Invoke(this, oldValue, value);
            }
        }

        private string dynamicText = "DYNAMIC";
        
        private string DynamicText
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

        private string staticText = "STATIC";

        public string StaticText
        {
            get { return staticText; }
            set
            {
                string oldValue = staticText;
                StaticTextChanging?.Invoke(this, oldValue, value);
                staticText = value;
                StaticTextChanged?.Invoke(this, oldValue, value);
            }
        }

        public event UmdUseStaticTextChanging UseStaticTextChanging;
        public event UmdUseStaticTextChanged UseStaticTextChanged;

        private bool useStaticText = false;

        public bool UseStaticText
        {
            get { return useStaticText; }
            set
            {
                bool oldValue = useStaticText;
                UseStaticTextChanging?.Invoke(this, oldValue, value);
                useStaticText = value;
                CurrentText = useStaticText ? staticText : dynamicText;
                UseStaticTextChanged?.Invoke(this, oldValue, value);
            }
        }

        public event UmdTallyChanging TallyChanging;
        public event UmdTallyChanged TallyChanged;

        // ...

        public event UmdIdChangingDelegate IdChanging;
        public event UmdIdChangedDelegate IdChanged;

        private int id = 0;

        public int ID
        {
            get { return id; }
            set
            {
                ValidateId(value);
                int oldValue = id;
                IdChanging?.Invoke(this, oldValue, value);
                id = value;
                IdChanged?.Invoke(this, oldValue, value);
            }
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            if (!UmdDatabase.Instance.IsIdValidForUmd(id, this))
                throw new ArgumentException();
        }

        public event UmdNameChangingDelegate NameChanging;
        public event UmdNameChangedDelegate NameChanged;

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                string oldValue = name;
                NameChanging?.Invoke(this, oldValue, value);
                name = value;
                NameChanged?.Invoke(this, oldValue, value);
            }

        }
    }
}
