using OpenSC.Model;

namespace OpenSC.Modules
{
    public abstract class BridgeModuleBase<TModuleFrom, TModuleTo> : IModule
    {
        public abstract void Initialize();
    }
}
