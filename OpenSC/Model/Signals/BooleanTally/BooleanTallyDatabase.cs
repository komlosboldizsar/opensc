using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals.BooleanTallies
{

    [DatabaseName(BooleanTallyDatabase.DBNAME)]
    [XmlTagNames("boolean_tallies", "boolean_tally")]
    class BooleanTallyDatabase : DatabaseBase<BooleanTally>
    {
        public static BooleanTallyDatabase Instance { get; } = new BooleanTallyDatabase();
        public const string DBNAME = "boolean_tallies";
    }

}
