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

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = MacroPanelDatabase.Instance;
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
        [PersistAs(null, 1)]
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
