﻿using OpenSC.Extensions;
using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.SerialPorts;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            HeaderText = $"Serial port monitor: {port}";
            subscribePortStatusSettingEvents();
            showPortStatusAndSettings();
            monitoringStatus = true;
        }

        private void initDropDowns()
        {
            IComboBoxAdapterFactory dataFormatComboBoxAdapterFactor = new EnumComboBoxAdapterFactory<DataFormat>(dataFormatEnumTranslations);
            ComboBox[] dataFormatDropDowns = new[]
            { 
                bothTextBoxDataFormatDropDown, sentTextBoxDataFormatDropDown, receivedTextBoxDataFormatDropDown,
                simulateSendingDataFormatDropDown, simulateReceivingDataFormatDropDown 
            };
            foreach (ComboBox dataFormatDropDown in dataFormatDropDowns)
            {
                dataFormatDropDown.GetAdapterFromFactoryAsDataSource(dataFormatComboBoxAdapterFactor);
                dataFormatDropDown.SelectByValue(DataFormat.ASCII);
            }
            IComboBoxAdapterFactory packetEndingComboBoxAdapterFactor = new EnumComboBoxAdapterFactory<PacketEnding>(packetEndingEnumTranslations);
            ComboBox[] packetEndingDropDowns = new[]
            {
                simulateSendingPacketEndingDropDown, simulateReceivingPacketEndingDropDown
            };
            foreach (ComboBox packetEndingDropDown in packetEndingDropDowns)
            {
                packetEndingDropDown.GetAdapterFromFactoryAsDataSource(packetEndingComboBoxAdapterFactor);
                packetEndingDropDown.SelectByValue(PacketEnding.None);
            }
        }
        #endregion

        #region On/off monitoring
        private bool _monitoringOn;
        private DateTime _monitoringSince;

        private bool monitoringStatus
        {
            get => _monitoringOn;
            set
            {
                if (_monitoringOn == value)
                    return;
                _monitoringOn = value;
                if (value)
                {
                    _monitoringSince = DateTime.Now;
                    if (sentTextClearOnMonitoringStartCheckBox.Checked)
                        clearSent();
                    if (receivedTextClearOnMonitoringStartCheckBox.Checked)
                        clearReceived();
                    if (bothTextClearOnMonitoringStartCheckBox.Checked)
                        clearBoth();
                    if (port != null)
                    {
                        port.SentPacket += Port_SentPacket;
                        port.ReceivedDataBytes += Port_ReceivedDataBytes;
                    }
                }
                else
                {
                    if (port != null)
                    {
                        port.SentPacket -= Port_SentPacket;
                        port.ReceivedDataBytes -= Port_ReceivedDataBytes;
                    }
                }
                showMonitoringStatus();
            }
        }

        private void showMonitoringStatus()
        {
            if (monitoringStatus)
            {
                monitoringStatusValueLabel.Text = $"on, since {_monitoringSince:yyyy. MM. dd. HH:mm:ss}";
                monitoringStatusValueLabel.ForeColor = COLOR_ON;
                startStopMonitoringButton.Text = "Stop monitoring";
            }
            else
            {
                monitoringStatusValueLabel.Text = "off";
                monitoringStatusValueLabel.ForeColor = COLOR_OFF;
                startStopMonitoringButton.Text = "Start monitoring";
            }
        }
        #endregion

        #region Serial port settings/status
        private void subscribePortStatusSettingEvents()
        {
            if (port == null)
                return;
            port.InitializedChanged += Port_InitializedChanged;
            port.ComPortNameChanged += Port_ComPortNameChanged;
            port.BaudRateChanged += Port_BaudRateChanged;
            port.DataBitsChanged += Port_DataBitsChanged;
            port.StopBitsChanged += Port_StopBitsChanged;
            port.ParityChanged += Port_ParityChanged;
        }

        private void showPortStatusAndSettings()
        {
            if (port == null)
            {
                portStatusValueLabel.Text = "-";
                portStatusValueLabel.ForeColor = COLOR_UNKNOWN;
                portInitDeinitButton.Text = "-";
                portNameValueLabel.Text = "-";
                portBaudrateValueLabel.Text = "-";
                portDatabitsValueLabel.Text = "-";
                portStopbitsValueLabel.Text = "-";
                portParityValueLabel.Text = "-";
                return;
            }
            showPortStatus();
            showPortName();
            showPortBaudrate();
            showPortDatabits();
            showPortStopbits();
            showPortParity();
        }

        private void showPortStatus()
        {
            portStatusValueLabel.Text = port.Initialized ? "initialized" : "deinitialized";
            portStatusValueLabel.ForeColor = port.Initialized ? COLOR_ON : COLOR_OFF;
            portInitDeinitButton.Text = port.Initialized ? "Deinitialize port" : "Initialize port";
        }

        private void Port_InitializedChanged(SerialPort item, bool oldValue, bool newValue)
        {
            showPortStatus();
            if (newValue)
            {
                if (sentTextClearOnPortInitCheckBox.Checked)
                    clearSent();
                if (receivedTextClearOnPortInitCheckBox.Checked)
                    clearReceived();
                if (bothTextClearOnPortInitCheckBox.Checked)
                    clearBoth();
            }
        }

        private void showPortName()
            => portNameValueLabel.Text = port.Name;

        private void Port_ComPortNameChanged(SerialPort item, string oldValue, string newValue)
            => showPortName();

        private void showPortBaudrate()
            => portBaudrateValueLabel.Text = port.BaudRate.ToString();

        private void Port_BaudRateChanged(SerialPort item, int oldValue, int newValue)
            => showPortBaudrate();

        private void showPortDatabits()
            => portDatabitsValueLabel.Text = port.DataBits.ToString();

        private void Port_DataBitsChanged(SerialPort item, int oldValue, int newValue)
            => showPortDatabits();

        private void showPortStopbits()
            => portStopbitsValueLabel.Text = SerialPortSettingTranslators.STOPBITS.Convert(port.StopBits);

        private void Port_StopBitsChanged(SerialPort item, System.IO.Ports.StopBits oldValue, System.IO.Ports.StopBits newValue)
            => showPortStopbits();

        private void showPortParity()
            => portParityValueLabel.Text = SerialPortSettingTranslators.PARITY.Convert(port.Parity);

        private void Port_ParityChanged(SerialPort item, System.IO.Ports.Parity oldValue, System.IO.Ports.Parity newValue)
            => showPortParity();
        #endregion

        #region Encoding/data format
        private enum DataFormat
        {
            ASCII,
            UTF8,
            Unicode,
            RawBytes
        }

        private Encoding dataFormatToEncoding(DataFormat dataFormat)
            => dataFormat switch
            {
                DataFormat.ASCII => Encoding.ASCII,
                DataFormat.UTF8 => Encoding.UTF8,
                DataFormat.Unicode => Encoding.Unicode,
                _ => null
            };

        private readonly Dictionary<DataFormat, string> dataFormatEnumTranslations = new()
        {
            { DataFormat.ASCII, "ASCII" },
            { DataFormat.UTF8, "UTF-8" },
            { DataFormat.Unicode, "Unicode" },
            { DataFormat.RawBytes, "Bytes (hex)" }
        };
        #endregion

        #region Packet ending
        private enum PacketEnding
        {
            None,
            CR,
            LF,
            CRLF
        }

        private string packetEndingToString(PacketEnding packetEnding)
            => packetEnding switch
            {
                PacketEnding.CR => "\r",
                PacketEnding.LF => "\n",
                PacketEnding.CRLF => "\r\n",
                _ => ""
            };

        private readonly Dictionary<PacketEnding, string> packetEndingEnumTranslations = new()
        {
            { PacketEnding.None, "none" },
            { PacketEnding.CR, "CR (0x13)" },
            { PacketEnding.LF, "LF (0x10)" },
            { PacketEnding.CRLF, "CR (0x13) + LF (0x10)" }
        };
        #endregion

        #region Serial port send/receive event handlers
        private void Port_ReceivedDataBytes(SerialPort port, byte[] data)
            => serialPortEventHandler(receivedBytes, data, showReceived);

        private void Port_SentPacket(SerialPort port, SerialPort.Packet packet)
            => serialPortEventHandler(sentBytes, packet.Data, showSent);

        private void serialPortEventHandler(List<byte> eventTotalBytes, byte[] newData, Action showDelegate)
        {
            if (!monitoringStatus)
                return;
            bothBytes.AddRange(newData);
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
        private void showSent() => showBase(sentTextBoxDataFormatDropDown, sentTextBox, sentTextAutoScrollCheckBox, sentBytes);
        private void showReceived() => showBase(receivedTextBoxDataFormatDropDown, receivedTextBox, receivedTextAutoScrollCheckBox, receivedBytes);
        private void showBoth() => showBase(bothTextBoxDataFormatDropDown, bothTextBox, bothTextAutoScrollCheckBox, bothBytes);
        
        private void showBase(ComboBox dataFormatDropDown, TextBox textBox, CheckBox autoScrollCheckBox, List<byte> eventTotalBytes)
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
                textBox.Text = eventTotalBytesArray.ToHexString(true);
                autoScroll(textBox, autoScrollCheckBox);
                return;
            }
            Encoding encoding = dataFormatToEncoding((DataFormat)dataFormat);
            textBox.Text = encoding?.GetString(eventTotalBytesArray) ?? string.Empty;
            autoScroll(textBox, autoScrollCheckBox);
        }

        private void autoScroll(TextBox textBox, CheckBox checkBox)
        {
            if (checkBox.Checked)
                scrollToEnd(textBox);
        }

        private void scrollToEnd(TextBox textBox)
        {
            textBox.SelectionStart = textBox.TextLength;
            textBox.ScrollToCaret();
        }

        private void clearSent()
        {
            sentBytes.Clear();
            showSent();
        }

        private void clearReceived()
        {
            receivedBytes.Clear();
            showReceived();
        }

        private void clearBoth()
        {
            bothBytes.Clear();
            showBoth();
        }

        private readonly List<byte> sentBytes = new();
        private readonly List<byte> receivedBytes = new();
        private readonly List<byte> bothBytes = new();
        #endregion

        #region Simulate sending/receiving
        private void sendReceiveBase(ComboBox dataFormatDropDown, ComboBox packetEndingDropDown, TextBox inputTextBox, string processName, Action<byte[]> action)
        {
            DataFormat? dataFormat = dataFormatDropDown.SelectedValue as DataFormat?;
            string text = inputTextBox.Text;
            if (dataFormat == DataFormat.RawBytes)
            {
                try
                {
                    action(text.HexToBytes());
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
                string packetEnding = packetEndingToString((PacketEnding)packetEndingDropDown.SelectedValue);
                action(enc.GetBytes(text + packetEnding));
            }
        }
        #endregion

        #region GUI event handlers
        private void sentTextClearButton_Click(object sender, EventArgs e)
            => clearSent();

        private void receivedTextClearButton_Click(object sender, EventArgs e)
            => clearReceived();

        private void bothTextClearButton_Click(object sender, EventArgs e)
            => clearBoth();

        private void sentTextBoxDataFormatDropDown_SelectedIndexChanged(object sender, EventArgs e)
            => showSent();

        private void receivedTextBoxDataFormatDropDown_SelectedIndexChanged(object sender, EventArgs e)
            => showReceived();

        private void bothTextBoxDataFormatDropDown_SelectedIndexChanged(object sender, EventArgs e)
            => showBoth();

        private void simulateSendingDataFormatDropDown_SelectedIndexChanged(object sender, EventArgs e)
            => enablePacketEndingDropDownByDataFormatDropDown(simulateSendingPacketEndingDropDown, simulateSendingDataFormatDropDown);

        private void simulateReceivingDataFormatDropDown_SelectedIndexChanged(object sender, EventArgs e)
            => enablePacketEndingDropDownByDataFormatDropDown(simulateReceivingPacketEndingDropDown, simulateReceivingDataFormatDropDown);

        private void enablePacketEndingDropDownByDataFormatDropDown(ComboBox packetEndingDropDown, ComboBox dataFormatDropDown)
            => packetEndingDropDown.Enabled = !DataFormat.RawBytes.Equals(dataFormatDropDown.SelectedValue);

        private void sendDataButton_Click(object sender, EventArgs e)
           => sendReceiveBase(simulateSendingDataFormatDropDown, simulateSendingPacketEndingDropDown, simulateSendingDataTextBox, "sending",
               (bytes) => port.SendData(bytes, DateTime.Now + new TimeSpan(0, 0, 2)));

        private void receiveDataButton_Click(object sender, EventArgs e)
            => sendReceiveBase(simulateReceivingDataFormatDropDown, simulateReceivingPacketEndingDropDown, simulateReceivingDataTextBox, "receiving",
                (bytes) => port.SimulateReceiveBytes(bytes));

        private void initDeinitPortButton_Click(object sender, EventArgs e)
        {
            if (port == null)
                return;
            if (port.Initialized)
                port.DeInit();
            else
                port.Init();
        }

        private void startStopMonitoringButton_Click(object sender, EventArgs e)
            => monitoringStatus = !monitoringStatus;
        #endregion

        #region Color constants
        private static readonly Color COLOR_OFF = Color.DarkRed;
        private static readonly Color COLOR_ON = Color.DarkGreen;
        private static readonly Color COLOR_UNKNOWN = SystemColors.ControlText;
        #endregion

    }

}
