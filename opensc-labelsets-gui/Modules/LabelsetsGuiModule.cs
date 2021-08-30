using OpenSC.GUI.Menus;
using OpenSC.GUI.Routers;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("labelsets-gui", "Labelsets (GUI)", "TODO")]
    [DependsOnModule(typeof(LabelsetsModelModule))]
    public class LabelsetsGuiModule : BasetypeGuiModuleBase<LabelsetsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<LabelsetList>();
        }

        protected override void registerMenus()
        {
            var routersMenu = MenuManager.Instance.TopMenu["Routers"];
            var labelsetsListMenu = routersMenu["Labelsets list"];
            labelsetsListMenu.ClickHandler = (menu, tag) => new LabelsetList().ShowAsChild();
        }

    }

}
