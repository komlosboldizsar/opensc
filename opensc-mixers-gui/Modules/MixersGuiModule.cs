namespace OpenSC.Modules
{

    [Module("mixers-gui", "Mixers (GUI)", "TODO")]
    [DependsOnModule(typeof(MixersModelModule))]
    public class MixersGuiModule : BasetypeGuiModuleBase<MixersModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            // TODO: register MixerList
        }

    }

}
