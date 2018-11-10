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

        private const string TITLE_NEW = "New UMD";
        private const string TITLE_EDIT = "Edit UMD: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New UMD";
        private const string HEADER_TEXT_EDIT = "Edit UMD";

        protected UMD umd;

        bool addingNew = false;

        protected bool AddingNew
        {
            get { return addingNew; }
            set
            {
                addingNew = value;
                Text = string.Format((value ? TITLE_NEW : TITLE_EDIT), umd?.ID, umd?.Name);
                HeaderText = string.Format((value ? HEADER_TEXT_NEW : HEADER_TEXT_EDIT), umd?.ID, umd?.Name);
            }
        }

        public UmdEditorFormBase()
        {
            InitializeComponent();
        }

        public UmdEditorFormBase(UMD umd)
        {
            InitializeComponent();
            AddingNew = (umd == null);
            if (umd != null) { 
                this.umd = umd;
                umd.CurrentTextChanged += umdCurrentTextChangedHandler;
            }
        }

        protected override void loadData()
        {

            if (umd == null)
                return;

            idNumericField.Value = (addingNew ? UmdDatabase.Instance.NextValidId() : umd.ID);
            nameTextBox.Text = umd.Name;
            currentTextTextBox.Text = umd.CurrentText;
            staticTextTextBox.Text = umd.StaticText;
            useStaticTextCheckBox.Checked = umd.UseStaticText;

            createTallySourceSettingTab();
            for (int i = 0; i < umd.Type.TallyCount; i++)
                tallySourceDropDowns[i]?.SelectByValue(umd.GetTallySource(i));

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

            umd.StartUpdate();
            writeFields();
            umd.StopUpdate();

            if (addingNew)
                UmdDatabase.Instance.Add(umd);
            AddingNew = false;

            return true;

        }

        protected virtual void validateFields()
        {
            if (umd == null)
                return;
            umd.ValidateId((int)idNumericField.Value);
            umd.ValidateName(nameTextBox.Text);
        }

        protected virtual void writeFields()
        {

            if (umd == null)
                return;

            umd.ID = (int)idNumericField.Value;
            umd.Name = nameTextBox.Text;
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
