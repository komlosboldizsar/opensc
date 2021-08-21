using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.Signals;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Signals
{

    public partial class ExternalSignalEditorForm : ModelEditorFormBase
    {

        private const string TITLE_NEW = "New external signal";
        private const string TITLE_EDIT = "Edit external signal: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New external signal";
        private const string HEADER_TEXT_EDIT = "Edit external signal";

        protected ExternalSignal signal;

        bool addingNew = false;

        protected bool AddingNew
        {
            get { return addingNew; }
            set
            {
                addingNew = value;
                Text = string.Format((value ? TITLE_NEW : TITLE_EDIT), signal?.ID, signal?.Name);
                HeaderText = string.Format((value ? HEADER_TEXT_NEW : HEADER_TEXT_EDIT), signal?.ID, signal?.Name);
            }
        }

        public ExternalSignalEditorForm()
        {
            InitializeComponent();
        }

        public ExternalSignalEditorForm(ExternalSignal signal)
        {
            InitializeComponent();
            initCategoryDropDown();
            AddingNew = (signal == null);
            this.signal = (signal != null) ? signal : new ExternalSignal();
        }

        protected override void loadData()
        {
            if (signal == null)
                return;
            idNumericField.Value = (addingNew ? ExternalSignalDatabases.Signals.NextValidId() : signal.ID);
            nameTextBox.Text = signal.Name;
            categoryDropDown.SelectByValue(signal.Category);
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

            signal.StartUpdate();
            writeFields();
            signal.EndUpdate();

            if (addingNew)
                ExternalSignalDatabases.Signals.Add(signal);
            AddingNew = false;

            return true;

        }

        protected virtual void validateFields()
        {
            if (signal == null)
                return;
            signal.ValidateId((int)idNumericField.Value);
            //category.ValidateName(nameTextBox.Text);
        }

        protected virtual void writeFields()
        {
            if (signal == null)
                return;
            signal.ID = (int)idNumericField.Value;
            signal.Name = nameTextBox.Text;
            signal.Category = categoryDropDown.SelectedValue as ExternalSignalCategory;
        }

        private void initCategoryDropDown()
        {
            categoryDropDown.CreateAdapterAsDataSource<ExternalSignalCategory>(
                ExternalSignalDatabases.Categories.ItemsAsList,
                category => string.Format("(#{0}) {1}", category.ID, category.Name),
                true,
                "(not associated)");
        }

    }

}
