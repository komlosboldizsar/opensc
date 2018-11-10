using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.SerialPorts;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.SerialPorts
{

    public partial class SerialPortEditorForm : ModelEditorFormBase
    {

        private const string TITLE_NEW = "New serial port";
        private const string TITLE_EDIT = "Edit serial port: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New serial port";
        private const string HEADER_TEXT_EDIT = "Edit serial port";

        protected SerialPort port;

        bool addingNew = false;

        protected bool AddingNew
        {
            get { return addingNew; }
            set
            {
                addingNew = value;
                Text = string.Format((value ? TITLE_NEW : TITLE_EDIT), port?.ID, port?.Name);
                HeaderText = string.Format((value ? HEADER_TEXT_NEW : HEADER_TEXT_EDIT), port?.ID, port?.Name);
            }
        }

        public SerialPortEditorForm()
        {
            InitializeComponent();
        }

        public SerialPortEditorForm(SerialPort port)
        {
            InitializeComponent();
            initDropDowns();
            AddingNew = (port == null);
            this.port = (port == null) ? new SerialPort() : port;
        }

        protected override void loadData()
        {

            if (port == null)
                return;

            idNumericField.Value = (AddingNew ? SerialPortDatabase.Instance.NextValidId() : port.ID);
            nameTextBox.Text = port.Name;

            portNameDropDown.SelectByValue(port.ComPortName);
            baudRateNumericField.Value = port.BaudRate;
            dataBitsNumericField.Value = port.DataBits;
            stopBitsDropDown.SelectByValue(port.StopBits);
            parityDropDowm.SelectByValue(port.Parity);

        }

        protected sealed override bool saveData()
        {

            try
            {
                validateFields();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Data validation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            port.StartUpdate();
            writeFields();
            port.EndUpdate();

            if (addingNew)
                SerialPortDatabase.Instance.Add(port);
            AddingNew = false;

            return true;

        }

        protected void validateFields()
        {

            if (port == null)
                return;

            port.ValidateId((int)idNumericField.Value);
            port.ValidateName(nameTextBox.Text);

        }

        protected void writeFields()
        {

            if (port == null)
                return;

            port.ID = (int)idNumericField.Value;
            port.Name = nameTextBox.Text;

            port.ComPortName = portNameDropDown.SelectedValue as string;
            port.BaudRate = (int)baudRateNumericField.Value;
            port.DataBits = (int)dataBitsNumericField.Value ;
            port.StopBits = (System.IO.Ports.StopBits)stopBitsDropDown.SelectedValue;
            port.Parity = (System.IO.Ports.Parity)parityDropDowm.SelectedValue;

        }

        private void initDropDowns()
        {

            string[] availableSerialPorts = System.IO.Ports.SerialPort.GetPortNames();
            portNameDropDown.CreateAdapterAsDataSource(availableSerialPorts, port => port, true, "(not connected)");

            IComboBoxAdapter stopBitsDropDownEnumAdapter = new EnumComboBoxAdapter<System.IO.Ports.StopBits>();
            stopBitsDropDown.SetAdapterAsDataSource(stopBitsDropDownEnumAdapter);

            IComboBoxAdapter parityDropDownEnumAdapter = new EnumComboBoxAdapter<System.IO.Ports.Parity>();
            parityDropDowm.SetAdapterAsDataSource(parityDropDownEnumAdapter);

        }

    }

}
