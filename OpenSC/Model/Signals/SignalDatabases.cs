using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public class SignalDatabases
    {

        public static SignalCategoryDatabase Categories { get; } = SignalCategoryDatabase.Instance;
        public static SignalDatabase Signals { get; } = SignalDatabase.Instance;

        public const string DBNAME_CATEGORIES = "signal_categories";
        public const string DBNAME_SIGNALS = "signals";

        [DatabaseName(DBNAME_CATEGORIES)]
        [XmlTagNames("signal_categories", "category")]
        public class SignalCategoryDatabase : DatabaseBase<SignalCategory>
        {
            public static SignalCategoryDatabase Instance { get; } = new SignalCategoryDatabase();
        }

        [DatabaseName(DBNAME_SIGNALS)]
        [XmlTagNames("signals", "signal")]
        public class SignalDatabase : DatabaseBase<Signal>
        {
            public static SignalDatabase Instance { get; } = new SignalDatabase();
        }

    }

}
