using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.TSL31;
using System;

namespace OpenSC.GUI.UMDs
{

    public partial class Tsl31UmdEditorForm : UmdEditorFormBase, IModelEditorForm<UMD>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as UMD);
        public IModelEditorForm<UMD> GetInstanceT(UMD modelInstance) => new Tsl31UmdEditorForm(modelInstance);

        public Tsl31UmdEditorForm(): base()
        {
            InitializeComponent();
        }

        public Tsl31UmdEditorForm(UMD umd) : base(umd)
        {
            InitializeComponent();
            if (umd == null)
                this.umd = new TSL31();
            else if (!(umd is TSL31))
                throw new ArgumentException();
        }

        protected override void loadData()
        {
            base.loadData();
            TSL31 tsl31Umd = umd as TSL31;
            if (tsl31Umd == null)
                return;
            //videoIdTextBox.Text = youtubeStream.VideoId;
        }

        protected override void writeFields()
        {
            base.writeFields();
            TSL31 tsl31Umd = umd as TSL31;
            if (tsl31Umd == null)
                return;
            //youtubeStream.VideoId = videoIdTextBox.Text;
        }

        protected override void validateFields()
        {
            base.validateFields();
            TSL31 tsl31Umd = umd as TSL31;
            if (tsl31Umd == null)
                return;
            // TODO: Validation
        }

    }

}
