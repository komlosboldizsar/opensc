using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{


    public delegate void SignalCategoryIdChangingDelegate(SignalCategory category, int oldValue, int newValue);
    public delegate void SignalCategoryIdChangedDelegate(SignalCategory category, int oldValue, int newValue);

    public delegate void SignalCategoryNameChangingDelegate(SignalCategory category, string oldName, string newName);
    public delegate void SignalCategoryNameChangedDelegate(SignalCategory category, string oldName, string newName);

    public delegate void SignalCategoryColorChangingDelegate(SignalCategory category, Color oldColor, Color newColor);
    public delegate void SignalCategoryColorChangedDelegate(SignalCategory category, Color oldColor, Color newColor);

    public class SignalCategory : ModelBase
    {

        public override void Restored()
        { }

        public event SignalCategoryIdChangingDelegate IdChanging;
        public event SignalCategoryIdChangedDelegate IdChanged;
        public event ParameterlessChangeNotifierDelegate IdChangingPCN;
        public event ParameterlessChangeNotifierDelegate IdChangedPCN;

        public int id = 0;

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
            if (!SignalDatabases.Categories.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }


        public event SignalCategoryNameChangingDelegate NameChanging;
        public event SignalCategoryNameChangedDelegate NameChanged;
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

        public event SignalCategoryColorChangingDelegate ColorChanging;
        public event SignalCategoryColorChangedDelegate ColorChanged;
        public event ParameterlessChangeNotifierDelegate ColorChangingPCN;
        public event ParameterlessChangeNotifierDelegate ColorChangedPCN;

        [PersistAs("color")]
        private Color color;

        public Color Color
        {
            get { return color; }
            set
            {
                if (value == color)
                    return;
                Color oldColor = color;
                ColorChanging?.Invoke(this, oldColor, value);
                ColorChangingPCN?.Invoke();
                color = value;
                ColorChanged?.Invoke(this, oldColor, value);
                ColorChangedPCN?.Invoke();
            }
        }

    }

}
