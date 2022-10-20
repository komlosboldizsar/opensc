namespace OpenSC.Library.BmdVideohub
{
    internal interface IMessageInterpreter
    {
        public bool CanInterpret(string header);
        public void InterpretLine(string line);
        public void BlockEnd();
    }
}
