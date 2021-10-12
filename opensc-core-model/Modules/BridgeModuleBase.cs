using OpenSC.Model;

namespace OpenSC.Modules
{

    public abstract class BridgeModuleBase<TModuleFrom, TModuleTo> : IModule
    {
        public abstract void Initialize();
    }

    public abstract class BridgeModuleBase<TModuleFrom1, TModuleFrom2, TModuleTo> : IModule
    {
        public abstract void Initialize();
    }

    public abstract class BridgeModuleBase<TModuleFrom1, TModuleFrom2, TModuleFrom3, TModuleTo> : IModule
    {
        public abstract void Initialize();
    }

}
