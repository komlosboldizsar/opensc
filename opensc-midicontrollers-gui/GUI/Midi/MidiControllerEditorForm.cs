using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.Core;
using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.MidiControllers;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace OpenSC.GUI.MidiControllers
{

    public partial class MidiControllerEditorForm : ModelEditorFormBase, IModelEditorForm<MidiController>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as MidiController);
        public IModelEditorForm<MidiController> GetInstanceT(MidiController modelInstance) => new MidiControllerEditorForm(modelInstance);

        public MidiControllerEditorForm() : base()
        {
            InitializeComponent();
            initDropDows();
        }

        public MidiControllerEditorForm(MidiController controller) : base(controller)
        {
            InitializeComponent();
            initDropDows();
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<MidiController, MidiController>(this, MidiControllerDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            MidiController controller = (MidiController)EditedModel;
            if (controller == null)
                return;
            deviceIndexNumericField.Value = controller.DeviceIndex;
            deviceNameDropDown.Text = controller.DeviceName;
            deviceIndexNumericField.Enabled = indexRadioButton.Checked = (controller.DeviceIdentifiedBy == MidiController.IdentifierType.Index);
            deviceNameDropDown.Enabled = nameRadioButton.Checked = (controller.DeviceIdentifiedBy == MidiController.IdentifierType.Name);
        }

        protected override void validateFields()
        {
            base.validateFields();
            MidiController controller = (MidiController)EditedModel;
            if (controller == null)
                return;
        }

        protected override void writeFields()
        {
            base.writeFields();
            MidiController port = (MidiController)EditedModel;
            if (port == null)
                return;
            port.DeviceIndex = (int)deviceIndexNumericField.Value;
            port.DeviceName = deviceNameDropDown.Text;
            port.DeviceIdentifiedBy = indexRadioButton.Checked ? MidiController.IdentifierType.Index : MidiController.IdentifierType.Name;
        }

        private void initDropDows()
        {
            IEnumerable<InputDevice> inputDevices = InputDevice.GetAll();
            deviceNameDropDown.CreateAdapterAsDataSource(inputDevices, id => id.Name);
        }

        private void indexRadioButton_CheckedChanged(object sender, EventArgs e)
            => deviceIndexNumericField.Enabled = indexRadioButton.Checked;

        private void nameRadioButton_CheckedChanged(object sender, EventArgs e)
            => deviceNameDropDown.Enabled = nameRadioButton.Checked;

    }

}
