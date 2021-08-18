using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.Leitch;
using OpenSC.Model.SerialPorts;
using System;

namespace OpenSC.GUI.Routers
{

    public partial class LeitchRouterEditorForm : RouterEditorFormBase, IModelEditorForm<Router>
    {

        public LeitchRouterEditorForm() : base()
        {
            InitializeComponent();
        }

        public LeitchRouterEditorForm(Router router) : base(router)
        {
            InitializeComponent();
            if (router == null)
                this.router = new LeitchRouter();
            else if (!(router is LeitchRouter))
                throw new ArgumentException();
            initDropDowns();
        }

        public IModelEditorForm<Router> GetInstance(Router modelInstance)
        {
            return new LeitchRouterEditorForm(modelInstance);
        }

        protected override void loadData()
        {
            base.loadData();
            LeitchRouter leitchRouter = router as LeitchRouter;
            if (leitchRouter == null)
                return;

            portDropDown.SelectByValue(leitchRouter.Port);
            levelNumericField.Value = leitchRouter.Level;

        }

        protected override void writeFields()
        {

            base.writeFields();
            LeitchRouter leitchRouter = router as LeitchRouter;
            if (leitchRouter == null)
                return;

            leitchRouter.Port = portDropDown.SelectedValue as SerialPort;
            leitchRouter.Level = (int)levelNumericField.Value;

        }

        protected override void validateFields()
        {
            base.validateFields();
            LeitchRouter leitchRouter = router as LeitchRouter;
            if (leitchRouter == null)
                return;
        }

        private void initDropDowns()
        {
            // Ports
            portDropDown.CreateAdapterAsDataSource(SerialPortDatabase.Instance, port => port.Name, true, "(not connected)");
        }

    }

}
