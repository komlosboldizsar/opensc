namespace OpenSC.Library.BmdVideohub
{
    internal sealed class VideoOutputLocksStateNotificationInterpreter : VideoOutputLocksStateMessageInterpreter
    {
        private VideohubClient videohubClient;
        public VideoOutputLocksStateNotificationInterpreter(VideohubClient videohubClient) => this.videohubClient = videohubClient;
        protected override void handleLockState(LockStateData lockStateData) => videohubClient?.NotifyLockStateChanged(lockStateData);
    }
}
