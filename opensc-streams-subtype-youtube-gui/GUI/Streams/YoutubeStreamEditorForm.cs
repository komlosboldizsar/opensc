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

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Stream);
        public IModelEditorForm<Stream> GetInstanceT(Stream modelInstance) => new YoutubeStreamEditorForm(modelInstance);

        public YoutubeStreamEditorForm() : base() => InitializeComponent();

        public YoutubeStreamEditorForm(Stream stream) : base(stream)
        {
            InitializeComponent();
            if ((stream != null) && !(stream is YoutubeStream))
                throw new ArgumentException($"Type of stream should be {nameof(YoutubeStream)}.", nameof(stream));
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<Stream, YoutubeStream>(this, StreamDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            YoutubeStream youtubeStream = (YoutubeStream)EditedModel;
            if (youtubeStream == null)
                return;
            videoIdTextBox.Text = youtubeStream.VideoId;
        }

        protected override void writeFields()
        {
            base.writeFields();
            YoutubeStream youtubeStream = (YoutubeStream)EditedModel;
            if (youtubeStream == null)
                return;
            youtubeStream.VideoId = videoIdTextBox.Text;
        }

        protected override void validateFields()
        {
            base.validateFields();
            YoutubeStream youtubeStream = (YoutubeStream)EditedModel;
            if (youtubeStream == null)
                return;
        }

    }
}
