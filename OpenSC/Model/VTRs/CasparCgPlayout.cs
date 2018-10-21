using Bespoke.Common.Osc;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.VTRs
{
    [TypeLabel("CasparCG playout")]
    [TypeCode("casparcg")]
    public class CasparCgPlayout: Vtr
    {

        [PersistAs("listened_ip")]
        private string listenedIP;

        public string ListenedIP
        {
            get { return listenedIP; }
            set { listenedIP = value; }
        }

        [PersistAs("watched_channel")]
        private int watchedChannel;

        public int WatchedChannel
        {
            get { return watchedChannel; }
            set
            {
                if (watchedChannel == value)
                    return;
                unsubscribeFromChannelLayer(this);
                watchedChannel = value;
                subscribeToChannelLayer(this);
            }
        }

        [PersistAs("watched_layer")]
        private int watchedLayer;

        public int WatchedLayer
        {
            get { return watchedLayer; }
            set
            {
                if (watchedLayer == value)
                    return;
                unsubscribeFromChannelLayer(this);
                watchedLayer = value;
                subscribeToChannelLayer(this);
            }
        }

        public override void Restored()
        {
            base.Restored();
            createAndStartStoppedStateDetectorThread();
            subscribeToChannelLayer(this);
        }

        private object stateUpdatingLock = new object();

        private void processOscMessage(OscMessage message, string subaddress)
        {
            try
            {
                lock (stateUpdatingLock)
                {
                    switch (subaddress)
                    {
                        case "file/path":
                            Title = message.Data[0].ToString();
                            break;
                        case "file/time":
                            float elapsedTime = (float)message.Data[0];
                            int tElapsed = Convert.ToInt32(elapsedTime);
                            int tFull = Convert.ToInt32(message.Data[1]);
                            if ((lastElapsedTime != -1) && (elapsedTime != lastElapsedTime))
                            {
                                lastElapsedTimeUpdate = DateTime.Now;
                                State = VtrState.Playing;
                            }
                            SecondsElapsed = tElapsed;
                            SecondsFull = tFull;
                            SecondsRemaining = tFull - tElapsed;
                            lastElapsedTime = elapsedTime;
                            break;
                        case "paused":
                            isPaused = (message.Data[0].ToString() == "True");
                            if (isPaused)
                                State = VtrState.Paused;
                            break;
                    }
                }
            }
            catch { }
        }

        private bool isPaused = false;
        private float lastElapsedTime = -1;
        private DateTime lastElapsedTimeUpdate = DateTime.Now;

        private Thread stoppedStateDetectorThread;
        private const int STOPPED_STATE_DIFFERENCE_MILLISECONDS = 1000;

        private void stoppedStateDetectorThreadMethod()
        {
            while (true)
            {
                lock (stateUpdatingLock)
                {
                    if (isPaused)
                        continue;
                    TimeSpan diff = DateTime.Now - lastElapsedTimeUpdate;
                    if (diff.TotalMilliseconds > STOPPED_STATE_DIFFERENCE_MILLISECONDS)
                        State = VtrState.Stopped;
                }
                Thread.Sleep(STOPPED_STATE_DIFFERENCE_MILLISECONDS);
            }
        }

        private void createAndStartStoppedStateDetectorThread()
        {
            stoppedStateDetectorThread = new Thread(stoppedStateDetectorThreadMethod)
            {
                IsBackground = true
            };
            stoppedStateDetectorThread.Start();
        }

        #region Common OSC listener
        private const int OSC_PORT = 5253;
        private static OscServer oscServer = new OscServer(TransportType.Udp, IPAddress.Any, OSC_PORT);

        private static void initOscServer()
        {
            if (!oscServer.IsRunning)
            {
                oscServer.Start();
                oscServer.BundleReceived += oscBundleReceived;
            }
        }

        private const string MESSAGE_ADDRESS_REGEXP = @"^/channel/(?<channel>[0-9]+)/stage/layer/(?<layer>[0-9]+)/(?<subaddress>.+)";
        private static readonly Regex messageAddressRegexp = new Regex(MESSAGE_ADDRESS_REGEXP, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static void oscBundleReceived(object sender, OscBundleReceivedEventArgs e)
        {
            var sourceIP = e.Bundle.SourceEndPoint.Address.ToString();
            foreach(var data in e.Bundle.Data)
            {

                OscMessage message = data as OscMessage;
                if (message == null)
                    continue;
                MatchCollection messageAddressRegexpMatches = messageAddressRegexp.Matches(message.Address);
                if (messageAddressRegexpMatches.Count != 1)
                    continue;

                string channel = messageAddressRegexpMatches[0].Groups["channel"].Value;
                string layer = messageAddressRegexpMatches[0].Groups["layer"].Value;

                string subscriptionsKey = string.Format("{0},{1},{2}", sourceIP, channel, layer);
                if (!channelLayerSubscriptions.TryGetValue(subscriptionsKey, out List<CasparCgPlayout> subscriptions))
                    continue;

                string subaddress = messageAddressRegexpMatches[0].Groups["subaddress"].Value;
                subscriptions.ForEach(instance => instance.processOscMessage(message, subaddress));

            }
        }

        private static Dictionary<string, List<CasparCgPlayout>> channelLayerSubscriptions = new Dictionary<string, List<CasparCgPlayout>>();

        private static List<CasparCgPlayout> getSubscribtionsForChannelLayer(string ip, int channel, int layer)
        {
            if (channel < 1)
                throw new ArgumentOutOfRangeException();
            if (layer < 1)
                throw new ArgumentOutOfRangeException();
            string key = string.Format("{0},{1},{2}", ip, channel, layer);
            if (!channelLayerSubscriptions.TryGetValue(key, out List<CasparCgPlayout> subscriptions))
                channelLayerSubscriptions.Add(key, new List<CasparCgPlayout>());
            return channelLayerSubscriptions[key];
        }

        private static void subscribeToChannelLayer(CasparCgPlayout instance)
        {
            List<CasparCgPlayout> subscribers = getSubscribtionsForChannelLayer(instance.ListenedIP, instance.WatchedChannel, instance.WatchedLayer);
            if (!subscribers.Contains(instance))
                subscribers.Add(instance);
            initOscServer();
        }

        private static void unsubscribeFromChannelLayer(CasparCgPlayout instance)
        {
            List<CasparCgPlayout> subscribers = getSubscribtionsForChannelLayer(instance.ListenedIP, instance.WatchedChannel, instance.WatchedLayer);
            subscribers.RemoveAll(i => (i == instance));
        }
        #endregion

    }

}
