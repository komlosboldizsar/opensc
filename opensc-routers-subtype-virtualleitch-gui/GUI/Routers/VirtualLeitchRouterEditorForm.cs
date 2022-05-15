using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.Leitch;
using OpenSC.Model.SerialPorts;
using System;

namespace OpenSC.GUI.Routers
{

    public partial class VirtualLeitchRouterEditorForm : RouterEditorFormBase, IModelEditorForm<Router>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Router);
        public IModelEditorForm<Router> GetInstanceT(Router modelInstance) => new VirtualLeitchRouterEditorForm(modelInstance);

        public VirtualLeitchRouterEditorForm() : base() => InitializeComponent();

        public VirtualLeitchRouterEditorForm(Router router) : base(router)
        {
            InitializeComponent();
            if ((router != null) && !(router is VirtualLeitchRouter))
                throw new ArgumentException($"Type of router should be {nameof(VirtualLeitchRouter)}.", nameof(router));
            initDropDowns();
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<Router, VirtualLeitchRouter>(this, RouterDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            VirtualLeitchRouter virtualLeitchRouter = (VirtualLeitchRouter)EditedModel;
            if (virtualLeitchRouter == null)
                return;
            portDropDown.SelectByValue(virtualLeitchRouter.Port);
            levelNumericField.Value = virtualLeitchRouter.Level;
        }

        protected override void writeFields()
        {
            base.writeFields();
            VirtualLeitchRouter virtualLeitchRouter = (VirtualLeitchRouter)EditedModel;
            if (virtualLeitchRouter == null)
                return;
            virtualLeitchRouter.Port = portDropDown.SelectedValue as SerialPort;
            virtualLeitchRouter.Level = (int)levelNumericField.Value;
        }

        protected override void validateFields()
        {
            base.validateFields();
            VirtualLeitchRouter virtualLeitchRouter = (VirtualLeitchRouter)EditedModel;
            if (virtualLeitchRouter == null)
                return;
        }

        private void initDropDowns()
        {
            portDropDown.CreateAdapterAsDataSource(SerialPortDatabase.Instance, port => port.Name, true, "(not connected)");
            portDropDown.ReceiveObjectDrop().FilterByType<SerialPort>();
        }

    }

}
