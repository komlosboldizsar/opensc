namespace OpenSC.Library.BmdVideohub
{
    internal sealed class OutputLabelsNofiticationInterpreter : OutputLabelsMessageInterpreter
    {
        private VideohubClient videohubClient;
        public OutputLabelsNofiticationInterpreter(VideohubClient videohubClient) => this.videohubClient = videohubClient;
        protected override void handleLabel(Label label) => videohubClient?.NotifyOutputLabelChanged(label);
    }
}
