using OpenSC.Model.Persistence;

namespace OpenSC.Model.Mixers
{
    public class MixerTypeRegister : ModelTypeRegisterBase<Mixer>
    {
        public static ModelTypeRegisterBase<Mixer> Instance { get; } = new MixerTypeRegister();
        private MixerTypeRegister() { }
    }
}
