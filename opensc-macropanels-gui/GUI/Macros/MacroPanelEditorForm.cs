using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.GUI.Helpers;
using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Macros
{

    public partial class MacroPanelEditorForm : ModelEditorFormBase, IModelEditorForm<MacroPanel>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as MacroPanel);
        public IModelEditorForm<MacroPanel> GetInstanceT(MacroPanel modelInstance) => new MacroPanelEditorForm(modelInstance);

        public MacroPanelEditorForm() : base() => InitializeComponent();
        public MacroPanelEditorForm(MacroPanel macroPanel) : base(macroPanel) => InitializeComponent();

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<MacroPanel, MacroPanel>(this, MacroPanelDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            MacroPanel macroPanel = (MacroPanel)EditedModel;
            if (macroPanel == null)
                return;
            resizeElementsPanel();
            loadElements();
            SelectedElement = null;
        }

        protected override void validateFields()
        {
            base.validateFields();
            MacroPanel macroPanel = (MacroPanel)EditedModel;
            if (macroPanel == null)
                return;
        }

        protected override void writeFields()
        {
            base.writeFields();
            MacroPanel macroPanel = (MacroPanel)EditedModel;
            if (macroPanel == null)
                return;
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
            foreach (MacroPanelElement element in ((MacroPanel)EditedModel).Elements)
                addButtonForElement(element);
        }
        private void saveElements()
        {
            foreach (var element in elements)
                element.Value.SaveToModel();
        }

        private void addElement()
        {
            MacroPanelElement element = ((MacroPanel)EditedModel).AddElement();
            element.SizeW = DEFAULT_ELEMENT_WIDTH;
            element.SizeH = DEFAULT_ELEMENT_HEIGHT;
            MacroPanelElementButton elementButton = addButtonForElement(element);
            SelectedElement = elementButton;
        }

        private void removeElement(MacroPanelElementButton elementButton)
        {
            MacroPanelElement element = elementButton.Element;
            ((MacroPanel)EditedModel).RemoveElement(element);
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
        private void addElementButton_Click(object sender, EventArgs e) => addElement();

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
            => elementMacroDropDown.CreateAdapterAsDataSource(MacroDatabase.Instance, null, true, "-");

        private void MacroPanelForm_Load(object sender, EventArgs e)
        {
            loadMacros();
            elementMacroDropDown.ReceiveObjectDrop().FilterByType<Macro>();
            MacroDatabase.Instance.ChangedItems += MacroDatabase_ChangedItems;
        }

        private void MacroPanelForm_FormClosing(object sender, FormClosingEventArgs e)
            => MacroDatabase.Instance.ChangedItems -= MacroDatabase_ChangedItems;

        private void MacroDatabase_ChangedItems(Model.DatabaseBase<Macro> database)
            => loadMacros();

        private void resizeElementsPanel()
        {
            MacroPanel macroPanel = (MacroPanel)EditedModel;
            Size elementsPanelOriginalSize = elementsPanel.Size;
            Size elementsPanelNewSize = new Size(macroPanel.SizeW, macroPanel.SizeH);
            Size difference = elementsPanelNewSize - elementsPanelOriginalSize;
            Size += difference;
        }
  
    }

}
