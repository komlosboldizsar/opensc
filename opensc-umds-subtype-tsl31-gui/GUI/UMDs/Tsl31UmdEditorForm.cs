using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.SerialPorts;
using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.Tsl31;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.UMDs
{

    public partial class Tsl31UmdEditorForm : UmdEditorFormBase, IModelEditorForm<Umd>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Umd);
        public IModelEditorForm<Umd> GetInstanceT(Umd modelInstance) => new Tsl31UmdEditorForm(modelInstance);

        public Tsl31UmdEditorForm(): base() => InitializeComponent();

        public Tsl31UmdEditorForm(Umd umd) : base(umd)
        {
            InitializeComponent();
            if ((umd != null) && !(umd is Tsl31))
                throw new ArgumentException($"Type of UMD should be {nameof(Tsl31)}.", nameof(umd));
            initPortDropDown();
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<Umd, Tsl31>(this, UmdDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            Tsl31 tsl31 = (Tsl31)EditedModel;
            if (tsl31 == null)
                return;
            portDropDown.SelectByValue(tsl31.Port);
            addressNumericInput.Value = tsl31.Address;
            loadTallyOverrideRadioButtonGroup(tsl31.Tally12OverrideMode, tally12paralellRadioButton, tally1overrides2RadioButton, tally2overrides1RadioButton);
            loadTallyOverrideRadioButtonGroup(tsl31.Tally34OverrideMode, tally34paralellRadioButton, tally3overrides4RadioButton, tally4overrides3RadioButton);
        }

        protected override void writeFields()
        {
            base.writeFields();
            Tsl31 tsl31 = (Tsl31)EditedModel;
            if (tsl31 == null)
                return;
            tsl31.Port = portDropDown.SelectedValue as SerialPort;
            tsl31.Address = (int)addressNumericInput.Value;
            tsl31.Tally12OverrideMode = getTallyOverrideModeFromRadioButtonGroup(tally1overrides2RadioButton, tally2overrides1RadioButton);
            tsl31.Tally34OverrideMode = getTallyOverrideModeFromRadioButtonGroup(tally3overrides4RadioButton, tally4overrides3RadioButton);
        }

        protected override void validateFields()
        {
            base.validateFields();
            Tsl31 tsl31 = (Tsl31)EditedModel;
            if (tsl31 == null)
                return;
            tsl31.ValidateAddress((int)addressNumericInput.Value);
        }

        private void initPortDropDown()
        {
            portDropDown.CreateAdapterAsDataSource(SerialPortDatabase.Instance, null, true, "(not associated)");
            portDropDown.ReceiveObjectDrop().FilterByType<SerialPort>();
        }

        private void loadTallyOverrideRadioButtonGroup(TallyOverrideMode overrideMode, RadioButton noOverrideRadioButton, RadioButton aOverridesBRadioButton, RadioButton bOverridesARadioButton)
        {
            noOverrideRadioButton.Checked = (overrideMode == TallyOverrideMode.NoOverride);
            aOverridesBRadioButton.Checked = (overrideMode == TallyOverrideMode.AOverridesB);
            bOverridesARadioButton.Checked = (overrideMode == TallyOverrideMode.BOverridesA);
        }

        private TallyOverrideMode getTallyOverrideModeFromRadioButtonGroup(RadioButton aOverridesBRadioButton, RadioButton bOverridesARadioButton)
        {
            if (aOverridesBRadioButton.Checked)
                return TallyOverrideMode.AOverridesB;
            if (bOverridesARadioButton.Checked)
                return TallyOverrideMode.BOverridesA;
            return TallyOverrideMode.NoOverride;
        }

    }

}
