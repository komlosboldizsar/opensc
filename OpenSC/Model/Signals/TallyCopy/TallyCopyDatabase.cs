using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals.TallyCopying
{

    [DatabaseName(TallyCopyDatabase.DBNAME)]
    [XmlTagNames("tally_copies", "tally_copy")]
    class TallyCopyDatabase : DatabaseBase<TallyCopy>
    {

        public static TallyCopyDatabase Instance { get; } = new TallyCopyDatabase();

        public const string DBNAME = "tally_copies";

    }

}
