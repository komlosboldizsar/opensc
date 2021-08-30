using OpenSC.Model.Mixers;

namespace OpenSC.GUI.Mixers
{
    public class MixerEditorFormTypeRegister : ModelEditorFormTypeRegister<Mixer>
    {
        public static ModelEditorFormTypeRegister<Mixer> Instance { get; } = new MixerEditorFormTypeRegister();
        private MixerEditorFormTypeRegister() { }
    }
}
