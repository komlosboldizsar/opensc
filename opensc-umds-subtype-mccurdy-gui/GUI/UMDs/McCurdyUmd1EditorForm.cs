using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model;
using OpenSC.Model.SerialPorts;
using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.McCurdy;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OpenSC.GUI.UMDs
{

    public partial class McCurdyUmd1EditorForm : UmdEditorFormBase, IModelEditorForm<Umd>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Umd);
        public IModelEditorForm<Umd> GetInstanceT(Umd modelInstance) => new McCurdyUmd1EditorForm(modelInstance);

        public McCurdyUmd1EditorForm() : base() => InitializeComponent();

        public McCurdyUmd1EditorForm(Umd umd) : base(umd)
        {
            InitializeComponent();
            if ((umd != null) && !(umd is McCurdyUMD1))
                throw new ArgumentException($"Type of UMD should be {nameof(McCurdyUMD1)}.", nameof(umd));
            initDropDowns();
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<Umd, McCurdyUMD1>(this, UmdDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            McCurdyUMD1 mcCurdyUmd1 = (McCurdyUMD1)EditedModel;
            if (mcCurdyUmd1 == null)
                return;
            portDropDown.SelectByValue(mcCurdyUmd1.Port);
            addressNumericInput.Value = mcCurdyUmd1.Address;
        }

        protected override void writeFields()
        {
            base.writeFields();
            McCurdyUMD1 mcCurdyUmd1 = (McCurdyUMD1)EditedModel;
            if (mcCurdyUmd1 == null)
                return;
            mcCurdyUmd1.Port = portDropDown.SelectedValue as SerialPort;
            mcCurdyUmd1.Address = (int)addressNumericInput.Value;
        }

        protected override void validateFields()
        {
            base.validateFields();
            McCurdyUMD1 mcCurdyUmd1 = (McCurdyUMD1)EditedModel;
            if (mcCurdyUmd1 == null)
                return;
            mcCurdyUmd1.ValidateAddress((int)addressNumericInput.Value);
        }

        private void initDropDowns()
        {
            // Ports
            portDropDown.CreateAdapterAsDataSource(SerialPortDatabase.Instance, port => port.Name, true, "(not connected)");
            portDropDown.ReceiveSystemObjectDrop().FilterByType<SerialPort>();
        }

    }

}
