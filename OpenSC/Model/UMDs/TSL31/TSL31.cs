using OpenSC.Model.Persistence;
using OpenSC.Model.SerialPorts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.TSL31
{
    [TypeLabel("TSL 3.1")]
    [TypeCode("tsl31")]
    class TSL31 : UMD
    {

        public override IUMDType Type => new TSL31Type();

        #region Property: Port
        public event PropertyChangedTwoValuesDelegate<TSL31, SerialPort> PortChanged;

        [PersistAs("port")]
        private SerialPort port;

#pragma warning disable CS0169
        [TempForeignKey(SerialPortDatabase.DBNAME, nameof(port))]
        private int _portId;
#pragma warning restore CS0169

        public SerialPort Port
        {
            get => port;
            set => setProperty(this, ref port, value, PortChanged);
        }
        #endregion

        #region Property: Address
        public event PropertyChangedTwoValuesDelegate<TSL31, int> AddressChanged;

        [PersistAs("address")]
        private int address = 1;

        public int Address
        {
            get => address;
            set => setProperty(this, ref address, value, AddressChanged, validator: ValidateAddress);
        }

        public void ValidateAddress(int address)
        {
            if ((address < 0) || (address > 126))
                throw new ArgumentOutOfRangeException();
        }
        #endregion

        public override Color[] TallyColors
        {
            get { return new Color[] { Color.Red, Color.Green }; }
        }
        protected override void tallyChanged(int index, bool state)
        {
            update();
        }

        protected override void update()
        {
            if (port == null)
                return;
            DateTime packetValidUntil = DateTime.Now + TimeSpan.FromSeconds(5);
            port.SendData(getBytesToSend(), packetValidUntil);
        }

        protected virtual byte[] getBytesToSend()
        {
            byte[] bytes = new byte[18];
            bytes[0] = (byte)Address;
            bytes[1] = (byte)((TallyStates[0] ? 1 : 0) & (TallyStates[1] ? 1 : 0) * 2);
            byte[] text = Encoding.ASCII.GetBytes(currentText);
            for (int i = 0; i < 16; i++)
                bytes[i + 2] = (i < text.Length) ? text[i] : (byte)' ';
            return bytes;
        }

    }
}
