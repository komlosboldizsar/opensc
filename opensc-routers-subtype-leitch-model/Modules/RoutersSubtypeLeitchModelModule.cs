using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.Leitch;
using OpenSC.Model.Settings;

namespace OpenSC.Modules
{

    [Module("routers-subtype-letich-model", "Routers / Leitch (model)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutersSubtypeLeitchModelModule : SubtypeModelModuleBase<RoutersModelModule>
    {

        protected override void registerModelTypes()
        {
            RouterTypeRegister.Instance.RegisterType<LeitchRouter>();
        }

        protected override void registerSerializers()
        {
            SerializerRegister.RegisterSerializer(new LeitchRouterOutputXmlSerializer());
        }

        protected override void registerSettings()
        {
            base.registerSettings();
            SettingsManager.Instance.RegisterSetting(LeitchRouter.PanelIdSetting);
        }

    }

}
