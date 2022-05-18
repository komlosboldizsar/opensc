namespace OpenSC.Library.BmdVideohub
{
    internal abstract class ProtocolPreambleMessageInterpreter : KeyValueMessageInterpreterBase
    {
        public override string Header => ProtocolStrings.BLOCK__PROTOCOL_PREAMBLE;
    }
}
