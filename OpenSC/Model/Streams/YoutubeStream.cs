using Newtonsoft.Json.Linq;
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
    class YoutubeStream: Stream
    {

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
        {
            base.Restored();
            createAndStartUpdaterThread();
        }

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
            /*string url = string.Format(API_URL, videoId, ApiKeySetting.Value);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (System.IO.Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                processResponse(reader.ReadToEnd());
            }*/
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
            catch
            {
                State = StreamState.Unknown;
            }
            
        }

    }
}
