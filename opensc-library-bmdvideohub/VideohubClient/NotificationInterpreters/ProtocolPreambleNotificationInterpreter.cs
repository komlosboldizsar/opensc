namespace OpenSC.Library.BmdVideohub
{
    internal sealed class ProtocolPreambleNotificationInterpreter : VideohubDeviceMessageInterpreter
    {
        private VideohubClient videohubClient;
        public ProtocolPreambleNotificationInterpreter(VideohubClient videohubClient) => this.videohubClient = videohubClient;
        protected override void handleData(string key, string value)
        {
            switch (key)
            {
                case ProtocolStrings.LINE__PROTOCOL_PREAMBLE__VERSION:
                    break;
            }
        }
    }
}
