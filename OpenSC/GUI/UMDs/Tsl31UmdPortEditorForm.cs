using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.TSL31;
using System;

namespace OpenSC.GUI.UMDs
{

    public partial class Tsl31UmdPortEditorForm : SerialUmdPortEditorForm, IModelEditorForm<UmdPort>
    {

        public Tsl31UmdPortEditorForm() : base()
        {
            InitializeComponent();
        }

        public Tsl31UmdPortEditorForm(UmdPort port) : base(port)
        {
            InitializeComponent();
            if (port == null)
                this.port = new TSL31Port();
            else if (!(port is TSL31Port))
                throw new ArgumentException();
        }

        public IModelEditorForm<UmdPort> GetInstance(UmdPort modelInstance)
        {
            return new Tsl31UmdPortEditorForm(modelInstance);
        }

        protected override void loadData()
        {
            base.loadData();
            TSL31Port tsl31Port = port as TSL31Port;
            if (tsl31Port == null)
                return;
            //videoIdTextBox.Text = youtubeStream.VideoId;
        }

        protected override void writeFields()
        {
            base.writeFields();
            TSL31Port tsl31Port = port as TSL31Port;
            if (tsl31Port == null)
                return;
            //youtubeStream.VideoId = videoIdTextBox.Text;
        }

        protected override void validateFields()
        {
            base.validateFields();
            TSL31Port tsl31Port = port as TSL31Port;
            if (tsl31Port == null)
                return;
            // TODO: Validation
        }

    }

}
