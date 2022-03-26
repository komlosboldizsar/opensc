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

    public abstract class HttpApiBasedStream : Stream
    {

        private const string LOG_TAG = "Stream/HttpApiBased";

        #region Instantiation, restoration, persistence
        public HttpApiBasedStream() => registerStreamForPeriodicUpdate(this);

        public override void Removed()
        {
            base.Removed();
            unregisterStreamForPeriodicUpdate(this);
        }
        #endregion

        #region Property: RefreshRate
        public event PropertyChangedTwoValuesDelegate<HttpApiBasedStream, int> RefreshRateChanged;

        [PersistAs("refresh_rate")]
        private int refreshRate = 5;

        public int RefreshRate
        {
            get => refreshRate;
            set => this.setProperty(ref refreshRate, value, RefreshRateChanged, validator: ValidateRefreshRate);
        }

        public void ValidateRefreshRate(int refreshRate)
        {
            if ((refreshRate < 1) || (refreshRate > 30))
                throw new ArgumentOutOfRangeException();
        }
        #endregion

        #region Property: RefreshEnabled
        public event PropertyChangedTwoValuesDelegate<HttpApiBasedStream, bool> RefreshEnabledChanged;

        [PersistAs("refresh_enabled")]
        private bool refreshEnabled = true;

        public bool RefreshEnabled
        {
            get => refreshEnabled;
            set => this.setProperty(ref refreshEnabled, value, RefreshEnabledChanged);
        }
        #endregion

        #region Update task and timer
        private static List<HttpApiBasedStream> registeredStreamsToUpdate = new();
        private static Task updateTask = null;

        private static void registerStreamForPeriodicUpdate(HttpApiBasedStream stream)
        {
            if (registeredStreamsToUpdate.Contains(stream))
                return;
            registeredStreamsToUpdate.Add(stream);
            if (updateTask == null)
                updateTask = Task.Run(updateTaskMethod);
        }

        private static void unregisterStreamForPeriodicUpdate(HttpApiBasedStream stream)
            => registeredStreamsToUpdate.RemoveAll(s => (s == stream));

        private static void updateTaskMethod()
        {
            while (true)
            {
                registeredStreamsToUpdate.ForEach(s => s.update1sTick());
                Task.Delay(1000);
            }
        }

        private int secondsSinceLastUpdate = -1;

        private void update1sTick()
        {
            if (refreshEnabled && ((secondsSinceLastUpdate == -1) || (secondsSinceLastUpdate >= refreshRate)))
            {
                doHttpRequest();
                secondsSinceLastUpdate = 0;
            }
            else
            {
                secondsSinceLastUpdate++;
            }
        }
        #endregion

        #region Update HTTP request and response processing
        protected string RequestApiUrl = "http://localhost";
        protected string RequestContentType = "application/json; charset=utf-8";
        protected WebHeaderCollection RequestHeaders = new WebHeaderCollection();

        protected abstract void initRequestData();
        protected virtual void updateRequestDataBeforeRequest() { }

        private void doHttpRequest()
        {
            try
            {
                updateRequestDataBeforeRequest();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RequestApiUrl);
                request.ContentType = RequestContentType;
                request.Headers = RequestHeaders;
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
                LogDispatcher.E(LOG_TAG, $"Error occurred while trying to get state of stream [{this}] using REST API. Exception message: [{ex.Message}]");
            }
        }

        protected abstract void processResponse(string responseBody);
        #endregion

    }
}
