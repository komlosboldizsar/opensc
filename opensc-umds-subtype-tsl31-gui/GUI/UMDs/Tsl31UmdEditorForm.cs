using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.SerialPorts;
using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.Tsl31;
using System;

namespace OpenSC.GUI.UMDs
{

    public partial class Tsl31UmdEditorForm : UmdEditorFormBase, IModelEditorForm<Umd>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Umd);
        public IModelEditorForm<Umd> GetInstanceT(Umd modelInstance) => new Tsl31UmdEditorForm(modelInstance);

        public Tsl31UmdEditorForm(): base() => InitializeComponent();

        public Tsl31UmdEditorForm(Umd umd) : base(umd)
        {
            InitializeComponent();
            if ((umd != null) && !(umd is Tsl31))
                throw new ArgumentException($"Type of UMD should be {nameof(Tsl31)}.", nameof(umd));
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<Umd, Tsl31>(this, UmdDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            Tsl31 tsl31 = (Tsl31)EditedModel;
            if (tsl31 == null)
                return;
            portDropDown.SelectByValue(tsl31.Port);
            addressNumericInput.Value = tsl31.Address;
        }

        protected override void writeFields()
        {
            base.writeFields();
            Tsl31 tsl31 = (Tsl31)EditedModel;
            if (tsl31 == null)
                return;
            tsl31.Port = portDropDown.SelectedValue as SerialPort;
            tsl31.Address = (int)addressNumericInput.Value;
        }

        protected override void validateFields()
        {
            base.validateFields();
            Tsl31 tsl31 = (Tsl31)EditedModel;
            if (tsl31 == null)
                return;
            tsl31.ValidateAddress((int)addressNumericInput.Value);
        }

        private void initPortDropDown()
        {
            portDropDown.CreateAdapterAsDataSource(SerialPortDatabase.Instance, null, true, "(not associated)");
            portDropDown.ReceiveSystemObjectDrop().FilterByType<SerialPort>();
        }

    }

}
