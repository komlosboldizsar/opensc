using OpenSC.Model.Routers;

namespace OpenSC.GUI.Routers
{
    public class RouterEditorFormTypeRegister : ModelEditorFormTypeRegister<Router>
    {
        public static ModelEditorFormTypeRegister<Router> Instance { get; } = new RouterEditorFormTypeRegister();
        private RouterEditorFormTypeRegister() { }
    }
}
