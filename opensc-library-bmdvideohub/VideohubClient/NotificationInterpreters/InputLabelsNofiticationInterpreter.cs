namespace OpenSC.Library.BmdVideohub
{
    internal sealed class InputLabelsNofiticationInterpreter : InputLabelsMessageInterpreter
    {
        private VideohubClient videohubClient;
        public InputLabelsNofiticationInterpreter(VideohubClient videohubClient) => this.videohubClient = videohubClient;
        protected override void handleLabel(Label label) => videohubClient?.NotifyInputLabelChanged(label);
    }
}
