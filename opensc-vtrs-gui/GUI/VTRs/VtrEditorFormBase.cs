using OpenSC.Model.VTRs;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.VTRs
{

    public partial class VtrEditorFormBase : ModelEditorFormBase
    {

        public VtrEditorFormBase() : base() => InitializeComponent();
        public VtrEditorFormBase(Vtr vtr) : base(vtr) => InitializeComponent();

        protected override void loadData()
        {
            base.loadData();
            Vtr vtr = (Vtr)EditedModel;
            if (vtr == null)
                return;
        }

        protected override void validateFields()
        {
            base.validateFields();
            Vtr vtr = (Vtr)EditedModel;
            if (vtr == null)
                return;
            vtr.ValidateId((int)idNumericField.Value);
        }

        protected override void writeFields()
        {
            base.writeFields();
            Vtr vtr = (Vtr)EditedModel;
            if (vtr == null)
                return;
        }

    }

}
