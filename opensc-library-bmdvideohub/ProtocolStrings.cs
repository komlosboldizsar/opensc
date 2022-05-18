namespace OpenSC
{
    internal class ProtocolStrings
    {

        #region ACK/NAK
        public const string ACK = "ACK";
        public const string NAK = "NAK";
        #endregion

        #region Block: Ping
        public const string BLOCK__PING = "PING";
        #endregion

        #region Block: Protocol preamble
        public const string BLOCK__PROTOCOL_PREAMBLE = "PROTOCOL PREAMBLE";
        public const string LINE__PROTOCOL_PREAMBLE__VERSION = "Version";
        #endregion

        #region Block: Device information
        public const string BLOCK__DEVICE_INFORMATION = "VIDEOHUB DEVICE";
        public const string LINE__DEVICE_INFORMATION__DEVICE_PRESENT = "Device present";
        public const string LINE__DEVICE_INFORMATION__MODEL_NAME = "Model name";
        public const string LINE__DEVICE_INFORMATION__VIDEO_INPUTS = "Video inputs";
        public const string LINE__DEVICE_INFORMATION__PROCESSING_UNITS = "Video processing units";
        public const string LINE__DEVICE_INFORMATION__VIDEO_OUTPUTS = "Video outputs";
        public const string LINE__DEVICE_INFORMATION__VIDEO_MONITORING_OUTPUTS = "Video monitoring outputs";
        public const string LINE__DEVICE_INFORMATION__SERIAL_PORTS = "Serial ports";
        #endregion

        #region Block: Video output routing
        public const string BLOCK__VIDEO_OUTPUT_ROUTING = "VIDEO OUTPUT ROUTING";
        #endregion

        #region Block: Video output locks
        public const string BLOCK__VIDEO_OUTPUT_LOCKS = "VIDEO OUTPUT LOCKS";
        #endregion

    }
}
