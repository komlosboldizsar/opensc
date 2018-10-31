using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.UMDs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
            portNameDropDown.SelectByValue(umdSerialPort.ComPortName);
        }

        protected override void writeFields()
        {
            base.writeFields();
            UmdSerialPort umdSerialPort = port as UmdSerialPort;
            if (umdSerialPort == null)
                return;
            umdSerialPort.ComPortName = portNameDropDown.SelectedValue as string;
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
            portNameDropDown.CreateAdapterAsDataSource<string>(SerialPort.GetPortNames(), pn => pn, true, "(not connected)");
        }

    }

}
