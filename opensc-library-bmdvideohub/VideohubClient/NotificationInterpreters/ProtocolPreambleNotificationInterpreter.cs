namespace OpenSC.Library.BmdVideohub
{
    internal sealed class ProtocolPreambleNotificationInterpreter : KeyValueMessageInterpreterBase
    {
        public override string Header => ProtocolStrings.BLOCK__PROTOCOL_PREAMBLE;
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
