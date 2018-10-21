using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.McCurdy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.UMDs
{

    public partial class McCurdyUmdPortEditorForm : SerialUmdPortEditorForm, IModelEditorForm<UmdPort>
    {

        public McCurdyUmdPortEditorForm() : base()
        {
            InitializeComponent();
        }

        public McCurdyUmdPortEditorForm(UmdPort port) : base(port)
        {
            InitializeComponent();
            if (port == null)
                this.port = new McCurdyPort();
            else if (!(port is McCurdyPort))
                throw new ArgumentException();
        }

        public IModelEditorForm<UmdPort> GetInstance(UmdPort modelInstance)
        {
            return new McCurdyUmdPortEditorForm(modelInstance);
        }

        protected override void loadData()
        {
            base.loadData();
            McCurdyPort mcCurdyPort = port as McCurdyPort;
            if (mcCurdyPort == null)
                return;
            //videoIdTextBox.Text = youtubeStream.VideoId;
        }

        protected override void writeFields()
        {
            base.writeFields();
            McCurdyPort mcCurdyPort = port as McCurdyPort;
            if (mcCurdyPort == null)
                return;
            //youtubeStream.VideoId = videoIdTextBox.Text;
        }

        protected override void validateFields()
        {
            base.validateFields();
            McCurdyPort mcCurdyPort = port as McCurdyPort;
            if (mcCurdyPort == null)
                return;
            // TODO: Validation
        }

    }

}
