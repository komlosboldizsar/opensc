using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.TSL31
{
    class TSL31 : UMD
    {

        public override IUMDType Type => new TSL31Type();

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

        public event UmdTextChanging TextChanging;
        public event UmdTextChanged TextChanged;

        public override Color[] TallyColors
        {
            get { return new Color[] { Color.Red, Color.Green }; }
        }

        public override event UmdTallyChanging TallyChanging;
        public override event UmdTallyChanged TallyChanged;

        private bool[] tallies = new bool[TSL31Type.TALLY_COUNT] { true };

        public override bool[] TallyStates
        {
            get { return tallies; }
        }

        public override void SetTally(int index, bool state)
        {

            if (index >= TSL31Type.TALLY_COUNT)
                throw new ArgumentOutOfRangeException();

            bool oldState = tallies[index];
            if (oldState != state)
            {
                TallyChanging?.Invoke(this, index, oldState, state);
                tallies[index] = state;
                update();
                TallyChanging?.Invoke(this, index, oldState, state);
            }
            
        }

        protected override void update()
        {
            if (port == null)
                return;
            var d = new Datagram()
            {
                Text = currentText,
                ValidUntil = DateTime.Now + TimeSpan.FromSeconds(5),
                Tallies = tallies
            };
            port.SendData(address, d);
        }

    }
}
