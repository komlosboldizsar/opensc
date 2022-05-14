namespace OpenSC.Library.BmdVideohub
{
    public sealed class AnyMessageInterpreter : IMessageInterpreter
    {
        public bool CanInterpret(string header) => true;
        public void InterpretLine(string line) { }
        public void BlockEnd() { }
    }
}
