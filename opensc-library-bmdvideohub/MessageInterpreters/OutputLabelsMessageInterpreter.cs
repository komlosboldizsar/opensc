namespace OpenSC.Library.BmdVideohub
{
    internal abstract class OutputLabelsMessageInterpreter : LabelCollectionInterpreterBase
    {
        public override string Header => ProtocolStrings.BLOCK__OUTPUT_LABELS;
    }
}
