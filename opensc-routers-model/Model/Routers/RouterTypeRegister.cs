using OpenSC.Model.Persistence;

namespace OpenSC.Model.Routers
{
    public class RouterTypeRegister : ModelTypeRegisterBase<Router>
    {
        public static ModelTypeRegisterBase<Router> Instance { get; } = new RouterTypeRegister();
        private RouterTypeRegister() { }
    }
}
