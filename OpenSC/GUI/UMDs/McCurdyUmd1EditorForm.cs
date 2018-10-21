using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.McCurdy;
using System;

namespace OpenSC.GUI.UMDs
{

    public partial class McCurdyUmd1EditorForm : UmdEditorFormBase, IModelEditorForm<UMD>
    {

        public McCurdyUmd1EditorForm(): base()
        {
            InitializeComponent();
        }

        public McCurdyUmd1EditorForm(UMD umd) : base(umd)
        {
            InitializeComponent();
            if (umd == null)
                this.umd = new McCurdyUMD1();
            else if (!(umd is McCurdyUMD1))
                throw new ArgumentException();
        }

        public IModelEditorForm<UMD> GetInstance(UMD modelInstance)
        {
            return new McCurdyUmd1EditorForm(modelInstance);
        }

        protected override void loadData()
        {
            base.loadData();
            McCurdyUMD1 mcCurdyUmd = umd as McCurdyUMD1;
            if (mcCurdyUmd == null)
                return;
            //videoIdTextBox.Text = youtubeStream.VideoId;
        }

        protected override void writeFields()
        {
            base.writeFields();
            McCurdyUMD1 mcCurdyUmd = umd as McCurdyUMD1;
            if (mcCurdyUmd == null)
                return;
            //youtubeStream.VideoId = videoIdTextBox.Text;
        }

        protected override void validateFields()
        {
            base.validateFields();
            McCurdyUMD1 mcCurdyUmd = umd as McCurdyUMD1;
            if (mcCurdyUmd == null)
                return;
            // TODO: Validation
        }

    }

}
