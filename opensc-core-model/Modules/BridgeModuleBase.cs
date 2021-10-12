using OpenSC.Model;

namespace OpenSC.Modules
{

    public abstract class BridgeModuleBase<TModuleFrom, TModuleTo> : IModule
    {
        public abstract void Initialize();
    }

    public abstract class BridgeModuleBase<TModuleFrom, TModuleTo1, TModuleTo2> : IModule
    {
        public abstract void Initialize();
    }

    public abstract class BridgeModuleBase<TModuleFrom, TModuleTo1, TModuleTo2, TModuleTo3> : IModule
    {
        public abstract void Initialize();
    }

}
