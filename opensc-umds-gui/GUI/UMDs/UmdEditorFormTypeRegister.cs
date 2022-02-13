using OpenSC.Model.UMDs;

namespace OpenSC.GUI.UMDs
{
    public class UmdEditorFormTypeRegister : ModelEditorFormTypeRegister<UMD>
    {
        public static ModelEditorFormTypeRegister<UMD> Instance { get; } = new UmdEditorFormTypeRegister();
        private UmdEditorFormTypeRegister() { }
    }
}
