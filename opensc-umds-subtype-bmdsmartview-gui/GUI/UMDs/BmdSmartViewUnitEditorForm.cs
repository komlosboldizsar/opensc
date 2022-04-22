using OpenSC.Model.UMDs.BmdSmartView;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.UMDs
{

    public partial class BmdSmartViewUnitEditorForm : ModelEditorFormBase, IModelEditorForm<BmdSmartViewUnit>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as BmdSmartViewUnit);
        public IModelEditorForm<BmdSmartViewUnit> GetInstanceT(BmdSmartViewUnit modelInstance) => new BmdSmartViewUnitEditorForm(modelInstance);

        public BmdSmartViewUnitEditorForm() : base() => InitializeComponent();
        public BmdSmartViewUnitEditorForm(BmdSmartViewUnit bmdSmartViewUnit) : base(bmdSmartViewUnit) => InitializeComponent();

        protected override void loadData()
        {
            base.loadData();
            BmdSmartViewUnit bmdSmartViewUnit = (BmdSmartViewUnit)EditedModel;
            if (bmdSmartViewUnit == null)
                return;
            ipAddressTextBox.Text = bmdSmartViewUnit.IpAddress;
            portNumericInput.Value = bmdSmartViewUnit.Port;
        }

        protected override void validateFields()
        {
            base.validateFields();
            BmdSmartViewUnit bmdSmartViewUnit = (BmdSmartViewUnit)EditedModel;
            if (bmdSmartViewUnit == null)
                return;
            bmdSmartViewUnit.IpAddress = ipAddressTextBox.Text;
            bmdSmartViewUnit.Port = (int)portNumericInput.Value;
        }

        protected override void writeFields()
        {
            base.writeFields();
            BmdSmartViewUnit bmdSmartViewUnit = (BmdSmartViewUnit)EditedModel;
            if (bmdSmartViewUnit == null)
                return;
            bmdSmartViewUnit.ValidatePort((int)portNumericInput.Value);
        }

    }

}
