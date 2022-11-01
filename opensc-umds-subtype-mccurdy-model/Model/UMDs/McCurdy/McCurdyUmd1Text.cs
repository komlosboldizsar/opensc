using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.McCurdy
{
    public class McCurdyUmd1Text : UmdText
    {

        public override UmdTextInfo Info
        {
            get => base.Info;
            set
            {
                base.Info = value;
                columnWidth = ((McCurdyUmd1TextInfo)value).DefaultColumnWidth;
            }
        }

        #region Property: ColumnWidth
        public event PropertyChangedTwoValuesDelegate<UmdText, int> ColumnWidthChanged;
        private int columnWidth;
        public int ColumnWidth
        {
            get => columnWidth;
            set => this.setProperty(ref columnWidth, value, ColumnWidthChanged, null, (ov, nv) => ((McCurdyUMD1)Parent)?.NotifyTextColumnWidthChanged(this));
        }
        #endregion

    }
}
