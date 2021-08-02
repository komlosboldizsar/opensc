using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public class ExternalSignalCategory : ModelBase
    {

        #region Property: ID
        public delegate void IdChangedDelegate(ExternalSignalCategory category, int oldValue, int newValue);
        public event IdChangedDelegate IdChanged;

        public int id = 0;

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
            if (!ExternalSignalDatabases.Categories.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Name
        public delegate void NameChangedDelegate(ExternalSignalCategory category, string oldName, string newName);
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
        #endregion

        #region Property: Color
        public delegate void ColorChangedDelegate(ExternalSignalCategory category, Color oldColor, Color newColor);
        public event ColorChangedDelegate ColorChanged;

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
                color = value;
                ColorChanged?.Invoke(this, oldColor, value);
                RaisePropertyChanged(nameof(Color));
            }
        }
        #endregion

    }

}
