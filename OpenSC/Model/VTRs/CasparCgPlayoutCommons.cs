using Bespoke.Common.Osc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenSC.Model.VTRs
{

    class CasparCgPlayoutCommons
    {

        #region Singleton
        public static CasparCgPlayoutCommons Instance { get; } = new CasparCgPlayoutCommons();
        private CasparCgPlayoutCommons()
        {
            initOscServer();
        }
        #endregion

        #region Common OSC listener
        private const int OSC_PORT = 5253;
        private OscServer oscServer = new OscServer(TransportType.Udp, IPAddress.Any, OSC_PORT);

        private void initOscServer()
        {
            if (!oscServer.IsRunning)
            {
                oscServer.Start();
                oscServer.BundleReceived += oscBundleReceived;
            }
        }

        private const string MESSAGE_ADDRESS_REGEXP = @"^/channel/(?<channel>[0-9]+)/stage/layer/(?<layer>[0-9]+)/(?<subaddress>.+)";
        private static readonly Regex messageAddressRegexp = new Regex(MESSAGE_ADDRESS_REGEXP, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private void oscBundleReceived(object sender, OscBundleReceivedEventArgs e)
        {
            var sourceIP = e.Bundle.SourceEndPoint.Address.ToString();
            foreach (var data in e.Bundle.Data)
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
                subscriptions.ForEach(instance => instance.ReceiveOscMessage(message, subaddress));

            }
        }

        private Dictionary<string, List<CasparCgPlayout>> channelLayerSubscriptions = new Dictionary<string, List<CasparCgPlayout>>();

        private List<CasparCgPlayout> getSubscribtionsForChannelLayer(string ip, int channel, int layer)
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

        internal void SubscribeToChannelLayer(CasparCgPlayout instance)
        {
            List<CasparCgPlayout> subscribers = getSubscribtionsForChannelLayer(instance.ListenedIP, instance.WatchedChannel, instance.WatchedLayer);
            if (!subscribers.Contains(instance))
                subscribers.Add(instance);
            initOscServer();
        }

        internal void UnsubscribeFromChannelLayer(CasparCgPlayout instance)
        {
            List<CasparCgPlayout> subscribers = getSubscribtionsForChannelLayer(instance.ListenedIP, instance.WatchedChannel, instance.WatchedLayer);
            subscribers.RemoveAll(i => (i == instance));
        }
        #endregion

    }

}
