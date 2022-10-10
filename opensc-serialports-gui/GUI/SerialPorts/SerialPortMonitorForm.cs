using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.SerialPorts;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace OpenSC.GUI.SerialPorts
{

    public partial class SerialPortMonitorForm : ChildWindowWithTitle
    {

        #region Initialization
        public SerialPortMonitorForm() : base() => InitializeComponent();
        public SerialPortMonitorForm(SerialPort port) : base()
        {
            this.port = port;
            InitializeComponent();
            initDropDowns();
        }

        private SerialPort port;

        private void SerialPortMonitorForm_Load(object sender, EventArgs e)
        {
            monitoringSinceLabel.Text = $"Monitoring since {DateTime.Now:yyyy. MM. dd. HH:mm:ss}";
            if (port != null)
            {
                port.SentPacket += Port_SentPacket;
                port.ReceivedDataBytes += Port_ReceivedDataBytes;
            }
        }

        private void initDropDowns()
        {
            IComboBoxAdapterFactory enumComboBoxAdapterFactory = new EnumComboBoxAdapterFactory<DataFormat>(dataFormatEnumTranslations);
            ComboBox[] dropDowns = new[]
            { 
                bothTextBoxEncodingDropDown, sentTextBoxEncodingDropDown, receivedTextBoxEncodingDropDown,
                simulateSendingEncodingDropDown, simulateReceivingEncodingDropDown 
            };
            foreach (ComboBox dropDown in dropDowns)
            {
                dropDown.GetAdapterFromFactoryAsDataSource(enumComboBoxAdapterFactory);
                dropDown.SelectByValue(DataFormat.ASCII);
            }
        }
        #endregion

        #region Encoding/data format
        private enum DataFormat
        {
            ASCII,
            UTF8,
            Unicode,
            RawBytes
        }

        private Encoding dataFormatToEncoding(DataFormat enum1)
            => enum1 switch
            {
                DataFormat.ASCII => Encoding.ASCII,
                DataFormat.UTF8 => Encoding.UTF8,
                DataFormat.Unicode => Encoding.Unicode,
                _ => null
            };

        private Dictionary<DataFormat, string> dataFormatEnumTranslations = new()
        {
            { DataFormat.ASCII, "ASCII" },
            { DataFormat.UTF8, "UTF-8" },
            { DataFormat.Unicode, "Unicode" },
            { DataFormat.RawBytes, "Bytes (hex)" }
        };
        #endregion

        #region Serial port event handlers
        private void Port_ReceivedDataBytes(SerialPort port, byte[] data)
            => serialPortEventHandler(receivedBytes, data, showReceived);

        private void Port_SentPacket(SerialPort port, SerialPort.Packet packet)
            => serialPortEventHandler(sentBytes, packet.Data, showSent);

        private void serialPortEventHandler(List<byte> eventTotalBytes, byte[] newData, Action showDelegate)
        {
            eventTotalBytes.AddRange(newData);
            Action action = () =>
            {
                showBoth();
                showDelegate();
            };
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }
        #endregion

        #region Show serial streams in text boxes
        private void showBase(ComboBox dataFormatDropDown, TextBox textBox, List<byte> eventTotalBytes)
        {
            DataFormat? dataFormat = dataFormatDropDown.SelectedValue as DataFormat?;
            if (dataFormat == null)
            {
                textBox.Text = "";
                return;
            }
            byte[] eventTotalBytesArray = eventTotalBytes.ToArray();
            if (dataFormat == DataFormat.RawBytes)
            {
                textBox.Text = byteArrayToHexString(eventTotalBytesArray);
                return;
            }
            Encoding encoding = dataFormatToEncoding((DataFormat)dataFormat);
            textBox.Text = encoding?.GetString(eventTotalBytesArray) ?? string.Empty;
            textBox.SelectionStart = textBox.TextLength;
            textBox.ScrollToCaret();
        }

        private List<byte> bothBytes = new();
        private List<byte> receivedBytes = new();
        private List<byte> sentBytes = new();

        private void showBoth() => showBase(bothTextBoxEncodingDropDown, bothTextBox, bothBytes);
        private void showReceived() => showBase(receivedTextBoxEncodingDropDown, receivedTextBox, receivedBytes);
        private void showSent() => showBase(sentTextBoxEncodingDropDown, sentTextBox, sentBytes);
        #endregion

        #region Simulate sending/receiving
        private void sendReceiveBase(ComboBox dataFormatDropDown, TextBox inputTextBox, string processName, Action<byte[]> action)
        {
            DataFormat? dataFormat = dataFormatDropDown.SelectedValue as DataFormat?;
            string text = inputTextBox.Text;
            if (dataFormat == DataFormat.RawBytes)
            {
                try
                {
                    action(hexStringToByteArray(text));
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show($"Data for simulated {processName} invalid: {ex.Message}.", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                Encoding enc = dataFormatToEncoding((DataFormat)dataFormat);
                if (enc == null)
                {
                    MessageBox.Show($"No valid encoding selected for simulated {processName}", "Setting error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                action(enc.GetBytes(text));
            }
        }
        #endregion

        #region GUI event handlers
        private void sentTextClearButton_Click(object sender, EventArgs e)
        {
            sentBytes.Clear();
            showSent();
        }

        private void receivedTextClearButton_Click(object sender, EventArgs e)
        {
            receivedBytes.Clear();
            showReceived();
        }

        private void allTextClearButton_Click(object sender, EventArgs e)
        {
            bothBytes.Clear();
            sentBytes.Clear();
            receivedBytes.Clear();
            showBoth();
            showSent();
            showReceived();
        }

        private void sendDataButton_Click(object sender, EventArgs e)
           => sendReceiveBase(simulateSendingEncodingDropDown, simulateSendingDataTextBox, "sending",
               (bytes) => port.SendData(bytes, DateTime.Now + new TimeSpan(0, 0, 2)));

        private void receiveDataButton_Click(object sender, EventArgs e)
            => sendReceiveBase(simulateReceivingEncodingDropDown, simulateReceivingDataTextBox, "receiving",
                (bytes) => port.SimulateReceiveBytes(bytes));

        private void bothTextBoxEncodingDropDown_SelectedIndexChanged(object sender, EventArgs e)
            => showBoth();

        private void sentTextBoxEncodingDropDown_SelectedIndexChanged(object sender, EventArgs e)
            => showSent();

        private void receivedTextBoxEncodingDropDown_SelectedIndexChanged(object sender, EventArgs e)
            => showReceived();
        #endregion

        #region Bytes to hex
        private static string byteArrayToHexString(byte[] bytes, bool spaces = false)
        {
            var lookup32 = _lookupByteToHex;
            char[] result = new char[bytes.Length * (spaces ? 3 : 2)];
            for (int i = 0, t = 0; i < bytes.Length; i++)
            {
                uint charValue = lookup32[bytes[i]];
                result[t++] = (char)charValue;
                result[t++] = (char)(charValue >> 16);
                if (spaces)
                    result[t++] = ' ';
            }
            return new string(result);
        }

        private static readonly uint[] _lookupByteToHex = _createLookupByteToHex();

        private static uint[] _createLookupByteToHex()
        {
            uint[] result = new uint[256];
            for (int i = 0; i < 256; i++)
            {
                string s = i.ToString("X2");
                result[i] = ((uint)s[0]) + ((uint)s[1] << 16);
            }
            return result;
        }
        #endregion

        #region Hex to bytes
        private byte[] hexStringToByteArray(string text)
        {
            text = text.Replace(" ", "").Replace("\t", "");
            int textLength = text.Length;
            if (textLength % 2 != 0)
                throw new ArgumentException("Odd number of hex characters", nameof(text));
            int bytesLength = textLength / 2;
            byte[] byteArray = new byte[bytesLength];
            for (int b = 0, c = 0; b < byteArray.Length; b++)
            {
                uint nibbleH = _lookupHexToUint[text[c++]];
                uint nibbleL = _lookupHexToUint[text[c++]];
                if ((nibbleH == LUT_HEXTOUINT_INVALID) || (nibbleL == LUT_HEXTOUINT_INVALID))
                    throw new ArgumentException($"Invalid hex character", nameof(text));
                byteArray[b] = (byte)((nibbleH << 4) + nibbleL);
            }
            return byteArray;
        }

        private const int LUT_HEXTOUINT_INVALID = 255;
        private static readonly uint[] _lookupHexToUint = _createLookupHexToUint();

        private static uint[] _createLookupHexToUint()
        {
            uint[] lut = new uint[256];
            for (int chr = 0; chr < 256; chr++)
            {
                if ((chr >= '0') && (chr <= '9'))
                    lut[chr] = (uint)(chr - '0');
                else if ((chr >= 'a') && (chr <= 'f'))
                    lut[chr] = (uint)(chr - 'a' + 10);
                else if ((chr >= 'A') && (chr <= 'F'))
                    lut[chr] = (uint)(chr - 'A' + 10);
                else
                    lut[chr] = LUT_HEXTOUINT_INVALID;
            }
            return lut;
        }



        #endregion
    }

}
