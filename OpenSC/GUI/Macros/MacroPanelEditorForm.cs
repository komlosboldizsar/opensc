using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers;
using OpenSC.Model.Macros;
using OpenSC.Model.Routers;
using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Macros
{

    public partial class MacroPanelEditorForm : ModelEditorFormBase
    {

        private const string TITLE_NEW = "New macro panel";
        private const string TITLE_EDIT = "Edit macro panel: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New macro panel";
        private const string HEADER_TEXT_EDIT = "Edit macro panel";

        protected MacroPanel macroPanel;

        private bool addingNew = false;

        protected bool AddingNew
        {
            get { return addingNew; }
            set
            {
                addingNew = value;
                Text = string.Format((value ? TITLE_NEW : TITLE_EDIT), macroPanel?.ID, macroPanel?.Name);
                HeaderText = string.Format((value ? HEADER_TEXT_NEW : HEADER_TEXT_EDIT), macroPanel?.ID, macroPanel?.Name);
            }
        }

        public MacroPanelEditorForm()
        {
            InitializeComponent();
        }

        public MacroPanelEditorForm(MacroPanel macroPanel)
        {
            InitializeComponent();
            this.macroPanel = (macroPanel != null) ? macroPanel : new MacroPanel();
            AddingNew = (macroPanel == null);
        }

        protected override void loadData()
        {
            if (macroPanel == null)
                return;
            idNumericField.Value = (addingNew ? MacroPanelDatabase.Instance.NextValidId() : macroPanel.ID);
            nameTextBox.Text = macroPanel.Name;
            resizeElementsPanel();
            loadElements();
            SelectedElement = null;
        }
        protected sealed override bool saveData()
        {

            try
            {
                validateFields();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Data validation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            macroPanel.StartUpdate();
            writeFields();
            macroPanel.EndUpdate();

            if (addingNew)
                MacroPanelDatabase.Instance.Add(macroPanel);
            AddingNew = false;

            return true;

        }

        protected virtual void validateFields()
        {
            if (macroPanel == null)
                return;
            macroPanel.ValidateId((int)idNumericField.Value);
            macroPanel.ValidateName(nameTextBox.Text);
        }

        protected virtual void writeFields()
        {
            if (macroPanel == null)
                return;
            macroPanel.ID = (int)idNumericField.Value;
            macroPanel.Name = nameTextBox.Text;
            macroPanel.SizeW = elementsPanel.Width;
            macroPanel.SizeH = elementsPanel.Height;
            saveElements();
        }

        #region Elements
        private Dictionary<MacroPanelElement, MacroPanelElementButton> elements = new Dictionary<MacroPanelElement, MacroPanelElementButton>();

        private MacroPanelElementButton selectedElement;

        private MacroPanelElementButton SelectedElement
        {
            get => selectedElement;
            set
            {

                selectedElement = value;
                if (selectedElement == null)
                {
                    elementDataPanel.Visible = false;
                    return;
                }

                elementDataPanel.Visible = true;
                elementMacroDropDown.SelectByValue(selectedElement.Macro);
                elementLabelTextBox.Text = selectedElement.Label;
                elementLabelTextBox.Enabled = selectedElement.ShowLabel;
                elementShowLabelCheckBox.Checked = selectedElement.ShowLabel;
                foregroundColorPanel.BackColor = selectedElement.ElementForeColor;
                backgroundColorPanel.BackColor = selectedElement.ElementBackColor;
                elementPosXNumericField.Value = selectedElement.Location.X;
                elementPosYNumericField.Value = selectedElement.Location.Y;
                elementSizeWNumericField.Value = selectedElement.Width;
                elementSizeHNumericField.Value = selectedElement.Height;

            }
        }

        private const int DEFAULT_ELEMENT_WIDTH = 60;
        private const int DEFAULT_ELEMENT_HEIGHT = 40;

        private void loadElements()
        {
            elements.Clear();
            elementsPanel.Controls.Clear();
            foreach (MacroPanelElement element in macroPanel.Elements)
                addButtonForElement(element);
        }
        private void saveElements()
        {
            foreach (var element in elements)
                element.Value.SaveToModel();
        }

        private void addElement()
        {
            MacroPanelElement element = macroPanel.AddElement();
            element.SizeW = DEFAULT_ELEMENT_WIDTH;
            element.SizeH = DEFAULT_ELEMENT_HEIGHT;
            MacroPanelElementButton elementButton = addButtonForElement(element);
            SelectedElement = elementButton;
        }

        private void removeElement(MacroPanelElementButton elementButton)
        {
            MacroPanelElement element = elementButton.Element;
            macroPanel.RemoveElement(element);
            elements.Remove(element);
            elementsPanel.Controls.Remove(elementButton);
            if (SelectedElement == elementButton)
                SelectedElement = null;
        }

        private MacroPanelElementButton addButtonForElement(MacroPanelElement element)
        {
            MacroPanelElementButton elementButton = new MacroPanelElementButton(element);
            elementButton.ButtonRightClick += ElementButton_ButtonRightClick;
            elementButton.LocationChanged += ElementButton_LocationChanged;
            elementButton.SizeChanged += ElementButton_SizeChanged;
            elements.Add(element, elementButton);
            elementsPanel.Controls.Add(elementButton);
            elementButton.Resizable();
            elementButton.Draggable(true);
            return elementButton;
        }

        private void ElementButton_ButtonRightClick(MacroPanelElementButton sender)
        {
            SelectedElement = sender;
        }

        private void ElementButton_SizeChanged(object sender, EventArgs e)
        {
            if (sender != SelectedElement)
                return;
            elementSizeWNumericField.Value = selectedElement.Width;
            elementSizeHNumericField.Value = selectedElement.Height;
        }

        private void ElementButton_LocationChanged(object sender, EventArgs e)
        {
            if (sender != SelectedElement)
                return;
            elementPosXNumericField.Value = selectedElement.Location.X;
            elementPosYNumericField.Value = selectedElement.Location.Y;
        }
        #endregion

        #region Edit panel handlers, etc.

        private void addElementButton_Click(object sender, EventArgs e)
        {
            addElement();
        }

        private void removeSelectedElementButton_Click(object sender, EventArgs e)
        {
            if (SelectedElement == null)
                return;
            removeElement(SelectedElement);
        }
        #endregion

        #region Element editor fields event handlers
        private void elementMacroDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedElement == null)
                return;
            SelectedElement.Macro = elementMacroDropDown.SelectedValue as Macro;
        }

        private void elementLabelTextBox_TextChanged(object sender, EventArgs e)
        {
            if (SelectedElement == null)
                return;
            SelectedElement.Label = elementLabelTextBox.Text;
        }

        private void showLabelCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectedElement == null)
                return;
            SelectedElement.ShowLabel = elementShowLabelCheckBox.Checked;
            elementLabelTextBox.Enabled = elementShowLabelCheckBox.Checked;
        }

        private void elementPosXNumericField_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedElement == null)
                return;
            SelectedElement.Location = new System.Drawing.Point((int)elementPosXNumericField.Value, SelectedElement.Location.Y);
        }

        private void elementPosYNumericField_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedElement == null)
                return;
            SelectedElement.Location = new System.Drawing.Point(SelectedElement.Location.X, (int)elementPosYNumericField.Value);
        }

        private void elementSizeWNumericField_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedElement == null)
                return;
            SelectedElement.Width = (int)elementSizeWNumericField.Value;
        }

        private void elementSizeHNumericField_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedElement == null)
                return;
            SelectedElement.Height = (int)elementSizeHNumericField.Value;
        }

        private void pickForegroundColorButton_Click(object sender, EventArgs e)
        {
            colorPickerDialog.Color = SelectedElement.ForeColor;
            if (colorPickerDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedElement.ElementForeColor = colorPickerDialog.Color;
                foregroundColorPanel.BackColor = colorPickerDialog.Color;
            }
        }

        private void pickBackgroundColorButton_Click(object sender, EventArgs e)
        {
            colorPickerDialog.Color = SelectedElement.BackColor;
            if (colorPickerDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedElement.ElementBackColor = colorPickerDialog.Color;
                backgroundColorPanel.BackColor = colorPickerDialog.Color;
            }
        }
        #endregion

        private void loadMacros()
        {
            elementMacroDropDown.CreateAdapterAsDataSource(MacroDatabase.Instance, m => m.Name, true, "-");
        }

        private void MacroPanelForm_Load(object sender, EventArgs e)
        {
            loadMacros();
            MacroDatabase.Instance.ChangedItems += MacroDatabase_ChangedItems;
        }

        private void MacroPanelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MacroDatabase.Instance.ChangedItems -= MacroDatabase_ChangedItems;
        }

        private void MacroDatabase_ChangedItems(Model.DatabaseBase<Macro> database)
        {
            loadMacros();
        }

        private void resizeElementsPanel()
        {
            Size elementsPanelOriginalSize = elementsPanel.Size;
            Size elementsPanelNewSize = new Size(macroPanel.SizeW, macroPanel.SizeH);
            Size difference = elementsPanelNewSize - elementsPanelOriginalSize;
            Size += difference;
        }
  
    }

}
