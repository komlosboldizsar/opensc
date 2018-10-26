using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{

    public delegate void DynamicTextIdChangingDelegate(DynamicText text, int oldValue, int newValue);
    public delegate void DynamicTextIdChangedDelegate(DynamicText text, int oldValue, int newValue);

    public class DynamicText: IModel
    {

        public virtual void Restored()
        { }

        public event DynamicTextIdChangingDelegate IdChanging;
        public event DynamicTextIdChangedDelegate IdChanged;
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
            if (!DynamicTextDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

    }

}
