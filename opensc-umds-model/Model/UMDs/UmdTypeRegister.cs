using OpenSC.Model.Persistence;

namespace OpenSC.Model.UMDs
{
    public class UmdTypeRegister : ModelTypeRegisterBase<UMD>
    {
        public static ModelTypeRegisterBase<UMD> Instance { get; } = new UmdTypeRegister();
        private UmdTypeRegister() { }
    }
}
