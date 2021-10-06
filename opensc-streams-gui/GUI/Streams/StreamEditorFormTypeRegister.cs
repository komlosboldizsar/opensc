using OpenSC.Model.Streams;

namespace OpenSC.GUI.Streams
{
    public class StreamEditorFormTypeRegister : ModelEditorFormTypeRegister<Stream>
    {
        public static ModelEditorFormTypeRegister<Stream> Instance { get; } = new StreamEditorFormTypeRegister();
        private StreamEditorFormTypeRegister() { }
    }
}
