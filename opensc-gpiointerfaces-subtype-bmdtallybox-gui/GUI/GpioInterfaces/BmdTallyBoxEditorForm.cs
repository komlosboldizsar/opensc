using OpenSC.Model.GpioInterfaces;
using OpenSC.Model.GpioInterfaces.BlackMagicDesign;
using System;

namespace OpenSC.GUI.GpioInterfaces
{

    public partial class BmdTallyBoxEditorForm : GpioInterfaceEditorFormBase, IModelEditorForm<GpioInterface>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as GpioInterface);
        public IModelEditorForm<GpioInterface> GetInstanceT(GpioInterface modelInstance) => new BmdTallyBoxEditorForm(modelInstance);

        public BmdTallyBoxEditorForm() : base() => InitializeComponent();

        public BmdTallyBoxEditorForm(GpioInterface router) : base(router)
        {
            InitializeComponent();
            if ((router != null) && !(router is BmdTallyBox))
                throw new ArgumentException($"Type of GPIO interface should be {nameof(BmdTallyBox)}.", nameof(router));
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<GpioInterface, BmdTallyBox>(this, GpioInterfaceDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            BmdTallyBox bmdTallyBox = (BmdTallyBox)EditedModel;
            if (bmdTallyBox == null)
                return;
            ipAddressInput.Text = bmdTallyBox.IpAddress;
        }

        protected override void writeFields()
        {
            base.writeFields();
            BmdTallyBox bmdTallyBox = (BmdTallyBox)EditedModel;
            if (bmdTallyBox == null)
                return;
            bmdTallyBox.IpAddress = ipAddressInput.Text;
        }

        protected override void validateFields()
        {
            base.validateFields();
            BmdTallyBox bmdTallyBox = (BmdTallyBox)EditedModel;
            if (bmdTallyBox == null)
                return;
            bmdTallyBox.ValidateIpAddress(ipAddressInput.Text);
        }

    }

}
