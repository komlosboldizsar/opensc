using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{

    public delegate void UmdIdChangingDelegate(UMD timer, int oldValue, int newValue);
    public delegate void UmdIdChangedDelegate(UMD timer, int oldValue, int newValue);

    public delegate void UmdTextChanging(UMD umd, string oldText, string newText);
    public delegate void UmdTextChanged(UMD umd, string oldText, string newText);

    public delegate void UmdTallyChanging(UMD umd, int index, bool oldState, bool newState);
    public delegate void UmdTallyChanged(UMD umd, int index, bool oldState, bool newState);

    public abstract class UMD
    {

        public abstract string Text { get; set; }
        public abstract void SetTally(int index, bool state);

        public event UmdTextChanging TextChanging;
        public event UmdTextChanged TextChanged;

        public event UmdTallyChanging TallyChanging;
        public event UmdTallyChanged TallyChanged;

        public event UmdIdChangingDelegate IdChanging;
        public event UmdIdChangedDelegate IdChanged;

        public int id = 0;

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

    }
}
