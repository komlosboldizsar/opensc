using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers
{

    class TimerDatabase: DatabaseBase<Timer>
    {

        public static TimerDatabase Instance { get; } = new TimerDatabase();

    }
}
