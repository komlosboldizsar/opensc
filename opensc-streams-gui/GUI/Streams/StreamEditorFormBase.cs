using OpenSC.Model.Streams;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Streams
{

    public partial class StreamEditorFormBase : ModelEditorFormBase
    {

        public StreamEditorFormBase() : base() => InitializeComponent();
        public StreamEditorFormBase(Stream stream) : base(stream) => InitializeComponent();

        protected override void loadData()
        {
            base.loadData();
            Stream stream = (Stream)EditedModel;
            if (stream == null)
                return;
        }

        protected override void validateFields()
        {
            base.validateFields();
            Stream stream = (Stream)EditedModel;
            if (stream == null)
                return;
        }

        protected override void writeFields()
        {
            base.writeFields();
            Stream stream = (Stream)EditedModel;
            if (stream == null)
                return;
        }

    }

}
