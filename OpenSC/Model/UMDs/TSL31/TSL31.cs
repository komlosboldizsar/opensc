using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.TSL31
{
    class TSL31 : UMD
    {

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

        private string text;

        public string Text
        {
            get { return text; }
            set
            {
                string oldValue = value;
                string newValue = value.Substring(0, 16);
                TextChanging?.Invoke(this, oldValue, newValue);
                text = newValue;
                update();
                TextChanged?.Invoke(this, oldValue, newValue);
            }
        }

        public event UmdTallyChanging TallyChanging;
        public event UmdTallyChanged TallyChanged;

        private bool[] tallies = new bool[TSL31Type.TALLY_COUNT];

        public void SetTally(int index, bool state)
        {

            if (index >= TSL31Type.TALLY_COUNT)
                throw new ArgumentOutOfRangeException();

            bool oldState = tallies[index];
            if (oldState != state)
            {
                TallyChanging(this, index, oldState, state);
                tallies[index] = state;
                update();
                TallyChanging(this, index, oldState, state);
            }
            
        }

        private void update()
        {
            if (port == null)
                return;
            var d = new Datagram()
            {
                Text = this.text,
                ValidUntil = DateTime.Now + TimeSpan.FromSeconds(5),
                Tallies = tallies
            };
            port.SendData(address, d);
        }

    }
}
