namespace OpenSC.Library.BmdVideohub
{
    internal abstract class LabelCollectionInterpreterBase : BlockMessageInterpreterBase
    {

        public override void InterpretLine(string line)
        {
            Label label = Label.FromProtocolStr(line);
            if (label == null)
                throw new MessageInterpreterException();
            handleLabel(label);
        }

        protected abstract void handleLabel(Label label);

    }
}
