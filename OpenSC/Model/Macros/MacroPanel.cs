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

        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            foreach (MacroPanelElement element in elements)
                element.Restored();
        }

        #region Property: ID
        public event PropertyChangedTwoValuesDelegate<MacroPanel, int> IdChanged;

        public int id = 0;

        public override int ID
        {
            get => id;
            set => setProperty(this, ref id, value, IdChanged, validator: ValidateId);
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            if (!MacroPanelDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Name
        public event PropertyChangedTwoValuesDelegate<MacroPanel, string> NameChanged;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get => name;
            set => setProperty(this, ref name, value, NameChanged, validator: ValidateName);
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Width
        public event PropertyChangedTwoValuesDelegate<MacroPanel, int> SizeWChanged;

        [PersistAs("width")]
        private int width;

        public int SizeW
        {
            get => width;
            set => setProperty(this, ref width, value, SizeWChanged);
        }
        #endregion

        #region Property: Height
        public event PropertyChangedTwoValuesDelegate<MacroPanel, int> SizeHChanged;

        [PersistAs("height")]
        private int height;

        public int SizeH
        {
            get => height;
            set => setProperty(this, ref height, value, SizeHChanged);
        }
        #endregion

        #region Elements
        private ObservableList<MacroPanelElement> elements = new ObservableList<MacroPanelElement>();

        public ObservableList<MacroPanelElement> Elements => elements;

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
                foreach (var element in elements)
                    element.Parent = this;
            }
        }

        internal void ElementUpdated()
        { }

        public MacroPanelElement AddElement()
        {
            MacroPanelElement newElement = new MacroPanelElement();
            newElement.Parent = this;
            elements.Add(newElement);
            return newElement;
        }

        public void RemoveElement(MacroPanelElement element)
        {
            if (!elements.Contains(element))
                return;
            elements.Remove(element);
            element.Parent = null;
        }
        #endregion

    }

}
