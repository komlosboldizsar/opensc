using OpenSC.Model;
using OpenSC.Model.Macros;
using OpenSC.Model.Persistence;
using OpenSC.Model.Settings;

namespace OpenSC.Modules
{

    [Module("macros-model", "Macros (model)", "TODO")]
    public class MacrosModelModule : BasetypeModuleBase
    {

        public override void Initialize()
        {
            base.Initialize();
            MacroOscServer.Instance.Start();
        }

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(MacroDatabase));
        }

        protected override void registerSerializers()
        {
            SerializerRegister.RegisterSerializer(new MacroCommandWithArgumentsXmlSerializer());
            SerializerRegister.RegisterSerializer(new MacroTriggerWithArgumentsXmlSerializer());
        }

        protected override void registerSettings()
        {
            base.registerSettings();
            SettingsManager.Instance.RegisterSetting(Macro.MaxStackDepthSetting);
            SettingsManager.Instance.RegisterSetting(MacroOscServer.PortSetting);
        }

    }

}
