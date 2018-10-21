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
            idNumericField.Value = umd.ID;
            nameTextBox.Text = umd.Name;
            currentTextTextBox.Text = umd.CurrentText;
            staticTextTextBox.Text = umd.StaticText;
            useStaticTextCheckBox.Checked = umd.UseStaticText;
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

            writeFields();
            if (addingNew)
            {
                UmdDatabase.Instance.Add(umd);
            }

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
        }

        private void umdCurrentTextChangedHandler(UMD umd, string oldText, string newText)
        {
            currentTextTextBox.Text = umd.CurrentText;
        }

    }
}
