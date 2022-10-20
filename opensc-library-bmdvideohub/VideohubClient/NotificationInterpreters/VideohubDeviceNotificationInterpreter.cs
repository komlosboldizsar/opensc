namespace OpenSC.Library.BmdVideohub
{
    internal sealed class VideohubDeviceNotificationInterpreter : VideohubDeviceMessageInterpreter
    {
        private VideohubClient videohubClient;
        public VideohubDeviceNotificationInterpreter(VideohubClient videohubClient) => this.videohubClient = videohubClient;
        protected override void handleData(string key, string value)
        {
            switch (key)
            {
                case ProtocolStrings.LINE__DEVICE_INFORMATION__DEVICE_PRESENT:
                    videohubClient.Connected = (value == "true");
                    break;
                case ProtocolStrings.LINE__DEVICE_INFORMATION__MODEL_NAME:
                    videohubClient.ModelName = value;
                    break;
                case ProtocolStrings.LINE__DEVICE_INFORMATION__VIDEO_INPUTS:
                    if (!int.TryParse(value, out int inputCount))
                        throw new MessageInterpreterException();
                    videohubClient.InputCount = inputCount;
                    break;
                case ProtocolStrings.LINE__DEVICE_INFORMATION__VIDEO_OUTPUTS:
                    if (!int.TryParse(value, out int outputCount))
                        throw new MessageInterpreterException();
                    videohubClient.OutputCount = outputCount;
                    break;
            }
        }
    }
}
