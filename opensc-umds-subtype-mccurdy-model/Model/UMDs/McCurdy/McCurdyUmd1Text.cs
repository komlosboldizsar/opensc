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

        public McCurdyUmd1Text(Umd owner, int indexAtOwner, UmdTextInfo info) : base(owner, indexAtOwner, info)
        {
            columnWidth = ((McCurdyUmd1TextInfo)info).DefaultColumnWidth;
        }

        #region Property: ColumnWidth
        public event PropertyChangedTwoValuesDelegate<UmdText, int> ColumnWidthChanged;
        private int columnWidth;
        public int ColumnWidth
        {
            get => columnWidth;
            set => this.setProperty(ref columnWidth, value, ColumnWidthChanged, null, (ov, nv) => ((McCurdyUMD1)Owner)?.NotifyTextColumnWidthChanged(this));
        }
        #endregion

    }
}
