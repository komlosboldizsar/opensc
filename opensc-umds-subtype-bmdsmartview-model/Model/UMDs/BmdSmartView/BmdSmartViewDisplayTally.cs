using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.BmdSmartView
{
    public class BmdSmartViewDisplayTally : UmdTally
    {

        public override UmdTallyInfo Info
        {
            get => base.Info;
            set
            {
                base.Info = value;
                priority = ((BmdSmartViewDisplayTallyInfo)value).DefaultPriority;
            }
        }

        #region Property: Priority
        public event PropertyChangedTwoValuesDelegate<BmdSmartViewDisplayTally, int> PriorityChanged;
        private int priority;
        public int Priority
        {
            get => priority;
            set => this.setProperty(ref priority, value, PriorityChanged, null, (ov, nv) => ((BmdSmartViewDisplay)Parent)?.NotifyTallyPriorityChanged(this));
        }
        #endregion

    }
}
