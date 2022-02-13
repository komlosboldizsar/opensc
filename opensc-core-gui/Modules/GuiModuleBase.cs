namespace OpenSC.Modules
{

    //[DependsOnModule(typeof(TModelModule))] - CS0416
    public abstract class GuiModuleBase<TModelModule> : IModule
    {

        public virtual void Initialize()
        {
            registerPersistableWindowTypes();
        }

        protected abstract void registerPersistableWindowTypes();

    }

}
