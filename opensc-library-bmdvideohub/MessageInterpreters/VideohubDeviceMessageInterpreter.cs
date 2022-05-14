namespace OpenSC.Library.BmdVideohub
{
    internal abstract class VideohubDeviceMessageInterpreter : KeyValueMessageInterpreterBase
    {
        public override string Header => ProtocolStrings.BLOCK__DEVICE_INFORMATION;
    }
}
