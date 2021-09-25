using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.Signals;
using OpenSC.Model.Signals.TallyCopying;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Signals.TallyCopying
{

    public partial class TallyCopyEditorForm : ModelEditorFormBase, IModelEditorForm<TallyCopy>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as TallyCopy);
        public IModelEditorForm<TallyCopy> GetInstanceT(TallyCopy modelInstance) => new TallyCopyEditorForm(modelInstance);

        public TallyCopyEditorForm() : base() => InitializeComponent();

        public TallyCopyEditorForm(TallyCopy tallyCopy) : base(tallyCopy)
        {
            InitializeComponent();
            initSourceSignalDropDown(fromSignalDropDown);
            initSourceSignalDropDown(toSignalDropDown);
            initColorDropDown(fromColorDropDown);
            initColorDropDown(toColorDropDown);
        }

        protected override IModelEditorFormDataManager createManager()
           => new ModelEditorFormDataManager<TallyCopy, TallyCopy>(this, TallyCopyDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            TallyCopy tallyCopy = (TallyCopy)EditedModel;
            if (tallyCopy == null)
                return;
            fromSignalDropDown.SelectByValue(tallyCopy.FromSignal);
            toSignalDropDown.SelectByValue(tallyCopy.ToSignal);
            fromColorDropDown.SelectByValue(tallyCopy.FromTallyColor);
            toColorDropDown.SelectByValue(tallyCopy.ToTallyColor);
        }

        protected override void validateFields()
        {
            base.validateFields();
            TallyCopy tallyCopy = (TallyCopy)EditedModel;
            if (tallyCopy == null)
                return;
            tallyCopy.ValidateId((int)idNumericField.Value);
            //category.ValidateName(nameTextBox.Text);
        }

        protected override void writeFields()
        {
            base.writeFields();
            TallyCopy tallyCopy = (TallyCopy)EditedModel;
            if (tallyCopy == null)
                return;
            tallyCopy.FromSignal = fromSignalDropDown.SelectedValue as ISignalSourceRegistered;
            tallyCopy.ToSignal = toSignalDropDown.SelectedValue as ISignalSourceRegistered;
            tallyCopy.FromTallyColor = (SignalTallyColor)fromColorDropDown.SelectedValue;
            tallyCopy.ToTallyColor = (SignalTallyColor)toColorDropDown.SelectedValue;
        }

        private void initSourceSignalDropDown(ComboBox dropDown)
            => dropDown.CreateAdapterAsDataSource<ISignalSourceRegistered>(
                SignalRegister.Instance,
                signal => string.Format("[{0}] {1}", signal.SignalUniqueId, signal.SignalLabel),
                true,
                "(not associated)");

        private void initColorDropDown(ComboBox dropDown)
            => dropDown.SetAdapterAsDataSource(new EnumComboBoxAdapter<SignalTallyColor>());

    }

}
