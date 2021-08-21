using OpenSC.Model.Mixers;
using OpenSC.Model.Mixers.BlackMagicDesign;
using System;

namespace OpenSC.GUI.Mixers
{

    public partial class BmdMixerEditorForm : MixerEditorFormBase, IModelEditorForm<Mixer>
    {

        public BmdMixerEditorForm() : base()
        {
            InitializeComponent();
        }

        public BmdMixerEditorForm(Mixer mixer) : base(mixer)
        {
            InitializeComponent();
            if (mixer == null)
                this.mixer = new BmdMixer();
            else if (!(mixer is BmdMixer))
                throw new ArgumentException();
        }

        public IModelEditorForm<Mixer> GetInstance(Mixer modelInstance)
        {
            return new BmdMixerEditorForm(modelInstance);
        }

        protected override void loadData()
        {
            base.loadData();
            BmdMixer bmdMixer = mixer as BmdMixer;
            if (bmdMixer == null)
                return;

            ipAddressTextbox.Text = bmdMixer.IpAddress;
            autoReconnectCheckbox.Checked = bmdMixer.AutoReconnect;

            bmdMixer.ConnectionStateChanged += connectionStateChangedHandler;
            connectButton.Enabled = !bmdMixer.ConnectionState;
            disconnectButton.Enabled = bmdMixer.ConnectionState;

        }

        protected override void writeFields()
        {

            base.writeFields();
            BmdMixer bmdMixer = mixer as BmdMixer;
            if (bmdMixer == null)
                return;

            bmdMixer.IpAddress = ipAddressTextbox.Text;
            bmdMixer.AutoReconnect = autoReconnectCheckbox.Checked;

        }

        protected override void validateFields()
        {

            base.validateFields();
            BmdMixer bmdMixer = mixer as BmdMixer;
            if (bmdMixer == null)
                return;

            bmdMixer.ValidateIpAddress(ipAddressTextbox.Text);

        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            (mixer as BmdMixer)?.Connect();
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            (mixer as BmdMixer)?.Disconnect();
        }

        private void connectionStateChangedHandler(BmdMixer mixer, bool oldState, bool newState)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => connectionStateChangedHandler(mixer, oldState, newState)));
                return;
            }
            connectButton.Enabled = !newState;
            disconnectButton.Enabled = newState;
        }

    }

}
