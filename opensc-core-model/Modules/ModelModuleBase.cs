using OpenSC.Model;

namespace OpenSC.Modules
{

    public abstract class ModelModuleBase : IModule
    {

        public virtual void Initialize()
        {
            registerDatabases();
        }

        protected abstract void registerDatabases();

    }

}
