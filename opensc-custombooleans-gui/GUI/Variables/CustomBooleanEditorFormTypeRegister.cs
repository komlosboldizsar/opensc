using OpenSC.Model.Variables;

namespace OpenSC.GUI.Variables
{
    public class CustomBooleanEditorFormTypeRegister : ModelEditorFormTypeRegister<CustomBoolean>
    {
        public static ModelEditorFormTypeRegister<CustomBoolean> Instance { get; } = new CustomBooleanEditorFormTypeRegister();
        private CustomBooleanEditorFormTypeRegister() { }
    }
}
