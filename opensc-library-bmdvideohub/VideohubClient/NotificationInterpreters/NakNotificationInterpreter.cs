namespace OpenSC.Library.BmdVideohub
{
    internal sealed class NakNotificationInterpreter : IMessageInterpreter
    {
        private VideohubClient videohubClient;
        public NakNotificationInterpreter(VideohubClient videohubClient) => this.videohubClient = videohubClient;
        public bool CanInterpret(string header) => header == ProtocolStrings.NAK;
        public void InterpretLine(string line) => throw new MessageInterpreterException();
        public void BlockEnd() => videohubClient.NakLastRequest();
    }
}
