using OpenSC.Model.VTRs;

namespace OpenSC.GUI.VTRs
{
    public class VtrEditorFormTypeRegister : ModelEditorFormTypeRegister<Vtr>
    {
        public static ModelEditorFormTypeRegister<Vtr> Instance { get; } = new VtrEditorFormTypeRegister();
        private VtrEditorFormTypeRegister() { }
    }
}
