using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.Signals;
using OpenSC.Model.Signals.BooleanTallies;
using OpenSC.Model.Variables;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Signals.BooleanTallies
{

    public partial class BooleanTallyEditorForm : ModelEditorFormBase, IModelEditorForm<BooleanTally>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as BooleanTally);
        public IModelEditorForm<BooleanTally> GetInstanceT(BooleanTally modelInstance) => new BooleanTallyEditorForm(modelInstance);

        private const string TITLE_NEW = "New boolean tally";
        private const string TITLE_EDIT = "Edit boolean tally: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New boolean tally";
        private const string HEADER_TEXT_EDIT = "Edit boolean tally";

        protected BooleanTally booleanTally;

        bool addingNew = false;

        protected bool AddingNew
        {
            get { return addingNew; }
            set
            {
                addingNew = value;
                Text = string.Format((value ? TITLE_NEW : TITLE_EDIT), booleanTally?.ID, booleanTally?.Name);
                HeaderText = string.Format((value ? HEADER_TEXT_NEW : HEADER_TEXT_EDIT), booleanTally?.ID, booleanTally?.Name);
            }
        }

        public BooleanTallyEditorForm()
        {
            InitializeComponent();
        }

        public BooleanTallyEditorForm(BooleanTally booleanTally)
        {
            InitializeComponent();
            initFromBooleanDropDown();
            initToSignalDropDown();
            initToColorDropDown();
            AddingNew = (booleanTally == null);
            this.booleanTally = (booleanTally != null) ? booleanTally : new BooleanTally();
        }

        protected override void loadData()
        {
            if (booleanTally == null)
                return;
            idNumericField.Value = (addingNew ? BooleanTallyDatabase.Instance.NextValidId() : booleanTally.ID);
            nameTextBox.Text = booleanTally.Name;
            fromBooleanDropDown.SelectByValue(booleanTally.FromBoolean);
            toSignalDropDown.SelectByValue(booleanTally.ToSignal);
            toColorDropDown.SelectByValue(booleanTally.ToTallyColor);
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

            booleanTally.StartUpdate();
            writeFields();
            booleanTally.EndUpdate();

            if (addingNew)
                BooleanTallyDatabase.Instance.Add(booleanTally);
            AddingNew = false;

            return true;

        }

        protected virtual void validateFields()
        {
            if (booleanTally == null)
                return;
            booleanTally.ValidateId((int)idNumericField.Value);
            //category.ValidateName(nameTextBox.Text);
        }

        protected virtual void writeFields()
        {
            if (booleanTally == null)
                return;
            booleanTally.ID = (int)idNumericField.Value;
            booleanTally.Name = nameTextBox.Text;
            booleanTally.FromBoolean = fromBooleanDropDown.SelectedValue as IBoolean;
            booleanTally.ToSignal = toSignalDropDown.SelectedValue as ISignalSourceRegistered;
            booleanTally.ToTallyColor = (SignalTallyColor)toColorDropDown.SelectedValue;
        }

        private void initFromBooleanDropDown()
        {
            fromBooleanDropDown.CreateAdapterAsDataSource<IBoolean>(
                BooleanRegister.Instance,
                boolean => boolean.Name,
                true,
                "(not associated)");
        }

        private void initToSignalDropDown()
        {
            toSignalDropDown.CreateAdapterAsDataSource<ISignalSourceRegistered>(
                SignalRegister.Instance,
                signal => string.Format("[{0}] {1}", signal.SignalUniqueId, signal.SignalLabel),
                true,
                "(not associated)");
        }

        private void initToColorDropDown()
        {
            EnumComboBoxAdapter<SignalTallyColor> adapter = new EnumComboBoxAdapter<SignalTallyColor>();
            toColorDropDown.SetAdapterAsDataSource(adapter);
        }

    }

}
