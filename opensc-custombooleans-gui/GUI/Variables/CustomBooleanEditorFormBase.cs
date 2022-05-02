using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Variables
{

    public partial class CustomBooleanEditorFormBase : ModelEditorFormBase
    {

        public CustomBooleanEditorFormBase() : base() => InitializeComponent();
        public CustomBooleanEditorFormBase(CustomBoolean customBoolean) : base(customBoolean) => InitializeComponent();

        protected override void loadData()
        {
            base.loadData();
            CustomBoolean customBoolean = (CustomBoolean)EditedModel;
            if (customBoolean == null)
                return;
            identifierTextBox.ReadOnly = !customBoolean.IdentifierUserEditable;
            descriptionTextBox.ReadOnly = !customBoolean.DescriptionUserEditable;
            colorButton.Text = customBoolean.ColorUserEditable ? "set" : string.Empty;
            identifierTextBox.Text = customBoolean.Identifier;
            descriptionTextBox.Text = customBoolean.Description;
            setColorButtonColor(customBoolean.Color);
        }

        protected override void validateFields()
        {
            base.validateFields();
            CustomBoolean customBoolean = (CustomBoolean)EditedModel;
            if (customBoolean == null)
                return;
        }

        protected override void writeFields()
        {
            base.writeFields();
            CustomBoolean customBoolean = (CustomBoolean)EditedModel;
            if (customBoolean == null)
                return;
            if (customBoolean.IdentifierUserEditable)
                customBoolean.Identifier = identifierTextBox.Text;
            if (customBoolean.DescriptionUserEditable)
                customBoolean.Description = descriptionTextBox.Text;
            if (customBoolean.ColorUserEditable)
                customBoolean.Color = colorButton.BackColor;
        }

        private void setColorButtonColor(Color color)
        {
            colorButton.BackColor = color;
            colorButton.ForeColor = (color.GetBrightness() > 0.5f) ? Color.Black : Color.White;
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            CustomBoolean customBoolean = (CustomBoolean)EditedModel;
            if ((customBoolean == null) || !customBoolean.ColorUserEditable)
                return;
            colorDialog.Color = colorButton.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
                setColorButtonColor(colorDialog.Color);
        }

        protected void updateNonUserEditableFields()
        {
            CustomBoolean customBoolean = (CustomBoolean)EditedModel;
            if (customBoolean == null)
                return;
            CustomBooleanDataStore dataStore = getDataStore();
            if (!customBoolean.IdentifierUserEditable)
                identifierTextBox.Text = customBoolean.GetIdentifierByData(dataStore);
            if (!customBoolean.DescriptionUserEditable)
                descriptionTextBox.Text = customBoolean.GetDescriptionByData(dataStore);
            if (!customBoolean.ColorUserEditable)
                setColorButtonColor(customBoolean.GetColorByData(dataStore));
        }

        protected virtual CustomBooleanDataStore getDataStore(CustomBooleanDataStore dataStore = null)
        {
            if (dataStore == null)
                dataStore = new CustomBooleanDataStore();
            return dataStore;
        }

    }

}
