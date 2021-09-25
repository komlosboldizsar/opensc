using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.UMDs;
using OpenSC.Model.Variables;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.UMDs
{
    public partial class UmdEditorFormBase : ModelEditorFormBase
    {

        public UmdEditorFormBase() : base() => InitializeComponent();

        public UmdEditorFormBase(UMD umd) : base(umd)
        {
            InitializeComponent();
            umd.CurrentTextChanged += umdCurrentTextChangedHandler;
        }

        protected override void loadData()
        {
            base.loadData();
            UMD umd = (UMD)EditedModel;
            if (umd == null)
                return;
            currentTextTextBox.Text = umd.CurrentText;
            staticTextTextBox.Text = umd.StaticText;
            useStaticTextCheckBox.Checked = umd.UseStaticText;
            createTallySourceSettingTab();
            for (int i = 0; i < umd.Type.TallyCount; i++)
                tallySourceDropDowns[i]?.SelectByValue(umd.GetTallySource(i));
        }

        protected override void validateFields()
        {
            base.validateFields();
            UMD umd = (UMD)EditedModel;
            if (umd == null)
                return;
            umd.ValidateId((int)idNumericField.Value);
            umd.ValidateName(nameTextBox.Text);
        }

        protected override void writeFields()
        {
            base.writeFields();
            UMD umd = (UMD)EditedModel;
            if (umd == null)
                return;
            umd.StaticText = staticTextTextBox.Text;
            umd.UseStaticText = useStaticTextCheckBox.Checked;
            for (int i = 0; i < umd.Type.TallyCount; i++)
                umd.SetTallySource(i, tallySourceDropDowns[i]?.SelectedValue as IBoolean);
        }

        private void umdCurrentTextChangedHandler(UMD umd, string oldText, string newText)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => umdCurrentTextChangedHandler(umd, oldText, newText)));
                return;
            }
            currentTextTextBox.Text = newText;
        }

        private ComboBox[] tallySourceDropDowns = new ComboBox[UMD.MAX_TALLIES];

        private void createTallySourceSettingTab()
        {
            UMD umd = (UMD)EditedModel;
            talliesTable.Controls.Clear();
            if (umd.Type.TallyCount == 0)
                mainTabControl.TabPages.Remove(talliesTabPage);
            for (int i = 0; i < umd.Type.TallyCount; i++)
                addRowToTallySourceSettingTable(i);
        }

        private void addRowToTallySourceSettingTable(int index)
        {

            Label label = new Label();
            label.Text = string.Format("Tally #{0} source", (index + 1));
            label.TextAlign = ContentAlignment.MiddleLeft;
            label.Dock = DockStyle.Left;
            label.Margin = new Padding(3, 0, 15, 0);
            talliesTable.Controls.Add(label, 0, index);

            ComboBox comboBox = new ComboBox();
            comboBox.Width = 280;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            talliesTable.Controls.Add(comboBox, 1, index);

            comboBox.CreateAdapterAsDataSource(BooleanRegister.Instance, b => string.Format("{0}: {1}", b.Name, b.Description), true, "(not set)");

            tallySourceDropDowns[index] = comboBox;

        }

    }

}
