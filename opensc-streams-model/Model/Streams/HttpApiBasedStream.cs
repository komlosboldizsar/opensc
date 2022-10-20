using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.Settings;
using OpenSC.Model.SourceGenerators;
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

    public abstract partial class HttpApiBasedStream : Stream
    {

        private const string LOG_TAG = "Stream/HttpApiBased";

        #region Instantiation, restoration, persistence
        public HttpApiBasedStream()
        {
            initRequestData();
            registerStreamForPeriodicUpdate(this);
        }

        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            initRequestData();
        }

        public override void Removed()
        {
            base.Removed();
            unregisterStreamForPeriodicUpdate(this);
        }
        #endregion

        #region Property: RefreshRate
        [AutoProperty]
        [AutoProperty.Validator(nameof(ValidateRefreshRate))]
        [PersistAs("refresh_rate")]
        private int refreshRate = 5;

        public void ValidateRefreshRate(int refreshRate)
        {
            if ((refreshRate < 1) || (refreshRate > 30))
                throw new ArgumentOutOfRangeException();
        }
        #endregion

        #region Property: RefreshEnabled
        [AutoProperty]
        [PersistAs("refresh_enabled")]
        private bool refreshEnabled = true;
        #endregion

        #region Update task and timer
        private static List<HttpApiBasedStream> registeredStreamsToUpdate = new();
        private static Task updateTask = null;

        private static void registerStreamForPeriodicUpdate(HttpApiBasedStream stream)
        {
            lock (registeredStreamsToUpdate)
            {
                if (registeredStreamsToUpdate.Contains(stream))
                    return;
                registeredStreamsToUpdate.Add(stream);
            }
            if (updateTask == null)
                updateTask = Task.Run(updateTaskMethod);
        }

        private static void unregisterStreamForPeriodicUpdate(HttpApiBasedStream stream)
            => registeredStreamsToUpdate.RemoveAll(s => (s == stream));

        private async static void updateTaskMethod()
        {
            while (true)
            {
                lock (registeredStreamsToUpdate)
                {
                    registeredStreamsToUpdate.ForEach(s => s.update1sTick());
                }
                await Task.Delay(1000);
            }
        }

        private int secondsSinceLastUpdate = -1;

        private void update1sTick()
        {
            if (_updateImmediately || (refreshEnabled && ((secondsSinceLastUpdate == -1) || (secondsSinceLastUpdate >= refreshRate))))
            {
                doHttpRequest();
                secondsSinceLastUpdate = 0;
                _updateImmediately = false;
            }
            else
            {
                secondsSinceLastUpdate++;
            }
        }

        public void UpdateImmediately()
            => _updateImmediately = true;

        private bool _updateImmediately = false;
        #endregion

        #region Update HTTP request and response processing
        protected string RequestApiUrl = "http://localhost";
        protected string RequestContentType = "application/json; charset=utf-8";
        protected WebHeaderCollection RequestHeaders = new();

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
                using System.IO.Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                processResponse(reader.ReadToEnd());
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
