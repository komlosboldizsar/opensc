using OpenSC.GUI.Helpers.Converters;

namespace OpenSC.GUI.SerialPorts
{
    internal class SerialPortSettingTranslators
    {

        public static readonly EnumToStringConverter<System.IO.Ports.Parity> PARITY = new() {
            { System.IO.Ports.Parity.None, "none" },
            { System.IO.Ports.Parity.Even, "even" },
            { System.IO.Ports.Parity.Odd, "odd" },
            { System.IO.Ports.Parity.Mark, "mark" },
            { System.IO.Ports.Parity.Space, "space" }
        };

        public static readonly EnumToStringConverter<System.IO.Ports.StopBits> STOPBITS = new() {
            { System.IO.Ports.StopBits.None, "none" },
            { System.IO.Ports.StopBits.One, "1" },
            { System.IO.Ports.StopBits.OnePointFive, "1.5" },
            { System.IO.Ports.StopBits.Two, "2" },
        };

    }
}
