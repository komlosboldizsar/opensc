using OpenSC.Model.Mixers;
using OpenSC.Model.Mixers.BlackMagicDesign;
using System;

namespace OpenSC.GUI.Mixers
{

    public partial class BmdMixerEditorForm : MixerEditorFormBase, IModelEditorForm<Mixer>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Mixer);
        public IModelEditorForm<Mixer> GetInstanceT(Mixer modelInstance) => new BmdMixerEditorForm(modelInstance);

        public BmdMixerEditorForm() : base() => InitializeComponent();

        public BmdMixerEditorForm(Mixer mixer) : base(mixer)
        {
            InitializeComponent();
            if ((mixer != null) && !(mixer is BmdMixer))
                throw new ArgumentException($"Type of mixer should be {nameof(BmdMixer)}.", nameof(mixer));
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<Mixer, BmdMixer>(this, MixerDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            BmdMixer bmdMixer = (BmdMixer)EditedModel;
            if (bmdMixer == null)
                return;
            ipAddressInput.Text = bmdMixer.IpAddress;
            autoReconnectCheckbox.Checked = bmdMixer.AutoReconnect;
            bmdMixer.ConnectionStateChanged += connectionStateChangedHandler;
            connectButton.Enabled = !bmdMixer.ConnectionState;
            disconnectButton.Enabled = bmdMixer.ConnectionState;
        }

        protected override void writeFields()
        {
            base.writeFields();
            BmdMixer bmdMixer = (BmdMixer)EditedModel;
            if (bmdMixer == null)
                return;
            bmdMixer.IpAddress = ipAddressInput.Text;
            bmdMixer.AutoReconnect = autoReconnectCheckbox.Checked;
        }

        protected override void validateFields()
        {
            base.validateFields();
            BmdMixer bmdMixer = (BmdMixer)EditedModel;
            if (bmdMixer == null)
                return;
            bmdMixer.ValidateIpAddress(ipAddressInput.Text);
        }

        private void connectButton_Click(object sender, EventArgs e) => (EditedModel as BmdMixer)?.Connect();
        private void disconnectButton_Click(object sender, EventArgs e) => (EditedModel as BmdMixer)?.Disconnect();

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
