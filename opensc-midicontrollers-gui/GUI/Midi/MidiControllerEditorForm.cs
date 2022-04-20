using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.MidiControllers;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.MidiControllers
{

    public partial class MidiControllerEditorForm : ModelEditorFormBase, IModelEditorForm<MidiController>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as MidiController);
        public IModelEditorForm<MidiController> GetInstanceT(MidiController modelInstance) => new MidiControllerEditorForm(modelInstance);

        public MidiControllerEditorForm() : base() => InitializeComponent();

        public MidiControllerEditorForm(MidiController controller) : base(controller) => InitializeComponent();

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<MidiController, MidiController>(this, MidiControllerDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            MidiController controller = (MidiController)EditedModel;
            if (controller == null)
                return;
            deviceIdNumericField.Value = controller.DeviceId;
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
            port.DeviceId = (int)deviceIdNumericField.Value;
        }

    }

}
