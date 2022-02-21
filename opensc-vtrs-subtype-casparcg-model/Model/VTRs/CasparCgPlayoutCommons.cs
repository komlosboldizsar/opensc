using Bespoke.Osc;
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
        private OscServer oscServer = new OscServer(Bespoke.Common.Net.TransportType.Udp, IPAddress.Any, OSC_PORT);

        private void initOscServer()
        {
            if (!oscServer.IsRunning)
            {
                oscServer.Start();
                oscServer.BundleReceived += oscBundleReceived;
            }
        }

        private const string CHANNEL_MESSAGE_ADDRESS_REGEXP = @"^/channel/(?<channel>[0-9]+)/(?<subaddress>.+)";
        private const string LAYER_MESSAGE_ADDRESS_REGEXP = @"^/channel/(?<channel>[0-9]+)/stage/layer/(?<layer>[0-9]+)/(?<subaddress>.+)";
        private static readonly Regex channelMessageAddressRegexp = new Regex(CHANNEL_MESSAGE_ADDRESS_REGEXP, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex layerMessageAddressRegexp = new Regex(LAYER_MESSAGE_ADDRESS_REGEXP, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private void oscBundleReceived(object sender, OscBundleReceivedEventArgs e)
        {
            var sourceIP = e.Bundle.SourceEndPoint.Address.ToString();
            foreach (var data in e.Bundle.Data)
            {

                OscMessage message = data as OscMessage;
                if (message == null)
                    continue;

                MatchCollection layerMessageAddressRegexpMatches = layerMessageAddressRegexp.Matches(message.Address);
                if (layerMessageAddressRegexpMatches.Count == 1)
                {
                    string channel = layerMessageAddressRegexpMatches[0].Groups["channel"].Value;
                    string layer = layerMessageAddressRegexpMatches[0].Groups["layer"].Value;
                    string subscriptionsKey = string.Format("{0},{1},{2}", sourceIP, channel, layer);
                    if (!channelLayerSubscriptions.TryGetValue(subscriptionsKey, out List<CasparCgPlayout> subscriptions))
                        continue;
                    string subaddress = layerMessageAddressRegexpMatches[0].Groups["subaddress"].Value;
                    subscriptions.ForEach(instance => instance.ReceiveLayerOscMessage(message, subaddress));
                }
                else
                {
                    MatchCollection channelMessageAddressRegexpMatches = channelMessageAddressRegexp.Matches(message.Address);
                    if (channelMessageAddressRegexpMatches.Count == 1)
                    {
                        string channel = channelMessageAddressRegexpMatches[0].Groups["channel"].Value;
                        string subscriptionsKey = string.Format("{0},{1}", sourceIP, channel);
                        if (!channelSubscriptions.TryGetValue(subscriptionsKey, out List<CasparCgPlayout> subscriptions))
                            continue;
                        string subaddress = channelMessageAddressRegexpMatches[0].Groups["subaddress"].Value;
                        subscriptions.ForEach(instance => instance.ReceiveChannelOscMessage(message, subaddress));
                    }
                }

            }
        }

        private Dictionary<string, List<CasparCgPlayout>> channelLayerSubscriptions = new Dictionary<string, List<CasparCgPlayout>>();
        private Dictionary<string, List<CasparCgPlayout>> channelSubscriptions = new Dictionary<string, List<CasparCgPlayout>>();

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

        private List<CasparCgPlayout> getSubscribtionsForChannel(string ip, int channel)
        {
            if (channel < 1)
                throw new ArgumentOutOfRangeException();
            string key = string.Format("{0},{1}", ip, channel);
            if (!channelSubscriptions.TryGetValue(key, out List<CasparCgPlayout> subscriptions))
                channelSubscriptions.Add(key, new List<CasparCgPlayout>());
            return channelSubscriptions[key];
        }

        internal void SubscribeToIpChannelLayer(CasparCgPlayout instance)
        {
            List<CasparCgPlayout> layerSubscribers = getSubscribtionsForChannelLayer(instance.ListenedIp, instance.WatchedChannel, instance.WatchedLayer);
            if (!layerSubscribers.Contains(instance))
                layerSubscribers.Add(instance);
            List<CasparCgPlayout> channelSubscribers = getSubscribtionsForChannel(instance.ListenedIp, instance.WatchedChannel);
            if (!channelSubscribers.Contains(instance))
                channelSubscribers.Add(instance);
            initOscServer();
        }

        internal void UnsubscribeFromIpChannelLayer(CasparCgPlayout instance)
        {
            List<CasparCgPlayout> layerSubscribers = getSubscribtionsForChannelLayer(instance.ListenedIp, instance.WatchedChannel, instance.WatchedLayer);
            layerSubscribers.RemoveAll(i => (i == instance));
            List<CasparCgPlayout> channelSubscribers = getSubscribtionsForChannel(instance.ListenedIp, instance.WatchedChannel);
            channelSubscribers.RemoveAll(i => (i == instance));
        }
        #endregion

    }

}
