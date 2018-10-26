using OpenSC.Model.Variables;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Variables
{
    public partial class DynamicTextEditorForm : ModelEditorFormBase
    {

        private const string TITLE_NEW = "New dynamic text";
        private const string TITLE_EDIT = "Edit dynamic text: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New dynamic text";
        private const string HEADER_TEXT_EDIT = "Edit dynamic text";

        protected DynamicText dyntext;

        bool addingNew = false;

        protected bool AddingNew
        {
            get { return addingNew; }
            set
            {
                addingNew = value;
                Text = string.Format((value ? TITLE_NEW : TITLE_EDIT), dyntext?.ID, dyntext?.Label);
                HeaderText = string.Format((value ? HEADER_TEXT_NEW : HEADER_TEXT_EDIT), dyntext?.ID, dyntext?.Label);
            }
        }

        public DynamicTextEditorForm()
        {
            InitializeComponent();
        }

        public DynamicTextEditorForm(DynamicText dyntext)
        {
            InitializeComponent();
            AddingNew = (dyntext == null);
            this.dyntext = (dyntext != null) ? dyntext : new DynamicText();
            this.dyntext.CurrentTextChanged += dynamicTextCurrentTextChangedHandler;
        }

        protected override void loadData()
        {
            if (dyntext == null)
                return;
            idNumericField.Value = dyntext.ID;
            labelTextBox.Text = dyntext.Label;
            formulaTextBox.Text = dyntext.Formula;
            currentTextTextBox.Text = dyntext.CurrentText;
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
                DynamicTextDatabase.Instance.Add(dyntext);
            }

            AddingNew = false;

            return true;

        }

        protected virtual void validateFields()
        {
            if (dyntext == null)
                return;
            dyntext.ValidateId((int)idNumericField.Value);
            dyntext.ValidateLabel(labelTextBox.Text);
        }

        protected virtual void writeFields()
        {
            if (dyntext == null)
                return;
            dyntext.ID = (int)idNumericField.Value;
            dyntext.Label = labelTextBox.Text;
            dyntext.Formula = formulaTextBox.Text;
        }

        private void dynamicTextCurrentTextChangedHandler(DynamicText dyntext, string oldText, string newText)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => dynamicTextCurrentTextChangedHandler(dyntext, oldText, newText)));
                return;
            }
            if (dyntext == this.dyntext)
                currentTextTextBox.Text = newText;
        }

    }
}
