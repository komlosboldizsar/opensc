using OpenSC.Model.Persistence;

namespace OpenSC.Model.Streams
{
    public class StreamTypeRegister : ModelTypeRegisterBase<Stream>
    {
        public static ModelTypeRegisterBase<Stream> Instance { get; } = new StreamTypeRegister();
        private StreamTypeRegister() { }
    }
}
