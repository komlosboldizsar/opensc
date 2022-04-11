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
            new("Tally", UmdTallyInfo.ColorSettingMode.Fix, Color.Red)
        };
        public override int TotalWidth => 150;
        #endregion

        #region Sending data to hardware
        protected override string getCommandTextToSend()
        {
            string replaced = textToHardware.Replace('1', (char)0x7E);
            replaced = replaced.Replace("%", "%%");
            replaced = string.Format("{0}{1}", (Tallies[0].CurrentState ? "#" : "! "), replaced);
            return string.Format("%{0}D{1}%Z", Address, replaced);
        }
        #endregion

    }

}
