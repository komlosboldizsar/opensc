using OpenSC.Model.Routers;
using OpenSC.Model.Routers.BlackMagicDesign;
using System;

namespace OpenSC.GUI.Routers
{

    public partial class BmdVideohubEditorForm : RouterEditorFormBase, IModelEditorForm<Router>
    {

        public BmdVideohubEditorForm() : base()
        {
            InitializeComponent();
        }

        public BmdVideohubEditorForm(Router router) : base(router)
        {
            InitializeComponent();
            if (router == null)
                this.router = new BmdVideohub();
            else if (!(router is BmdVideohub))
                throw new ArgumentException();
        }

        public IModelEditorForm<Router> GetInstance(Router modelInstance)
        {
            return new BmdVideohubEditorForm(modelInstance);
        }

        protected override void loadData()
        {
            base.loadData();
            BmdVideohub bmdVideohub = router as BmdVideohub;
            if (bmdVideohub == null)
                return;

            ipAddressTextbox.Text = bmdVideohub.IpAddress;

            bmdVideohub.ConnectionStateChanged += connectionStateChangedHandler;
            connectButton.Enabled = !bmdVideohub.Connected;
            disconnectButton.Enabled = bmdVideohub.Connected;

        }

        protected override void writeFields()
        {

            base.writeFields();
            BmdVideohub bmdVideohub = router as BmdVideohub;
            if (bmdVideohub == null)
                return;

            bmdVideohub.IpAddress = ipAddressTextbox.Text;

        }

        protected override void validateFields()
        {

            base.validateFields();
            BmdVideohub bmdVideohub = router as BmdVideohub;
            if (bmdVideohub == null)
                return;

            bmdVideohub.ValidateIpAddress(ipAddressTextbox.Text);

        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            (router as BmdVideohub)?.Connect();
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            (router as BmdVideohub)?.Disconnect();
        }

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
