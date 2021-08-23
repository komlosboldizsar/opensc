using OpenSC.GUI.Menus;
using OpenSC.GUI.Variables;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("dynamictexts-gui", "Dynamictexts GUI", "TODO")]
    [DependsOnModule(typeof(DynamictextsModelModule))]
    public class DynamictextsGuiModule : IModule
    {

        public void Initialize()
        {
            registerWindowTypes();
            registerMenus();
        }

        private void registerWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType(typeof(DynamicTextList));
        }

        private void registerMenus()
        {
            var variablesMenu = MenuManager.Instance.TopMenu["Variables"];
            var dynamicTextsMenu = variablesMenu["Dynamic texts"];
            dynamicTextsMenu.ClickHandler = (menu, tag) => new DynamicTextList().ShowAsChild();
        }

    }

}
