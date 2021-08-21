using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.CrosspointStores;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers.CrosspointStores
{

    public partial class CrosspointStoreEditorForm : ModelEditorFormBase
    {

        private const string TITLE_NEW = "New crosspoint store";
        private const string TITLE_EDIT = "Edit crosspoint store: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New crosspoint store";
        private const string HEADER_TEXT_EDIT = "Edit crosspoint store";

        protected CrosspointStore crosspointStore;

        bool addingNew = false;

        protected bool AddingNew
        {
            get { return addingNew; }
            set
            {
                addingNew = value;
                Text = string.Format((value ? TITLE_NEW : TITLE_EDIT), crosspointStore?.ID, crosspointStore?.Name);
                HeaderText = string.Format((value ? HEADER_TEXT_NEW : HEADER_TEXT_EDIT), crosspointStore?.ID, crosspointStore?.Name);
            }
        }

        public CrosspointStoreEditorForm()
        {
            InitializeComponent();
        }

        public CrosspointStoreEditorForm(CrosspointStore crosspointStore)
        {
            InitializeComponent();
            initRouterDropDown(routerInputRouterDropDown);
            initRouterDropDown(routerOutputRouterDropDown);
            routerInputRouterDropDown.SelectedIndexChanged += selectedRouterForInputChanged;
            routerOutputRouterDropDown.SelectedIndexChanged += selectedRouterForOutputChanged;
            AddingNew = (crosspointStore == null);
            this.crosspointStore = (crosspointStore != null) ? crosspointStore : new CrosspointStore();
        }

        private void selectedRouterForInputChanged(object sender, EventArgs e)
            => updateRouterInputDropDown(routerInputRouterDropDown.SelectedValue as Router);

        private void selectedRouterForOutputChanged(object sender, EventArgs e)
            => updateRouterOutputDropDown(routerOutputRouterDropDown.SelectedValue as Router);

        protected override void loadData()
        {
            if (crosspointStore == null)
                return;
            idNumericField.Value = (addingNew ? CrosspointStoreDatabase.Instance.NextValidId() : crosspointStore.ID);
            nameTextBox.Text = crosspointStore.Name;
            routerInputRouterDropDown.SelectByValue(crosspointStore.StoredInput?.Router);
            routerInputInputDropDown.SelectByValue(crosspointStore.StoredInput);
            routerOutputRouterDropDown.SelectByValue(crosspointStore.StoredOutput?.Router);
            routerOutputOutputDropDown.SelectByValue(crosspointStore.StoredOutput);
            autotakeAfterOutputSetCheckbox.Checked = crosspointStore.Autotake;
            clearInputAfterTakeCheckbox.Checked = crosspointStore.ClearInputAfterTake;
            clearOutputAfterTakeCheckbox.Checked = crosspointStore.ClearOutputAfterTake;
            importInputAfterOutputSetCheckbox.Checked = crosspointStore.ImportInputAfterOutputSet;
        }

        protected sealed override bool saveData()
        {

            try
            {
                validateFields();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Data validation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            crosspointStore.StartUpdate();
            writeFields();
            crosspointStore.EndUpdate();

            if (addingNew)
                CrosspointStoreDatabase.Instance.Add(crosspointStore);
            AddingNew = false;

            return true;

        }

        protected virtual void validateFields()
        {
            if (crosspointStore == null)
                return;
            crosspointStore.ValidateId((int)idNumericField.Value);
            //category.ValidateName(nameTextBox.Text);
        }

        protected virtual void writeFields()
        {
            if (crosspointStore == null)
                return;
            crosspointStore.ID = (int)idNumericField.Value;
            crosspointStore.Name = nameTextBox.Text;
            crosspointStore.StoredInput = routerInputInputDropDown.SelectedValue as RouterInput;
            crosspointStore.StoredOutput = routerOutputOutputDropDown.SelectedValue as RouterOutput;
            crosspointStore.Autotake = autotakeAfterOutputSetCheckbox.Checked;
            crosspointStore.ClearInputAfterTake = clearInputAfterTakeCheckbox.Checked;
            crosspointStore.ClearOutputAfterTake = clearOutputAfterTakeCheckbox.Checked;
            crosspointStore.ImportInputAfterOutputSet = importInputAfterOutputSetCheckbox.Checked;
        }

        private void initRouterDropDown(ComboBox dropDown)
            => dropDown.CreateAdapterAsDataSource<Router>(
                RouterDatabase.Instance,
                router => string.Format("(#{0}) {1}", router.ID, router.Name),
                true,
                "(not associated)");

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
