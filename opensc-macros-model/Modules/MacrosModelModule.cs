using OpenSC.Model;
using OpenSC.Model.Macros;
using OpenSC.Model.Settings;

namespace OpenSC.Modules
{

    [Module("macros-model", "Macros (model)", "TODO")]
    public class MacrosModelModule : BasetypeModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(MacroDatabase));
        }

        protected override void registerSettings()
        {
            base.registerSettings();
            SettingsManager.Instance.RegisterSetting(Macro.MaxStackDepthSetting);
            SettingsManager.Instance.RegisterSetting(MacroOscServer.PortSetting);
        }

    }

}
