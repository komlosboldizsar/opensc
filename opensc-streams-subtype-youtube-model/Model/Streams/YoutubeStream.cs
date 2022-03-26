using Newtonsoft.Json.Linq;
using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.Streams
{

    [TypeLabel("YouTube stream")]
    [TypeCode("youtube")]
    public class YoutubeStream : HttpApiBasedStream
    {

        private const string LOG_TAG = "Stream/YouTube";

        #region Persistence, instantiation
        public YoutubeStream()
            => ApiKeySetting.ValueChanged += apiKeySettingValueChangedHandler;

        public override void Removed()
        {
            base.Removed();
            ApiKeySetting.ValueChanged -= apiKeySettingValueChangedHandler;
        }
        #endregion

        #region Settings
        public static readonly Setting<string> ApiKeySetting = new Setting<string>(
            "streams.youtubestream.apikey",
            "Streams",
            "YouTube API key",
            "Get this from Google Developer Console!"
        );

        private void apiKeySettingValueChangedHandler(ISetting setting, object oldValue, object newValue)
            => updateRequestDataBeforeRequest();
        #endregion

        #region Property: VideoId
        public event PropertyChangedTwoValuesDelegate<YoutubeStream, string> VideoIdChanged;

        [PersistAs("video_id")]
        private string videoId;

        public string VideoId
        {
            get => videoId;
            set => this.setProperty(ref videoId, value, VideoIdChanged, null, (ov, nv) => setRequestApiUrl());
        }
        #endregion

        #region Request data
        protected override void initRequestData()
        {
            setRequestApiUrl();
            RequestContentType = "application/json; charset=utf-8";
            RequestHeaders = new WebHeaderCollection();
        }

        private const string API_BASE_URL = "https://www.googleapis.com/youtube/v3/videos?id={0}&part=liveStreamingDetails&key={1}";

        private void setRequestApiUrl()
            => RequestApiUrl = string.Format(API_BASE_URL, videoId, ApiKeySetting.Value);

        protected override void processResponse(string responseBody)
        {
            JObject json = JObject.Parse(responseBody);
            try
            {
                int totalResults = json["pageInfo"]["totalResults"].ToObject<int>();
                if (totalResults == 0) {
                    State = StreamState.Unknown;
                    ViewerCount = null;
                    return;
                }
                JToken firstItem = json["items"][0];
                JToken liveStreamingDetails = firstItem["liveStreamingDetails"];
                string actualEndTime = liveStreamingDetails["actualEndTime"]?.ToString();
                if (!string.IsNullOrEmpty(actualEndTime))
                {
                    State = StreamState.Ended;
                    ViewerCount = null;
                    return;
                }
                string actualStartTime = liveStreamingDetails["actualStartTime"]?.ToString();
                if (!string.IsNullOrEmpty(actualStartTime))
                {
                    State = StreamState.Running;
                    if (int.TryParse(liveStreamingDetails["concurrentViewers"]?.ToString(), out int viewerCount))
                        ViewerCount = viewerCount;
                    else
                        ViewerCount = null;
                    return;
                }
                string scheduledStartTime = liveStreamingDetails["scheduledStartTime"]?.ToString();
                if (!string.IsNullOrEmpty(scheduledStartTime))
                {
                    State = StreamState.NotStarted;
                    ViewerCount = null;
                    return;
                }
                State = StreamState.Unknown;
                ViewerCount = null;
            }
            catch (Exception ex)
            {
                LogDispatcher.E(LOG_TAG, $"Couldn't process response of YouTube API while tried to get state of stream [{this}], video ID: [{videoId}]. Exception message: [{ex.Message}]");
                State = StreamState.Unknown;
            }
        }
        #endregion

    }

}
