using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.SerialPorts;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.SerialPorts
{

    public partial class SerialPortEditorForm : ModelEditorFormBase, IModelEditorForm<SerialPort>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as SerialPort);
        public IModelEditorForm<SerialPort> GetInstanceT(SerialPort modelInstance) => new SerialPortEditorForm(modelInstance);

        public SerialPortEditorForm() : base() => InitializeComponent();

        public SerialPortEditorForm(SerialPort port) : base(port)
        {
            InitializeComponent();
            initDropDowns();
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<SerialPort, SerialPort>(this, SerialPortDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            SerialPort port = (SerialPort)EditedModel;
            if (port == null)
                return;
            portNameDropDown.SelectByValue(port.ComPortName);
            baudRateNumericField.Value = port.BaudRate;
            dataBitsNumericField.Value = port.DataBits;
            stopBitsDropDown.SelectByValue(port.StopBits);
            parityDropDowm.SelectByValue(port.Parity);
        }

        protected override void validateFields()
        {
            base.validateFields();
            SerialPort port = (SerialPort)EditedModel;
            if (port == null)
                return;
        }

        protected override void writeFields()
        {
            base.writeFields();
            SerialPort port = (SerialPort)EditedModel;
            if (port == null)
                return;
            port.ComPortName = portNameDropDown.SelectedValue as string;
            port.BaudRate = (int)baudRateNumericField.Value;
            port.DataBits = (int)dataBitsNumericField.Value ;
            port.StopBits = (System.IO.Ports.StopBits)stopBitsDropDown.SelectedValue;
            port.Parity = (System.IO.Ports.Parity)parityDropDowm.SelectedValue;
        }

        private void initDropDowns()
        {
            string[] availableSerialPorts = System.IO.Ports.SerialPort.GetPortNames();
            portNameDropDown.CreateAdapterAsDataSource(availableSerialPorts, null, true, "(not connected)");
            stopBitsDropDown.SetAdapterAsDataSource(new EnumComboBoxAdapter<System.IO.Ports.StopBits>());
            parityDropDowm.SetAdapterAsDataSource(new EnumComboBoxAdapter<System.IO.Ports.Parity>());
        }

    }

}
