using OpenSC.Model.Streams;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Streams
{

    public partial class HttpApiBasedStreamEditorFormBase : StreamEditorFormBase
    {

        public HttpApiBasedStreamEditorFormBase() : base() => InitializeComponent();
        public HttpApiBasedStreamEditorFormBase(Stream stream) : base(stream) => InitializeComponent();

        protected override void loadData()
        {
            base.loadData();
            HttpApiBasedStream stream = (HttpApiBasedStream)EditedModel;
            if (stream == null)
                return;
            updateIntervalNumericField.Value = stream.RefreshRate;
        }

        protected override void validateFields()
        {
            base.validateFields();
            HttpApiBasedStream stream = (HttpApiBasedStream)EditedModel;
            if (stream == null)
                return;
        }

        protected override void writeFields()
        {
            base.writeFields();
            HttpApiBasedStream stream = (HttpApiBasedStream)EditedModel;
            if (stream == null)
                return;
            stream.RefreshRate = (int)updateIntervalNumericField.Value;
        }

    }

}
