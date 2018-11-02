using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.Signals;
using OpenSC.Model.Variables;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Signals
{

    public partial class SignalEditorForm : ModelEditorFormBase
    {

        private const string TITLE_NEW = "New signal";
        private const string TITLE_EDIT = "Edit signal: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New signal";
        private const string HEADER_TEXT_EDIT = "Edit signal";

        protected Signal signal;

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

        public SignalEditorForm()
        {
            InitializeComponent();
        }

        public SignalEditorForm(Signal signal)
        {
            InitializeComponent();
            initCategoryDropDown();
            initTallySourceDropDown();
            AddingNew = (signal == null);
            this.signal = (signal != null) ? signal : new Signal();
        }

        protected override void loadData()
        {
            if (signal == null)
                return;
            idNumericField.Value = (addingNew ? SignalDatabases.Signals.NextValidId() : signal.ID);
            nameTextBox.Text = signal.Name;
            categoryDropDown.SelectByValue(signal.Category);
            redTallySourceDropDown.SelectByValue(signal.RedTallySource);
            greenTallySourceDropDown.SelectByValue(signal.GreenTallySource);
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
                SignalDatabases.Signals.Add(signal);
            }

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
            signal.Category = categoryDropDown.SelectedValue as SignalCategory;
            signal.RedTallySource = redTallySourceDropDown.SelectedValue as IBoolean;
            signal.GreenTallySource = greenTallySourceDropDown.SelectedValue as IBoolean;
        }

        private void initCategoryDropDown()
        {
            categoryDropDown.CreateAdapterAsDataSource<SignalCategory>(
                SignalDatabases.Categories.ItemsAsList,
                category => string.Format("(#{0}) {1}", category.ID, category.Name),
                true,
                "(not associated)");
        }

        private void initTallySourceDropDown()
        {
            IComboBoxAdapter adapter = new ComboBoxAdapter<IBoolean>(
                BooleanRegister.Instance,
                b => string.Format("{0}: {1}", b.Name, b.Description),
                true,
                "(not set)");
            redTallySourceDropDown.SetAdapterAsDataSource(adapter);
            greenTallySourceDropDown.SetAdapterAsDataSource(adapter);
        }

    }

}
