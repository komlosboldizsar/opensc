namespace OpenSC.Library.BmdVideohub
{
    public abstract class BlockMessageInterpreterBase : IMessageInterpreter
    {
        public abstract string Header { get; }
        private string headerDdot;
        public BlockMessageInterpreterBase() => headerDdot = Header + ":";
        public bool CanInterpret(string header) => header == headerDdot;
        public abstract void InterpretLine(string line);
        public virtual void BlockEnd() { }
    }

}
