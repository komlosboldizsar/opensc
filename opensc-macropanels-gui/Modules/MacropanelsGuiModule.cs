namespace OpenSC.Modules
{

    [Module("macropanels-gui", "Macro panels (GUI)", "TODO")]
    [DependsOnModule(typeof(MacropanelsModelModule))]
    public class MacropanelsGuiModule : BasetypeGuiModuleBase<MacropanelsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            // TODO: register MacroPanelList
        }

    }

}
