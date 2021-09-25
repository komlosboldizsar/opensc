using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.CrosspointStores;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers.CrosspointStores
{

    public partial class CrosspointStoreEditorForm : ModelEditorFormBase, IModelEditorForm<CrosspointStore>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as CrosspointStore);
        public IModelEditorForm<CrosspointStore> GetInstanceT(CrosspointStore modelInstance) => new CrosspointStoreEditorForm(modelInstance);

        public CrosspointStoreEditorForm() : base() => InitializeComponent();

        public CrosspointStoreEditorForm(CrosspointStore crosspointStore)
            : base(crosspointStore)
        {
            InitializeComponent();
            initRouterDropDown(routerInputRouterDropDown);
            initRouterDropDown(routerOutputRouterDropDown);
            routerInputRouterDropDown.SelectedIndexChanged += selectedRouterForInputChanged;
            routerOutputRouterDropDown.SelectedIndexChanged += selectedRouterForOutputChanged;
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<CrosspointStore, CrosspointStore>(this, CrosspointStoreDatabase.Instance);

        private void selectedRouterForInputChanged(object sender, EventArgs e)
            => updateRouterInputDropDown(routerInputRouterDropDown.SelectedValue as Router);

        private void selectedRouterForOutputChanged(object sender, EventArgs e)
            => updateRouterOutputDropDown(routerOutputRouterDropDown.SelectedValue as Router);

        protected override void loadData()
        {
            base.loadData();
            CrosspointStore crosspointStore = (CrosspointStore)EditedModel;
            if (crosspointStore == null)
                return;
            routerInputRouterDropDown.SelectByValue(crosspointStore.StoredInput?.Router);
            routerInputInputDropDown.SelectByValue(crosspointStore.StoredInput);
            routerOutputRouterDropDown.SelectByValue(crosspointStore.StoredOutput?.Router);
            routerOutputOutputDropDown.SelectByValue(crosspointStore.StoredOutput);
            autotakeAfterOutputSetCheckbox.Checked = crosspointStore.Autotake;
            clearInputAfterTakeCheckbox.Checked = crosspointStore.ClearInputAfterTake;
            clearOutputAfterTakeCheckbox.Checked = crosspointStore.ClearOutputAfterTake;
            importInputAfterOutputSetCheckbox.Checked = crosspointStore.ImportInputAfterOutputSet;
        }

        protected override void validateFields()
        {
            base.loadData();
            CrosspointStore crosspointStore = (CrosspointStore)EditedModel;
            if (crosspointStore == null)
                return;
            crosspointStore.ValidateId((int)idNumericField.Value);
            //category.ValidateName(nameTextBox.Text);
        }

        protected override void writeFields()
        {
            base.writeFields();
            CrosspointStore crosspointStore = (CrosspointStore)EditedModel;
            if (crosspointStore == null)
                return;
            crosspointStore.StoredInput = routerInputInputDropDown.SelectedValue as RouterInput;
            crosspointStore.StoredOutput = routerOutputOutputDropDown.SelectedValue as RouterOutput;
            crosspointStore.Autotake = autotakeAfterOutputSetCheckbox.Checked;
            crosspointStore.ClearInputAfterTake = clearInputAfterTakeCheckbox.Checked;
            crosspointStore.ClearOutputAfterTake = clearOutputAfterTakeCheckbox.Checked;
            crosspointStore.ImportInputAfterOutputSet = importInputAfterOutputSetCheckbox.Checked;
        }

        private void initRouterDropDown(ComboBox dropDown)
            => dropDown.CreateAdapterAsDataSource(RouterDatabase.Instance, null, true, "(not associated)");

        private void updateRouterInputDropDown(Router router)
        {
            routerInputInputDropDown.CreateAdapterAsDataSource<RouterInput>(
                router?.Inputs,
                routerInput => string.Format("(#{0}) {1}", routerInput.Index, routerInput.Name),
                true,
                "(not associated)");
        }

        private void updateRouterOutputDropDown(Router router)
        {
            routerOutputOutputDropDown.CreateAdapterAsDataSource<RouterOutput>(
                router?.Outputs,
                routerOutput => string.Format("(#{0}) {1}", routerOutput.Index, routerOutput.Name),
                true,
                "(not associated)");
        }

    }

}
