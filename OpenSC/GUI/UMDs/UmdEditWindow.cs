using OpenSC.Model.Timers;
using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.TSL31;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.UMDs
{
    public partial class UmdEditWindow : ChildWindowWithTitle
    {

        private const string TITLE_NEW = "New UMD";
        private const string TITLE_EDIT = "Edit UMD: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New UMD";
        private const string HEADER_TEXT_EDIT = "Edit UMD";

        private UMD umd;
        bool addingNew = false;

        public UmdEditWindow(UMD umd)
        {

            InitializeComponent();

            if (umd == null)
            {
                this.umd = new TSL31(); // TODO
                addingNew = true;
                HeaderText = HEADER_TEXT_NEW;
                Text = string.Format(TITLE_NEW, 0, "");
            }
            else
            {
                this.umd = umd;
                HeaderText = HEADER_TEXT_EDIT;
                Text = string.Format(TITLE_EDIT, umd.ID, umd.Name);
            }

        }

        private void UmdEditWindow_Load(object sender, EventArgs e)
        {
            loadUmd();
            umd.CurrentTextChanged += umdCurrentTextChangedHandler;
        }

        private void loadUmd()
        {
            idNumericField.Value = umd.ID;
            nameTextBox.Text = umd.Name;
            currentTextTextBox.Text = umd.CurrentText;
            staticTextTextBox.Text = umd.StaticText;
            useStaticTextCheckBox.Checked = umd.UseStaticText;
        }

        private bool saveUmd()
        {

            try
            {
                umd.ValidateId((int)idNumericField.Value);
                umd.ValidateName(nameTextBox.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Data validation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            umd.ID = (int)idNumericField.Value;
            umd.Name = nameTextBox.Text;
            umd.StaticText = staticTextTextBox.Text;
            umd.UseStaticText = useStaticTextCheckBox.Checked;

            if (addingNew)
            {
                UmdDatabase.Instance.Add(umd);
                addingNew = false;
                HeaderText = HEADER_TEXT_EDIT;
                
            }

            Text = string.Format(TITLE_EDIT, umd.ID, umd.Name);

            return true;

        }

        private void saveAndCloseButton_Click(object sender, EventArgs e)
        {
            if (saveUmd())
                Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveUmd();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void umdCurrentTextChangedHandler(UMD umd, string oldText, string newText)
        {
            currentTextTextBox.Text = umd.CurrentText;
        }

    }
}
