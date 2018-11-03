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
            return string.Format("{0}{1}",
                (TallyStates[0] ? "#" : "! "),
                currentText);
        }

    }

}
