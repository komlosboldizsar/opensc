using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers
{

    [DatabaseName(TimerDatabase.DBNAME)]
    [XmlTagNames("timers", "timer")]
    class TimerDatabase: DatabaseBase<Timer>
    {

        public static TimerDatabase Instance { get; } = new TimerDatabase();

        public const string DBNAME = "timers";

    }
}
