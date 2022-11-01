using OpenSC.Model.Streams;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace OpenSC.GUI.Streams
{
    public partial class YoutubeStreamEditorForm : HttpApiBasedStreamEditorFormBase, IModelEditorForm<Stream>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Stream);
        public IModelEditorForm<Stream> GetInstanceT(Stream modelInstance) => new YoutubeStreamEditorForm(modelInstance);

        public YoutubeStreamEditorForm() : base()
        {
            InitializeComponent();
            initPasteTextbox();
        }

        public YoutubeStreamEditorForm(Stream stream) : base(stream)
        {
            InitializeComponent();
            initPasteTextbox();
            if ((stream != null) && (stream is not YoutubeStream))
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

        private void initPasteTextbox()
            => videoIdTextBox.PastedTextConverter = videoIdPastedTextConverter;

        private string videoIdPastedTextConverter(string clipboardText)
        {
            if (VIDEO_ID_REGEX.IsMatch(clipboardText))
                return clipboardText;
            try
            {
                string clipboardTextWithProtocol = clipboardText;
                if (YOUTUBE_URI_REGEX_START.IsMatch(clipboardTextWithProtocol))
                    clipboardTextWithProtocol = $"https://{clipboardTextWithProtocol}";
                Uri uri = new(clipboardTextWithProtocol);
                Func<Uri, string>[] converters = { convertUriPublic, convertUriPublicShort, convertUriStudio };
                foreach (Func<Uri, string> coverter in converters)
                {
                    string videoId = coverter(uri);
                    if (videoId != null)
                        return videoId;
                }
                throw new StreamIdPasteTextBox.PastedTextConversionException($"Pasted text [{clipboardText}] is not a valid YouTube video URL.");
            }
            catch (UriFormatException)
            {
                throw new StreamIdPasteTextBox.PastedTextConversionException($"Pasted text [{clipboardText}] couldn't be recognized as a YouTube video ID nor URL.");
            }
        }

        private readonly Regex VIDEO_ID_REGEX = new("^[a-zA-Z0-9_]{11}$");
        private readonly Regex YOUTUBE_URI_REGEX_START = new("^((((www|m|studio)\\.)?youtube\\.com)|(youtu\\.be))");

        private string convertUriPublic(Uri uri)
        {
            string videoId = HttpUtility.ParseQueryString(uri.Query).Get("v");
            if (!YOUTUBE_PUBLIC_HOST_REGEX.IsMatch(uri.Host))
                return null;
            if (uri.LocalPath != "/watch")
                return null;
            if (videoId == null)
                return null;
            if (!VIDEO_ID_REGEX.IsMatch(videoId))
                return null;
            return videoId;
        }

        private readonly Regex YOUTUBE_PUBLIC_HOST_REGEX = new("^((www|m)\\.)?youtube.com$");

        private string convertUriPublicShort(Uri uri)
        {
            if (uri.Host != YOUTUBE_PUBLIC_SHORT_HOST)
                return null;
            if (uri.Segments.Length < 2)
                return null;
            string videoId = uri.Segments[1][0..11];
            if (!VIDEO_ID_REGEX.IsMatch(videoId))
                return null;
            return videoId;
        }

        private const string YOUTUBE_PUBLIC_SHORT_HOST = "youtu.be";

        private string convertUriStudio(Uri uri)
        {
            if (uri.Host != YOUTUBE_STUDIO_HOST)
                return null;
            if (uri.Segments.Length < 3)
                return null;
            if (uri.Segments[1] != "video/")
                return null;
            string videoId = uri.Segments[2][0..11];
            if (!VIDEO_ID_REGEX.IsMatch(videoId))
                return null;
            return videoId;
        }

        private const string YOUTUBE_STUDIO_HOST = "studio.youtube.com";

    }
}
