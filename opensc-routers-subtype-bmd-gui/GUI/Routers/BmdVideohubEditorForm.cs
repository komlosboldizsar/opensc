using OpenSC.Model.Routers;
using OpenSC.Model.Routers.BlackMagicDesign;
using System;

namespace OpenSC.GUI.Routers
{

    public partial class BmdVideohubEditorForm : RouterEditorFormBase, IModelEditorForm<Router>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Router);
        public IModelEditorForm<Router> GetInstanceT(Router modelInstance) => new BmdVideohubEditorForm(modelInstance);

        public BmdVideohubEditorForm() : base() => InitializeComponent();

        public BmdVideohubEditorForm(Router router) : base(router)
        {
            InitializeComponent();
            if ((router != null) && !(router is BmdVideohub))
                throw new ArgumentException($"Type of router should be {nameof(BmdVideohub)}.", nameof(router));
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<Router, BmdVideohub>(this, RouterDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            BmdVideohub bmdVideohub = (BmdVideohub)EditedModel;
            if (bmdVideohub == null)
                return;
            ipAddressTextbox.Text = bmdVideohub.IpAddress;
            autoReconnectCheckbox.Checked = bmdVideohub.AutoReconnect;
            bmdVideohub.ConnectionStateChanged += connectionStateChangedHandler;
            connectButton.Enabled = !bmdVideohub.Connected;
            disconnectButton.Enabled = bmdVideohub.Connected;
        }

        protected override void writeFields()
        {
            base.writeFields();
            BmdVideohub bmdVideohub = (BmdVideohub)EditedModel;
            if (bmdVideohub == null)
                return;
            bmdVideohub.IpAddress = ipAddressTextbox.Text;
            bmdVideohub.AutoReconnect = autoReconnectCheckbox.Checked;
        }

        protected override void validateFields()
        {
            base.validateFields();
            BmdVideohub bmdVideohub = (BmdVideohub)EditedModel;
            if (bmdVideohub == null)
                return;
            bmdVideohub.ValidateIpAddress(ipAddressTextbox.Text);
        }

        private void connectButton_Click(object sender, EventArgs e) => (EditedModel as BmdVideohub)?.Connect();
        private void disconnectButton_Click(object sender, EventArgs e) => (EditedModel as BmdVideohub)?.Disconnect();

        private void connectionStateChangedHandler(BmdVideohub router, bool oldState, bool newState)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => connectionStateChangedHandler(router, oldState, newState)));
                return;
            }
            connectButton.Enabled = !newState;
            disconnectButton.Enabled = newState;
        }

    }

}
