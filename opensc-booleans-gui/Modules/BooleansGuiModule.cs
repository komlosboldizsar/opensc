using OpenSC.GUI.Menus;
using OpenSC.GUI.Variables;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{
    [Module("booleans-gui", "Booleans (GUI)", "TODO")]
    [DependsOnModule(typeof(BooleansModelModule))]
    public class BooleansGuiModule : BasetypeGuiModuleBase<BooleansModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<BooleanList>();
        }

        protected override void registerMenus()
        {
            var variablesMenu = MenuManager.Instance.TopMenu["Variables"];
            var booleansMenu = variablesMenu["Booleans"];
            booleansMenu.ClickHandler = (menu, tag) => new BooleanList().ShowAsChild();
        }

    }

}
