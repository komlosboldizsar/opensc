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

        private const string LOG_TAG = "Stream/YouTube";

        public static readonly Setting<string> ApiKeySetting = new Setting<string>(
            "streams.youtubestream.apikey",
            "Streams",
            "YouTube API key",
            "Get this from Google Developer Console!"
        );

        private const string API_URL = "https://www.googleapis.com/youtube/v3/videos?id={0}&part=liveStreamingDetails&key={1}";

        [PersistAs("video_id")]
        private string videoId;

        public string VideoId
        {
            get { return videoId; }
            set { videoId = value; }
        }

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

        public YoutubeStream()
        {
            createAndStartUpdaterThread();
        }

        public override void Restored()
        { }

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
                    return;
                }

                JToken firstItem = json["items"][0];
                JToken liveStreamingDetails = firstItem["liveStreamingDetails"];

                string actualEndTime = liveStreamingDetails["actualEndTime"]?.ToString();
                if (!string.IsNullOrEmpty(actualEndTime))
                {
                    State = StreamState.Ended;
                    return;
                }

                string actualStartTime = liveStreamingDetails["actualStartTime"]?.ToString();
                if (!string.IsNullOrEmpty(actualStartTime))
                {
                    State = StreamState.Running;
                    if (int.TryParse(liveStreamingDetails["concurrentViewers"]?.ToString(), out int viewerCount))
                        ViewerCount = viewerCount;
                    else
                        ViewerCount = 0;
                    return;
                }

                string scheduledStartTime = liveStreamingDetails["scheduledStartTime"]?.ToString();
                if (!string.IsNullOrEmpty(scheduledStartTime))
                {
                    State = StreamState.NotStarted;
                    return;
                }

                State = StreamState.Unknown;

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

    }
}
