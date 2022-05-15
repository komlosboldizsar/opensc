using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.Variables
{

    public partial class FlipFlopBooleanEditorForm : CustomBooleanEditorFormBase, IModelEditorForm<CustomBoolean>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as CustomBoolean);
        public IModelEditorForm<CustomBoolean> GetInstanceT(CustomBoolean modelInstance) => new FlipFlopBooleanEditorForm(modelInstance);

        public FlipFlopBooleanEditorForm() : base() => InitializeComponent();

        public FlipFlopBooleanEditorForm(CustomBoolean customBoolean) : base(customBoolean)
        {
            InitializeComponent();
            if ((customBoolean != null) && !(customBoolean is FlipFlopBoolean))
                throw new ArgumentException($"Type of custom boolean should be {nameof(FlipFlopBoolean)}.", nameof(customBoolean));
            initDropDowns();
            subscribeChangeEvents();
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<CustomBoolean, FlipFlopBoolean>(this, CustomBooleanDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            FlipFlopBoolean flipFlopBoolean = (FlipFlopBoolean)EditedModel;
            if (flipFlopBoolean == null)
                return;
            typeDropDown.SelectByValue(flipFlopBoolean.Type);
            input1DropDown.SelectByValue(flipFlopBoolean.Input1);
            input2DropDown.SelectByValue(flipFlopBoolean.Input2);
            input1InvertCheckBox.Checked = flipFlopBoolean.Input1Inverted;
            input2InvertCheckBox.Checked = flipFlopBoolean.Input2Inverted;
            updateFieldsByType();
        }

        protected override void writeFields()
        {
            base.writeFields();
            FlipFlopBoolean flipFlopBoolean = (FlipFlopBoolean)EditedModel;
            if (flipFlopBoolean == null)
                return;
            flipFlopBoolean.Type = (FlipFlopType)typeDropDown.SelectedValue;
            flipFlopBoolean.Input1 = input1DropDown.SelectedValue as IBoolean;
            flipFlopBoolean.Input2 = input2DropDown.SelectedValue as IBoolean;
            flipFlopBoolean.Input1Inverted = input1InvertCheckBox.Checked;
            flipFlopBoolean.Input2Inverted = input2InvertCheckBox.Checked;
        }

        protected override void validateFields()
        {
            base.validateFields();
            FlipFlopBoolean flipFlopBoolean = (FlipFlopBoolean)EditedModel;
            if (flipFlopBoolean == null)
                return;
        }

        private void initDropDowns()
        {
            typeDropDown.SetAdapterAsDataSource(new EnumComboBoxAdapter<FlipFlopType>(FLIPFLOP_TYPE_TRANSLATIONS));
            input1DropDown.CreateAdapterAsDataSource(BooleanRegister.Instance, b => b.Identifier, true, "(not associated)");
            input2DropDown.CreateAdapterAsDataSource(BooleanRegister.Instance, b => b.Identifier, true, "(not associated)");
            input1DropDown.ReceiveObjectDrop().FilterByType<IBoolean>();
            input2DropDown.ReceiveObjectDrop().FilterByType<IBoolean>();
        }

        private static readonly Dictionary<FlipFlopType, string> FLIPFLOP_TYPE_TRANSLATIONS = new()
        {
            { FlipFlopType.FlipFlopD, "D flip-flop" },
            { FlipFlopType.LatchSR, "SR latch (S priority)" },
            { FlipFlopType.LatchRS, "RS latch (R priority)" },
            { FlipFlopType.FlipFlopSR, "SR flip-flop" },
            { FlipFlopType.LatchJK, "JK latch" },
            { FlipFlopType.FlipFlopT, "T flip-flop" }
        };

        private void subscribeChangeEvents()
        {
            typeDropDown.SelectedValueChanged += typeDropDown_SelectedValueChanged;
            input1DropDown.SelectedValueChanged += inputXDropDown_SelectedValueChanged;
            input2DropDown.SelectedValueChanged += inputXDropDown_SelectedValueChanged;
            input1InvertCheckBox.CheckedChanged += inputXInvertCheckBox_CheckedChanged;
            input2InvertCheckBox.CheckedChanged += inputXInvertCheckBox_CheckedChanged;
        }

        private void typeDropDown_SelectedValueChanged(object sender, EventArgs e)
        {
            updateFieldsByType();
            updateNonUserEditableFields();
        }

        private void inputXDropDown_SelectedValueChanged(object sender, EventArgs e) => updateNonUserEditableFields();
        private void inputXInvertCheckBox_CheckedChanged(object sender, EventArgs e) => updateNonUserEditableFields();

        private void updateFieldsByType()
        {
            FlipFlopType type = (FlipFlopType)typeDropDown.SelectedValue;
            FlipFlopTypeDescriptor typeDescriptor = FlipFlopBoolean.GetTypeDescriptorByType(type);
            input1Label.Text = typeDescriptor.Input1Name;
            input2Label.Text = typeDescriptor.Input2Name;
            input1DropDown.Enabled = typeDescriptor.Input1Used;
            input2DropDown.Enabled = typeDescriptor.Input2Used;
            input1InvertCheckBox.Enabled = (typeDescriptor.Input1Used && typeDescriptor.Input1Invertable);
            input2InvertCheckBox.Enabled = (typeDescriptor.Input2Used && typeDescriptor.Input2Invertable);
        }

        protected override CustomBooleanDataStore getDataStore(CustomBooleanDataStore dataStore = null)
        {
            FlipFlopDataStore typedDataStore = (FlipFlopDataStore)dataStore;
            if (typedDataStore == null)
                typedDataStore = new FlipFlopDataStore();
            typedDataStore.Type = (FlipFlopType)typeDropDown.SelectedValue;
            typedDataStore.Input1 = input1DropDown.SelectedValue as IBoolean;
            typedDataStore.Input2 = input2DropDown.SelectedValue as IBoolean;
            typedDataStore.Input1Inverted = input1InvertCheckBox.Checked;
            typedDataStore.Input2Inverted = input2InvertCheckBox.Checked;
            return base.getDataStore(typedDataStore);
        }

    }

}
