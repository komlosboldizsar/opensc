namespace OpenSC.Modules
{

    public abstract class SubtypeGuiModuleBase<TModelModule> : GuiModuleBase<TModelModule>
    {

        public override void Initialize()
        {
            base.Initialize();
            registerSubtypeEditorWindowTypes();
        }

        protected abstract void registerSubtypeEditorWindowTypes();

    }

}
