namespace OpenSC.Library.BmdVideohub
{
    internal abstract class KeyValueMessageInterpreterBase : BlockMessageInterpreterBase
    {

        public override void InterpretLine(string line)
        {
            string[] parts = line.Split(": " );
            if (parts.Length != 2)
                throw new MessageInterpreterException();
            handleData(parts[0], parts[1]);
        }

        protected abstract void handleData(string key, string value);

    }
}
