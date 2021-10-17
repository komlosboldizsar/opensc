using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.Tsl50
{
    [TypeLabel("TSL 5.0")]
    [TypeCode("tsl50")]
    public class Tsl50Display : UMD
    {

        public override IUMDType Type => new Tsl50DisplayType();

        #region Property: Screen
        public event PropertyChangedTwoValuesDelegate<Tsl50Display, Tsl50Screen> ScreenChanged;

        [PersistAs("screen")]
        private Tsl50Screen screen;

#pragma warning disable CS0169
        [TempForeignKey(nameof(screen))]
        private string _screenId;
#pragma warning restore CS0169

        public Tsl50Screen Screen
        {
            get => screen;
            set => this.setProperty(ref screen, value, ScreenChanged);
        }
        #endregion

        #region Property: Index
        public event PropertyChangedTwoValuesDelegate<Tsl50Display, int> IndexChanged;

        [PersistAs("index")]
        private int index = 1;

        public int Index
        {
            get => index;
            set => this.setProperty(ref index, value, IndexChanged, validator: ValidateIndex);
        }

        public void ValidateIndex(int index)
        {
            if ((index < 0) || (index > 65534))
                throw new ArgumentOutOfRangeException();
        }
        #endregion

        public override Color[] TallyColors => new Color[] { Color.Red, Color.Green, Color.Red, Color.Green, Color.Red, Color.Green };
        protected override void tallyChanged(int index, bool state) => updateWithoutText();

        protected override void update() => screen?.SendDisplayData(getBytesToSend());
        protected void updateWithoutText() => screen?.SendDisplayData(getBytesToSend(false));

        protected virtual byte[] getBytesToSend(bool updateText = true)
        {
            int byteCount = 4; // INDEX + CONTROL
            byte[] text = null;
            if (updateText)
            {
                text = Encoding.ASCII.GetBytes(currentText);
                byteCount += 2 + text.Length; // LENGTH + TEXT
            }
            byte[] bytes = new byte[byteCount];
            bytes[0] = (byte)((index >> 8) & 0xFF);
            bytes[1] = (byte)(index & 0xFF);
            bytes[2] = 0; // CONTROL 
            bytes[3] = 0; // CONTROL (tallies)
            for (int i = 0, t = 1; i < Type.TallyCount; i++, t *= 2)
                if (TallyStates[i])
                    bytes[3] += (byte)t;
            if (updateText)
            {
                bytes[4] = (byte)((text.Length >> 8) & 0xFF); // LENGTH
                bytes[5] = (byte)(text.Length & 0xFF); // LENGTH
                text.CopyTo(bytes, 6);
            }
            return bytes;
        }

    }

}
