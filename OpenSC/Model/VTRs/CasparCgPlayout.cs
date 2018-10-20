using Bespoke.Common.Osc;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenSC.Model.VTRs
{

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
                watchedChannel = value;
                subscribeToChannelLayer(this);
            }
        }

        public override void Restored()
        {
            base.Restored();
            subscribeToChannelLayer(this);
        }

        private void processOscMessage(OscMessage message, string subaddress)
        {
            switch(subaddress)
            {
                case "file/path":
                    Title = message.Data[0].ToString();
                    break;
                case "file/time":
                    if (!int.TryParse(message.Data[0].ToString(), out int tElapsed))
                        tElapsed = 0;
                    if (!int.TryParse(message.Data[1].ToString(), out int tFull))
                        tFull = 0;
                    SecondsElapsed = tElapsed;
                    SecondsFull = tFull;
                    SecondsRemaining = tFull - tElapsed;
                    break;
                case "paused":
                    State = (message.Data[0].ToString() == "True") ? VtrState.Paused : VtrState.Playing;
                    break;
            }
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
