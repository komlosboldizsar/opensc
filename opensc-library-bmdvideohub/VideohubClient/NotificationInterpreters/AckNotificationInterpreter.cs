namespace OpenSC.Library.BmdVideohub
{
    internal sealed class AckNotificationInterpreter : IMessageInterpreter
    {
        private VideohubClient videohubClient;
        public AckNotificationInterpreter(VideohubClient videohubClient) => this.videohubClient = videohubClient;
        public bool CanInterpret(string header) => header == ProtocolStrings.ACK;
        public void InterpretLine(string line) => throw new MessageInterpreterException();
        public void BlockEnd() => videohubClient.AckLastRequest();
    }
}
