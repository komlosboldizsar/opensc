namespace OpenSC.Modules
{

    [Module("mixers-subtype-bmd-gui", "Mixers / BlackMagic Design ATEM (GUI)", "TODO")]
    [DependsOnModule(typeof(MixersSubtypeBmdModelModule))]
    public class BooleantalliesGuiModule : SubtypeGuiModuleBase<MixersSubtypeBmdModelModule>
    {

        protected override void registerPersistableWindowTypes()
        { }

        protected override void registerSubtypeEditorWindowTypes()
        {
            // TODO: register BmdMixerEditorForm for BmdMixer
        }

    }

}
