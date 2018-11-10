using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public class ExternalSignalDatabases
    {

        public static ExternalSignalCategoryDatabase Categories { get; } = ExternalSignalCategoryDatabase.Instance;
        public static ExternalSignalDatabase Signals { get; } = ExternalSignalDatabase.Instance;

        public const string DBNAME_CATEGORIES = "external_signal_categories";
        public const string DBNAME_SIGNALS = "external_signals";

        [DatabaseName(DBNAME_CATEGORIES)]
        [XmlTagNames("external_signal_categories", "category")]
        public class ExternalSignalCategoryDatabase : DatabaseBase<ExternalSignalCategory>
        {
            public static ExternalSignalCategoryDatabase Instance { get; } = new ExternalSignalCategoryDatabase();
        }

        [DatabaseName(DBNAME_SIGNALS)]
        [XmlTagNames("external_signals", "signal")]
        public class ExternalSignalDatabase : DatabaseBase<ExternalSignal>
        {

            public static ExternalSignalDatabase Instance { get; } = new ExternalSignalDatabase();

            protected override void afterAdd(ExternalSignal item)
            {
                base.afterAdd(item);
                SignalRegister.Instance.RegisterSignal(item);
            }

            protected override void afterRemove(ExternalSignal item)
            {
                base.afterRemove(item);
                SignalRegister.Instance.UnregisterSignal(item);
            }

            protected override void afterLoad()
            {
                base.afterLoad();
                foreach(ISignal signal in ItemsAsList)
                    SignalRegister.Instance.RegisterSignal(signal);
            }

        }

    }

}
