namespace OpenSC.Library.BmdVideohub
{
    internal sealed class VideoOutputRoutingNotificationInterpreter : VideoOutputRoutingMessageInterpreter
    {
        private VideohubClient videohubClient;
        public VideoOutputRoutingNotificationInterpreter(VideohubClient videohubClient) => this.videohubClient = videohubClient;
        protected override void handleCrosspoint(Crosspoint crosspoint) => videohubClient?.NotifyCrosspointChanged(crosspoint);
    }
}
