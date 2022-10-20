using OpenSC.Model.Persistence;

namespace OpenSC.Model.UMDs
{
    public class UmdTypeRegister : ModelTypeRegisterBase<Umd>
    {
        public static ModelTypeRegisterBase<Umd> Instance { get; } = new UmdTypeRegister();
        private UmdTypeRegister() { }
    }
}
