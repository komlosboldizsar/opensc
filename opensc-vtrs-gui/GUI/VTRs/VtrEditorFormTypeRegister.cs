using OpenSC.Model.VTRs;

namespace OpenSC.GUI.VTRs
{
    public class VtrEditorFormTypeRegister: ModelEditorFormTypeRegister<Vtr>
    {
        public static VtrEditorFormTypeRegister Instance { get; } = new VtrEditorFormTypeRegister();
    }
}
