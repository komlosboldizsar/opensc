using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{

    public class MacroPanel : ModelBase
    {

        public override void Restored()
        {
            base.Restored();
            foreach (MacroPanelElement element in elements)
                element.Restored();
        }

        public delegate void IdChangedDelegate(MacroPanel panel, int oldValue, int newValue);
        public event IdChangedDelegate IdChanged;

        public int id = 0;

        public override int ID
        {
            get { return id; }
            set
            {
                ValidateId(value);
                if (value == id)
                    return;
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
            if (!MacroPanelDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

        public delegate void NameChangedDelegate(MacroPanel panel, string oldName, string newName);
        public event NameChangedDelegate NameChanged;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                ValidateName(value);
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

        [PersistAs("width")]
        public int SizeW { get; set; }

        [PersistAs("height")]
        public int SizeH { get; set; }

        #region Elements
        private ObservableList<MacroPanelElement> elements = new ObservableList<MacroPanelElement>();

        public ObservableList<MacroPanelElement> Elements
        {
            get { return elements; }
        }

        [PersistAs("elements")]
        [PersistAs("element", 1)]
        private MacroPanelElement[] _elements
        {
            get { return elements.ToArray(); }
            set
            {
                elements.Clear();
                if (value != null)
                    elements.AddRange(value);
            }
        }
        #endregion

    }

}
