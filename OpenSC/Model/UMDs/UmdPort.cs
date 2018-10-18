using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{

    public delegate void UmdPortIdChangingDelegate(UmdPort port, int oldValue, int newValue);
    public delegate void UmdPortIdChangedDelegate(UmdPort port, int oldValue, int newValue);

    abstract public class UmdPort: IModel
    {

        public event UmdPortIdChangingDelegate IdChanging;
        public event UmdPortIdChangedDelegate IdChanged;
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
            if (!UmdPortDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

        public abstract void Init();
        public abstract void DeInit();

        public virtual void Restored()
        {
        }

    }
}
