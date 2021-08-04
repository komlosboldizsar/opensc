using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.Leitch;
using OpenSC.Model.SerialPorts;
using System;

namespace OpenSC.GUI.Routers
{

    public partial class VirtualLeitchRouterEditorForm : RouterEditorFormBase, IModelEditorForm<Router>
    {

        public VirtualLeitchRouterEditorForm() : base()
        {
            InitializeComponent();
        }

        public VirtualLeitchRouterEditorForm(Router router) : base(router)
        {
            InitializeComponent();
            if (router == null)
                this.router = new VirtualLeitchRouter();
            else if (!(router is VirtualLeitchRouter))
                throw new ArgumentException();
            initDropDowns();
        }

        public IModelEditorForm<Router> GetInstance(Router modelInstance)
        {
            return new VirtualLeitchRouterEditorForm(modelInstance);
        }

        protected override void loadData()
        {
            base.loadData();
            VirtualLeitchRouter virtualLeitchRouer = router as VirtualLeitchRouter;
            if (virtualLeitchRouer == null)
                return;

            portDropDown.SelectByValue(virtualLeitchRouer.Port);
            levelNumericField.Value = virtualLeitchRouer.Level;

        }

        protected override void writeFields()
        {

            base.writeFields();
            VirtualLeitchRouter virtualLeitchRouter = router as VirtualLeitchRouter;
            if (virtualLeitchRouter == null)
                return;

            virtualLeitchRouter.Port = portDropDown.SelectedValue as SerialPort;
            virtualLeitchRouter.Level = (int)levelNumericField.Value;

        }

        protected override void validateFields()
        {
            base.validateFields();
            VirtualLeitchRouter virtualLeitchRouter = router as VirtualLeitchRouter;
            if (virtualLeitchRouter == null)
                return;
        }

        private void initDropDowns()
        {
            // Ports
            portDropDown.CreateAdapterAsDataSource(SerialPortDatabase.Instance, port => port.Name, true, "(not connected)");
        }

    }

}
