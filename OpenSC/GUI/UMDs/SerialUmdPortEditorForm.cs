using OpenSC.Model.UMDs;
using System;
using System.IO.Ports;

namespace OpenSC.GUI.UMDs
{

    public partial class SerialUmdPortEditorForm : UmdPortEditorFormBase
    {

        public SerialUmdPortEditorForm() : base()
        {
            InitializeComponent();
            loadAvailableSerialPorts();
        }

        public SerialUmdPortEditorForm(UmdPort port) : base(port)
        {
            InitializeComponent();
            loadAvailableSerialPorts();
            if ((port != null) && !(port is UmdSerialPort))
                throw new ArgumentException();
        }

        protected override void loadData()
        {
            base.loadData();
            UmdSerialPort umdSerialPort = port as UmdSerialPort;
            if (umdSerialPort == null)
                return;
            setSerialPortDropDown(umdSerialPort.ComPortName);
        }

        protected override void writeFields()
        {
            base.writeFields();
            UmdSerialPort umdSerialPort = port as UmdSerialPort;
            if (umdSerialPort == null)
                return;
            umdSerialPort.ComPortName = ((SerialPortDropDownItem)portNameDropDown.SelectedValue).PortName;
        }

        protected override void validateFields()
        {
            base.validateFields();
            UmdSerialPort umdSerialPort = port as UmdSerialPort;
            if (umdSerialPort == null)
                return;
            // TODO: Validation
        }

        private void loadAvailableSerialPorts()
        {
            portNameDropDown.Items.Add(new SerialPortDropDownItem("(not connected)", null));
            foreach (string portName in SerialPort.GetPortNames())
                portNameDropDown.Items.Add(new SerialPortDropDownItem(portName, portName));
        }

        private void setSerialPortDropDown(string portName)
        {
            for (int i = 0; i < portNameDropDown.Items.Count; i++)
            {
                if (((SerialPortDropDownItem)portNameDropDown.Items[i]).PortName == portName)
                {
                    portNameDropDown.SelectedIndex = i;
                    return;
                }
            }
            portNameDropDown.SelectedIndex = 0;
        }

        private class SerialPortDropDownItem
        {

            public string Label;
            public string PortName;

            public SerialPortDropDownItem(string label, string portName)
            {
                Label = label;
                PortName = portName;
            }

            public override string ToString()
            {
                return Label;
            }

        } 

    }

}
