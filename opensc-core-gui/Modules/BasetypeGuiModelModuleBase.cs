namespace OpenSC.Modules
{

    public abstract class BasetypeGuiModuleBase<TModelModule> : GuiModuleBase<TModelModule>
    {

        public override void Initialize()
        {
            base.Initialize();
            registerMenus();
        }

        protected abstract void registerMenus();

    }

}
