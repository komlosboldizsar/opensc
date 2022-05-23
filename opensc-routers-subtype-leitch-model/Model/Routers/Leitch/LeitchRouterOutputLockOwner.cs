namespace OpenSC.Model.Routers.Leitch
{
    internal class LeitchRouterOutputLockOwner : RouterOutputLockOwner
    {
        public int Panel { get; init; }
        public LeitchRouterOutputLockOwner(int panel) => Panel = panel;
        public override string Owner
        {
            get
            {
                string ownerStr = $"panel with ID {Panel}";
                if (LeitchRouter.PanelIdSetting.Value == Panel)
                    ownerStr += " (this application)";
                return ownerStr;
            }
        }
    }
}
