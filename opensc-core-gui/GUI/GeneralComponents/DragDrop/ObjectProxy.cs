namespace OpenSC.Model
{
    public sealed class ObjectProxy
    {
        public ObjectProxy(object obj) => Object = obj;
        public object Object { get; init; }
    }
}
