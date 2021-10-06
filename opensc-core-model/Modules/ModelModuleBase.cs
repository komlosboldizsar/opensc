using OpenSC.Model;

namespace OpenSC.Modules
{

    public abstract class ModelModuleBase : IModule
    {

        public virtual void Initialize()
        {
            registerSerializers();
            registerSettings();
        }

        protected abstract void registerSerializers();

        protected virtual void registerSettings()
        { }

    }

}
