using OpenSC.Model.Persistence;
using OpenSC.Model.VTRs;

namespace OpenSC.Model.VTRs
{
    public class VtrTypeRegister : ModelTypeRegisterBase<Vtr>
    {
        public static ModelTypeRegisterBase<Vtr> Instance { get; } = new VtrTypeRegister();
        private VtrTypeRegister() { }
    }
}
