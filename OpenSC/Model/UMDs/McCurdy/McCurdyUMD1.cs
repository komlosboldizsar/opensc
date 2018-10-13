using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.McCurdy
{
    class McCurdyUMD1 : UMD
    {

        public override IUMDType Type => new McCurdyUMD1Type();

        private McCurdyPort port;

        public McCurdyPort Port
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
                if (value <= 0 || value > 255)
                    throw new ArgumentOutOfRangeException();
                address = value;
            }
        }

        public event UmdTextChanging TextChanging;
        public event UmdTextChanged TextChanged;

        private string text;

        public override string Text
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

        public override Color[] TallyColors
        {
            get { return new Color[] { Color.Red, Color.Green }; }
        }

        public override event UmdTallyChanging TallyChanging;
        public override event UmdTallyChanged TallyChanged;

        private bool[] tallies = new bool[McCurdyUMD1Type.TALLY_COUNT] { };

        public override bool[] TallyStates
        {
            get { return tallies; }
        }

        public override void SetTally(int index, bool state)
        {

            if (index >= McCurdyUMD1Type.TALLY_COUNT)
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
