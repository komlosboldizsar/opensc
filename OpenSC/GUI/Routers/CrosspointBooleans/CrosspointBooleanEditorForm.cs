using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.CrosspointBooleans;
using OpenSC.Model.Routers.CrosspointStores;
using OpenSC.Model.Signals;
using OpenSC.Model.Variables;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers.CrosspointBooleans
{

    public partial class CrosspointBooleanEditorForm : ModelEditorFormBase
    {

        private const string TITLE_NEW = "New crosspoint boolean";
        private const string TITLE_EDIT = "Edit crosspoint boolean: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New crosspoint boolean";
        private const string HEADER_TEXT_EDIT = "Edit crosspoint boolean";

        protected CrosspointBoolean crosspointBoolean;

        bool addingNew = false;

        protected bool AddingNew
        {
            get { return addingNew; }
            set
            {
                addingNew = value;
                Text = string.Format((value ? TITLE_NEW : TITLE_EDIT), crosspointBoolean?.ID, crosspointBoolean?.Name);
                HeaderText = string.Format((value ? HEADER_TEXT_NEW : HEADER_TEXT_EDIT), crosspointBoolean?.ID, crosspointBoolean?.Name);
            }
        }

        public CrosspointBooleanEditorForm()
        {
            InitializeComponent();
        }

        public CrosspointBooleanEditorForm(CrosspointBoolean crosspointBoolean)
        {
            InitializeComponent();
            initRouterDropDown();
            routerDropDown.SelectedIndexChanged += selectedRouterChanged;
            AddingNew = (crosspointBoolean == null);
            this.crosspointBoolean = (crosspointBoolean != null) ? crosspointBoolean : new CrosspointBoolean();
        }

        protected override void loadData()
        {
            if (crosspointBoolean == null)
                return;
            idNumericField.Value = (addingNew ? CrosspointBooleanDatabase.Instance.NextValidId() : crosspointBoolean.ID);
            nameTextBox.Text = crosspointBoolean.Name;
            routerDropDown.SelectByValue(crosspointBoolean.WatchedRouter);
            routerInputDropDown.SelectByValue(crosspointBoolean.WatchedInput);
            routerOutputDropDown.SelectByValue(crosspointBoolean.WatchedOutput);
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

            crosspointBoolean.StartUpdate();
            writeFields();
            crosspointBoolean.EndUpdate();

            if (addingNew)
                CrosspointBooleanDatabase.Instance.Add(crosspointBoolean);
            AddingNew = false;

            return true;

        }

        protected virtual void validateFields()
        {
            if (crosspointBoolean == null)
                return;
            crosspointBoolean.ValidateId((int)idNumericField.Value);
            //category.ValidateName(nameTextBox.Text);
        }

        protected virtual void writeFields()
        {
            if (crosspointBoolean == null)
                return;
            crosspointBoolean.ID = (int)idNumericField.Value;
            crosspointBoolean.Name = nameTextBox.Text;
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
