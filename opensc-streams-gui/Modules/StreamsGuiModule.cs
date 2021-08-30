using OpenSC.GUI.Menus;
using OpenSC.GUI.Streams;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("streams-gui", "Streams (GUI)", "TODO")]
    [DependsOnModule(typeof(StreamsModelModule))]
    public class StreamsGuiModule : BasetypeGuiModuleBase<StreamsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<StreamList>();
        }

        protected override void registerMenus()
        {
            var streamsMenu = MenuManager.Instance.TopMenu["Streams"]["Stream list"];
            streamsMenu.ClickHandler = (menu, tag) => new StreamList().ShowAsChild();
        }

    }

}
