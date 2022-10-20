namespace OpenSC.Library.BmdVideohub
{
    internal abstract class InputLabelsMessageInterpreter : LabelCollectionInterpreterBase
    {
        public override string Header => ProtocolStrings.BLOCK__INPUT_LABELS;
    }
}
