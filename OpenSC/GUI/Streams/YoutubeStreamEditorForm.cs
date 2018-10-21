using OpenSC.Model.Streams;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.Streams
{
    public partial class YoutubeStreamEditorForm : StreamEditorFormBase, IModelEditorForm<Stream>
    {

        public YoutubeStreamEditorForm(): base()
        {
            InitializeComponent();
        }

        public YoutubeStreamEditorForm(Stream stream): base(stream)
        {
            InitializeComponent();
            if (stream == null)
                this.stream = new YoutubeStream();
            else if (!(stream is YoutubeStream))
                throw new ArgumentException();
        }

        public IModelEditorForm<Stream> GetInstance(Stream modelInstance)
        {
            return new YoutubeStreamEditorForm(modelInstance);
        }

        protected override void loadData()
        {
            base.loadData();
            YoutubeStream youtubeStream = stream as YoutubeStream;
            if (youtubeStream == null)
                return;
            videoIdTextBox.Text = youtubeStream.VideoId;
        }

        protected override void writeFields()
        {
            base.writeFields();
            YoutubeStream youtubeStream = stream as YoutubeStream;
            if (youtubeStream == null)
                return;
            youtubeStream.VideoId = videoIdTextBox.Text;
        }

        protected override void validateFields()
        {
            base.validateFields();
            YoutubeStream youtubeStream = stream as YoutubeStream;
            if (youtubeStream == null)
                return;
            // TODO: Validation
        }

    }
}
