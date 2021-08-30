using OpenSC.GUI.Menus;
using OpenSC.GUI.Variables;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("dynamictexts-gui", "Dynamic texts (GUI)", "TODO")]
    [DependsOnModule(typeof(DynamictextsModelModule))]
    public class DynamictextsGuiModule : BasetypeGuiModuleBase<DynamictextsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<DynamicTextList>();
        }

        protected override void registerMenus()
        {
            var variablesMenu = MenuManager.Instance.TopMenu["Variables"];
            var dynamicTextsMenu = variablesMenu["Dynamic texts"];
            dynamicTextsMenu.ClickHandler = (menu, tag) => new DynamicTextList().ShowAsChild();
        }

    }

}
