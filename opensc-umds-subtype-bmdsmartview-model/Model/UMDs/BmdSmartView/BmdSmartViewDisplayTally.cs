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

        public BmdSmartViewDisplayTally(Umd owner, int indexAtOwner, UmdTallyInfo info) : base(owner, indexAtOwner, info)
        {
            priority = ((BmdSmartViewDisplayTallyInfo)info).DefaultPriority;
        }

        #region Property: Priority
        public event PropertyChangedTwoValuesDelegate<BmdSmartViewDisplayTally, int> PriorityChanged;
        private int priority;
        public int Priority
        {
            get => priority;
            set => this.setProperty(ref priority, value, PriorityChanged, null, (ov, nv) => ((BmdSmartViewDisplay)Owner)?.NotifyTallyPriorityChanged(this));
        }
        #endregion

    }
}
