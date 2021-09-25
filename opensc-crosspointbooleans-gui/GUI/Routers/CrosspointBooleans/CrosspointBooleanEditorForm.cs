using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.CrosspointBooleans;
using OpenSC.Model.Variables;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers.CrosspointBooleans
{

    public partial class CrosspointBooleanEditorForm : ModelEditorFormBase, IModelEditorForm<CrosspointBoolean>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as CrosspointBoolean);
        public IModelEditorForm<CrosspointBoolean> GetInstanceT(CrosspointBoolean modelInstance) => new CrosspointBooleanEditorForm(modelInstance);

        public CrosspointBooleanEditorForm() : base() => InitializeComponent();

        public CrosspointBooleanEditorForm(CrosspointBoolean crosspointBoolean)
            : base(crosspointBoolean)
        {
            InitializeComponent();
            initRouterDropDown();
            routerDropDown.SelectedIndexChanged += selectedRouterChanged;
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<CrosspointBoolean, CrosspointBoolean>(this, CrosspointBooleanDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            CrosspointBoolean crosspointBoolean = ((CrosspointBoolean)EditedModel);
            if (crosspointBoolean == null)
                return;
            routerDropDown.SelectByValue(crosspointBoolean.WatchedRouter);
            routerInputDropDown.SelectByValue(crosspointBoolean.WatchedInput);
            routerOutputDropDown.SelectByValue(crosspointBoolean.WatchedOutput);
        }

        protected override void validateFields()
        {
            base.validateFields();
            CrosspointBoolean crosspointBoolean = ((CrosspointBoolean)EditedModel);
            if (crosspointBoolean == null)
                return;
            crosspointBoolean.ValidateId((int)idNumericField.Value);
            //category.ValidateName(nameTextBox.Text);
        }

        protected override void writeFields()
        {
            base.writeFields();
            CrosspointBoolean crosspointBoolean = ((CrosspointBoolean)EditedModel);
            if (crosspointBoolean == null)
                return;
            crosspointBoolean.WatchedInput = routerInputDropDown.SelectedValue as RouterInput;
            crosspointBoolean.WatchedOutput = routerOutputDropDown.SelectedValue as RouterOutput;
        }

        private void initRouterDropDown()
            => routerDropDown.CreateAdapterAsDataSource<Router>(
                RouterDatabase.Instance,
                router => string.Format("(#{0}) {1}", router.ID, router.Name),
                true,
                "(not associated)");

        private void updateRouterInputDropDown()
            => routerInputDropDown.CreateAdapterAsDataSource<RouterInput>(
                (routerDropDown.SelectedValue as Router)?.Inputs,
                routerInput => string.Format("(#{0}) {1}", routerInput.Index, routerInput.Name),
                true,
                "(not associated)");

        private void updateRouterOutputDropDown()
            => routerOutputDropDown.CreateAdapterAsDataSource<RouterOutput>(
                (routerDropDown.SelectedValue as Router)?.Outputs,
                routerOutput => string.Format("(#{0}) {1}", routerOutput.Index, routerOutput.Name),
                true,
                "(not associated)");
        private void selectedRouterChanged(object sender, EventArgs e)
        {
            updateRouterInputDropDown();
            updateRouterOutputDropDown();
        }

    }

}
