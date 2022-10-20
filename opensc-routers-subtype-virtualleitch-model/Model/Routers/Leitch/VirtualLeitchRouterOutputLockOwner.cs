namespace OpenSC.Model.Routers.Leitch
{
    internal class VirtualLeitchRouterOutputLockOwner : RouterOutputLockOwner
    {
        public int Panel { get; init; }
        public VirtualLeitchRouterOutputLockOwner(int panel) => Panel = panel;
        public override string Owner
        {
            get
            {
                string ownerStr = $"panel with ID {Panel}";
                if (VirtualLeitchRouter.PanelIdSetting.Value == Panel)
                    ownerStr += " (this application)";
                return ownerStr;
            }
        }
    }
}
