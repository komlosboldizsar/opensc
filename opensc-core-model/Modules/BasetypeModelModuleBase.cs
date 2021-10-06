using OpenSC.Model;

namespace OpenSC.Modules
{

    public abstract class BasetypeModuleBase : ModelModuleBase
    {

        public override void Initialize()
        {
            base.Initialize();
            registerDatabases();
        }

        protected abstract void registerDatabases();

    }

}
