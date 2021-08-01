using Newtonsoft.Json.Linq;
using OpenSC.Logger;
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
    class YoutubeStream: Stream
    {

        #region Persistence, instantiation
        public YoutubeStream()
        {
            createAndStartUpdaterThread();
        }

        public override void Removed()
        {
            base.Removed();
            updaterThread.Abort();
            updaterThread = null;
        }
        #endregion

        #region Constants
        private const string LOG_TAG = "Stream/YouTube";

        private const string API_URL = "https://www.googleapis.com/youtube/v3/videos?id={0}&part=liveStreamingDetails&key={1}";
        #endregion

        #region Settings
        public static readonly Setting<string> ApiKeySetting = new Setting<string>(
            "streams.youtubestream.apikey",
            "Streams",
            "YouTube API key",
            "Get this from Google Developer Console!"
        );
        #endregion

        #region Property: VideoId
        [PersistAs("video_id")]
        private string videoId;

        public string VideoId
        {
            get { return videoId; }
            set { videoId = value; }
        }
        #endregion

        #region Property: RefreshRate
        [PersistAs("refresh_rate")]
        private int refreshRate = 5;

        public int RefreshRate
        {
            get { return refreshRate; }
            set
            {
                if ((value < 1) || (value > 30))
                    throw new ArgumentOutOfRangeException();
                refreshRate = value;
            }
        }
        #endregion

        #region Viewer count update: periodic thread
        private void createAndStartUpdaterThread()
        {
            updaterThread = new Thread(updaterThreadMethod)
            {
                IsBackground = true
            };
            updaterThread.Start();
        }

        private Thread updaterThread;

        private void updaterThreadMethod()
        {
            while (true)
            {
                doHttpRequest();
                Thread.Sleep(refreshRate * 1000);
            }
        }
        #endregion

        #region Viewer count update: HTTP request and response processing
        private void doHttpRequest()
        {
            try
            {
                string url = string.Format(API_URL, videoId, ApiKeySetting.Value);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/json; charset=utf-8";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (System.IO.Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    processResponse(reader.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                State = StreamState.Unknown;
                string logMessage = string.Format("Error occurred while trying to get state of a stream (ID: {0}) using YouTube API. Exception message: [{1}]",
                    videoId,
                    ex.Message);
                LogDispatcher.E(LOG_TAG, logMessage);
            }
        }

        private void processResponse(string responseBody)
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
                string logMessage = string.Format("Couldn't process response of YouTube API while tried to get state of a stream (ID: {0}). Exception message: [{1}]",
                    videoId,
                    ex.Message);
                LogDispatcher.E(LOG_TAG, logMessage);
                State = StreamState.Unknown;
            }
            
        }
        #endregion

    }
}
