using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.General;
using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.Macros
{
    [WindowTypeName("macros.macropanel")]
    public partial class MacroPanelForm : ChildWindowWithTitle
    {

        private const string TITLE_TEXT = "[Macro panel] {1}";

        protected MacroPanel _macroPanelTempRef;

        protected MacroPanel macroPanel;

        public MacroPanelForm()
        {
            InitializeComponent();
        }

        public MacroPanelForm(MacroPanel macroPanel)
        {
            InitializeComponent();
            this._macroPanelTempRef = macroPanel;
        }

        #region Elements
        private Dictionary<MacroPanelElement, MacroPanelElementButton> elements = new Dictionary<MacroPanelElement, MacroPanelElementButton>();

        private void loadElements()
        {
            elements.Clear();
            elementsPanel.Controls.Clear();
            foreach (MacroPanelElement element in macroPanel.Elements)
                addButtonForElement(element);
        }

        private MacroPanelElementButton addButtonForElement(MacroPanelElement element)
        {
            MacroPanelElementButton elementButton = new MacroPanelElementButton(element);
            elementButton.ButtonClick += ElementButton_ButtonClick;
            elements.Add(element, elementButton);
            elementsPanel.Controls.Add(elementButton);
            return elementButton;
        }

        private void ElementButton_ButtonClick(MacroPanelElementButton sender)
        {
            sender.RunMacro();
        }
        #endregion

        private void resizeElementsPanel()
        {
            Size elementsPanelOriginalSize = elementsPanel.Size;
            Size elementsPanelNewSize = new Size(macroPanel.Width, macroPanel.Height);
            Size difference = elementsPanelNewSize - elementsPanelOriginalSize;
            Size += difference;
        }

        private void MacroPanelForm_Load(object sender, EventArgs e)
        {
            macroPanel = _macroPanelTempRef;
            if (macroPanel == null)
                return;
            Text = string.Format(TITLE_TEXT, macroPanel.ID, macroPanel.Name);
            HeaderText = macroPanel.Name;
            loadElements();
            resizeElementsPanel();
            macroPanel.Elements.ItemsAdded += elementsChangedHandler;
            macroPanel.Elements.ItemsRemoved += elementsChangedHandler;
        }


        private void MacroPanelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            macroPanel.Elements.ItemsAdded -= elementsChangedHandler;
            macroPanel.Elements.ItemsRemoved -= elementsChangedHandler;
        }


        private void elementsChangedHandler(IEnumerable<IObservableEnumerable<MacroPanelElement>.ItemWithPosition> affectedItemsWithPositions) => loadElements();

        #region Persistence
        private const string PERSISTENCE_KEY_MACRO_PANEL_ID = "macro_panel_id";

        protected override void restoreBeforeOpen(Dictionary<string, object> keyValuePairs)
        {
            base.restoreBeforeOpen(keyValuePairs);
            _macroPanelTempRef = MacroPanelDatabase.Instance.GetTById((int)keyValuePairs[PERSISTENCE_KEY_MACRO_PANEL_ID]);
        }

        public override Dictionary<string, object> GetKeyValuePairs()
        {
            var dict = base.GetKeyValuePairs();
            dict.Add(PERSISTENCE_KEY_MACRO_PANEL_ID, macroPanel?.ID);
            return dict;
        }
        #endregion

    }
}
