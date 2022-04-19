using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.McCurdy
{

    [TypeLabel("McCurdy UMD-1 with Tally")]
    [TypeCode("mccurdy_tally")]
    public class McCurdyUMD1T : McCurdyUMD1
    {

        #region Info
        public override UmdTallyInfo[] TallyInfo => new UmdTallyInfo[]
        {
            new("L+R red", UmdTallyInfo.ColorSettingMode.Fix, Color.Red)
        };
        public override int TotalColumnWidth => 149;
        #endregion

        #region Calculating and sending data to hardware
        protected override void calculateTallyFields() => tallyToHardware = Tallies[0].CurrentState ? "#" : "! ";
        protected string tallyToHardware = "";
        #endregion

        #region Sending data to hardware
        protected override string getCommandTextToSend() => string.Format("%{0}D{1}{2}%Z", Address, tallyToHardware, textToHardware);
        #endregion

    }

}
