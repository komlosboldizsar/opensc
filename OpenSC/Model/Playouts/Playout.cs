using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Playouts
{

    public delegate void PlayoutIdChangingDelegate(Playout playout, int oldValue, int newValue);
    public delegate void PlayoutIdChangedDelegate(Playout playout, int oldValue, int newValue);

    public class Playout: IModel
    {

        public virtual void Restored()
        { }

        public event PlayoutIdChangingDelegate IdChanging;
        public event PlayoutIdChangedDelegate IdChanged;
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
            if (!PlayoutDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

    }
}
