using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{
    public class UmdTallyCollection : ComponentCollection<Umd, UmdTally, UmdTallyCollection>
    {

        public UmdTallyCollection(Umd owner) : base(owner)
        {
            int i = 0;
            foreach (UmdTallyInfo tallyInfo in owner.TallyInfo)
            {
                UmdTally tally = owner.CreateTally();
                tally.Info = tallyInfo;
                tally.Index = i++;
                Add(tally);
            }
        }

        protected override UmdTally createInstance(string typeCode) => new();
        protected override string InstanceNameTemplate => "Tally #{0}";

        internal void RestoredOwnFields()
        {
            int tallyCount = Count;
            int tallyInfoLength = owner.TallyInfo.Length;
            if (tallyCount > tallyInfoLength)
                for (int i = tallyCount - 1; i >= tallyInfoLength; i--)
                    RemoveByKey(i);
            if (tallyCount < tallyInfoLength)
                for (int i = tallyCount; i < tallyInfoLength; i++)
                    Add(owner.CreateTally());
        }

        internal void RestoreCustomRelations()
        {
            foreach (UmdTally tally in this)
                tally.RestoreCustomRelations();   
        }

    }
}
