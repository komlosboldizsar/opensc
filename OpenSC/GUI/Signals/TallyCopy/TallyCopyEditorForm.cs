using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.Signals;
using OpenSC.Model.Signals.TallyCopying;
using OpenSC.Model.Variables;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Signals.TallyCopying
{

    public partial class TallyCopyEditorForm : ModelEditorFormBase
    {

        private const string TITLE_NEW = "New tally copy";
        private const string TITLE_EDIT = "Edit tally copy: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New tally copy";
        private const string HEADER_TEXT_EDIT = "Edit tally copy";

        protected TallyCopy tallyCopy;

        bool addingNew = false;

        protected bool AddingNew
        {
            get { return addingNew; }
            set
            {
                addingNew = value;
                Text = string.Format((value ? TITLE_NEW : TITLE_EDIT), tallyCopy?.ID, tallyCopy?.Name);
                HeaderText = string.Format((value ? HEADER_TEXT_NEW : HEADER_TEXT_EDIT), tallyCopy?.ID, tallyCopy?.Name);
            }
        }

        public TallyCopyEditorForm()
        {
            InitializeComponent();
        }

        public TallyCopyEditorForm(TallyCopy tallyCopy)
        {
            InitializeComponent();
            initSourceSignalDropDown(fromSignalDropDown);
            initSourceSignalDropDown(toSignalDropDown);
            initColorDropDown(fromColorDropDown);
            initColorDropDown(toColorDropDown);
            AddingNew = (tallyCopy == null);
            this.tallyCopy = (tallyCopy != null) ? tallyCopy : new TallyCopy();
        }

        protected override void loadData()
        {
            if (tallyCopy == null)
                return;
            idNumericField.Value = (addingNew ? TallyCopyDatabase.Instance.NextValidId() : tallyCopy.ID);
            nameTextBox.Text = tallyCopy.Name;
            fromSignalDropDown.SelectByValue(tallyCopy.FromSignal);
            toSignalDropDown.SelectByValue(tallyCopy.ToSignal);
            fromColorDropDown.SelectByValue(tallyCopy.FromTallyColor);
            toColorDropDown.SelectByValue(tallyCopy.ToTallyColor);
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

            tallyCopy.StartUpdate();
            writeFields();
            tallyCopy.EndUpdate();

            if (addingNew)
                TallyCopyDatabase.Instance.Add(tallyCopy);
            AddingNew = false;

            return true;

        }

        protected virtual void validateFields()
        {
            if (tallyCopy == null)
                return;
            tallyCopy.ValidateId((int)idNumericField.Value);
            //category.ValidateName(nameTextBox.Text);
        }

        protected virtual void writeFields()
        {
            if (tallyCopy == null)
                return;
            tallyCopy.ID = (int)idNumericField.Value;
            tallyCopy.Name = nameTextBox.Text;
            tallyCopy.FromSignal = fromSignalDropDown.SelectedValue as ISignalSourceRegistered;
            tallyCopy.ToSignal = toSignalDropDown.SelectedValue as ISignalSourceRegistered;
            tallyCopy.FromTallyColor = (SignalTallyColor)fromColorDropDown.SelectedValue;
            tallyCopy.ToTallyColor = (SignalTallyColor)toColorDropDown.SelectedValue;
        }

        private void initSourceSignalDropDown(ComboBox dropDown)
        {
            dropDown.CreateAdapterAsDataSource<ISignalSourceRegistered>(
                SignalRegister.Instance,
                signal => string.Format("[{0}] {1}", signal.SignalUniqueId, signal.SignalLabel),
                true,
                "(not associated)");
        }

        private void initColorDropDown(ComboBox dropDown)
        {
            EnumComboBoxAdapter<SignalTallyColor> adapter = new EnumComboBoxAdapter<SignalTallyColor>();
            dropDown.SetAdapterAsDataSource(adapter);
        }

    }

}
