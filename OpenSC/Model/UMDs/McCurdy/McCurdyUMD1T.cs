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
    class McCurdyUMD1T : McCurdyUMD1
    {

        public override IUMDType Type => new McCurdyUMD1TType();

        public override int TotalWidth => 150;

        protected override string getTextToSend()
        {
            string replaced = currentText.Replace('1', (char)0x7E);
            replaced = replaced.Replace("%", "%%");
            replaced = string.Format("{0}{1}", (TallyStates[0] ? "#" : "! "), replaced);
            return string.Format("%{0}D{1}%Z", Address, replaced);
        }

    }

}
