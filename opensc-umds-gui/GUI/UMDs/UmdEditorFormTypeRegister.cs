using OpenSC.Model.UMDs;

namespace OpenSC.GUI.UMDs
{
    public class UmdEditorFormTypeRegister : ModelEditorFormTypeRegister<Umd>
    {
        public static ModelEditorFormTypeRegister<Umd> Instance { get; } = new UmdEditorFormTypeRegister();
        private UmdEditorFormTypeRegister() { }
    }
}
