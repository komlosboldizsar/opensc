using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{

    public partial class MacroPanel : ModelBase
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
        [AutoProperty]
        [PersistAs("width")]
        private int width;
        #endregion

        #region Property: Height
        [AutoProperty]
        [PersistAs("height")]
        private int height;
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

        internal void ElementUpdated() { }

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
