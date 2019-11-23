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
    public partial class MacroPanelForm : ChildWindowWithTitle
    {

        private const string TITLE_TEXT = "[Macro panel] {1}";

        protected MacroPanel macroPanel;

        public MacroPanelForm()
        {
            InitializeComponent();
        }

        public MacroPanelForm(MacroPanel macroPanel)
        {
            InitializeComponent();
            this.macroPanel = macroPanel;
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
            Size elementsPanelNewSize = new Size(macroPanel.SizeW, macroPanel.SizeH);
            Size difference = elementsPanelNewSize - elementsPanelOriginalSize;
            Size += difference;
        }

        private void MacroPanelForm_Load(object sender, EventArgs e)
        {
            if (macroPanel == null)
                return;
            Text = string.Format(TITLE_TEXT, macroPanel.ID, macroPanel.Name);
            HeaderText = macroPanel.Name;
            loadElements();
            resizeElementsPanel();
            macroPanel.Elements.ItemsChanged += Elements_ItemsChanged;
        }

        private void MacroPanelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            macroPanel.Elements.ItemsChanged -= Elements_ItemsChanged;
        }

        private void Elements_ItemsChanged()
        {
            loadElements();
        }

    }
}
