using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.Leitch;
using OpenSC.Model.Settings;

namespace OpenSC.Modules
{

    [Module("routers-subtype-virtualleitch-model", "Routers / Virtual Leitch (model)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutersSubtypeVirtualleitchModelModule : SubtypeModelModuleBase<RoutersModelModule>
    {

        protected override void registerModelTypes()
        {
            RouterTypeRegister.Instance.RegisterType<VirtualLeitchRouter>();
        }

        protected override void registerSerializers()
        {
            SerializerRegister.RegisterCompleteSerializer(new VirtualLeitchRouterOutputXmlSerializer());
        }

        protected override void registerSettings()
        {
            base.registerSettings();
            SettingsManager.Instance.RegisterSetting(VirtualLeitchRouter.PanelIdSetting);
        }

    }

}
