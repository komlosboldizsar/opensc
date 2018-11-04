using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers
{

    public delegate void MixerIdChangingDelegate(Mixer mixer, int oldValue, int newValue);
    public delegate void MixerIdChangedDelegate(Mixer mixer, int oldValue, int newValue);

    public abstract class Mixer : IModel
    {

        public Mixer()
        { }

        public virtual void Restored()
        { }

        public event MixerIdChangingDelegate IdChanging;
        public event MixerIdChangedDelegate IdChanged;
        public event ParameterlessChangeNotifierDelegate IdChangingPCN;
        public event ParameterlessChangeNotifierDelegate IdChangedPCN;

        public int id = 0;

        public int ID
        {
            get { return id; }
            set
            {
                ValidateId(value);
                if (value == id)
                    return;
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
            if (!MixerDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

    }

}
