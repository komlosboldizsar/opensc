using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.Leitch;
using OpenSC.Model.SerialPorts;
using System;

namespace OpenSC.GUI.Routers
{

    public partial class LeitchRouterEditorForm : RouterEditorFormBase, IModelEditorForm<Router>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Router);
        public IModelEditorForm<Router> GetInstanceT(Router modelInstance) => new LeitchRouterEditorForm(modelInstance);

        public LeitchRouterEditorForm() : base() => InitializeComponent();

        public LeitchRouterEditorForm(Router router) : base(router)
        {
            InitializeComponent();
            if ((router != null) && !(router is LeitchRouter))
                throw new ArgumentException($"Type of router should be {nameof(LeitchRouter)}.", nameof(router));
            initDropDowns();
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<Router, LeitchRouter>(this, RouterDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            LeitchRouter leitchRouter = (LeitchRouter)EditedModel;
            if (leitchRouter == null)
                return;
            portDropDown.SelectByValue(leitchRouter.Port);
            levelNumericField.Value = leitchRouter.Level;
        }

        protected override void writeFields()
        {
            base.writeFields();
            LeitchRouter leitchRouter = (LeitchRouter)EditedModel;
            if (leitchRouter == null)
                return;
            leitchRouter.Port = portDropDown.SelectedValue as SerialPort;
            leitchRouter.Level = (int)levelNumericField.Value;
        }

        protected override void validateFields()
        {
            base.validateFields();
            LeitchRouter leitchRouter = (LeitchRouter)EditedModel;
            if (leitchRouter == null)
                return;
        }

        private void initDropDowns()
        {
            portDropDown.CreateAdapterAsDataSource(SerialPortDatabase.Instance, port => port.Name, true, "(not connected)");
            portDropDown.ReceiveObjectDrop().FilterByType<SerialPort>();
        }

    }

}
