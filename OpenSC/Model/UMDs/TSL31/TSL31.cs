using OpenSC.Model.Persistence;
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

        [PersistAs("port")]
        private TSL31Port port;

        public TSL31Port Port
        {
            get { return port; }
            set { port = value; }
        }

        private int address;

        public int Address
        {
            get { return address; }
            set
            {
                if (value < 0 || value > 126)
                    throw new ArgumentOutOfRangeException();
                address = value;
            }
        }

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
            var d = new Datagram()
            {
                Text = currentText,
                ValidUntil = DateTime.Now + TimeSpan.FromSeconds(5),
                Tallies = TallyStates
            };
            port.SendData(address, d);
        }

    }
}
