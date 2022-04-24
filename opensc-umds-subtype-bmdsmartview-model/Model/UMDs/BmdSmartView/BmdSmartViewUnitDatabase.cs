using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenSC.Model.UMDs.BmdSmartView
{

    [DatabaseName(BmdSmartViewUnitDatabase.DBNAME)]
    [XmlTagNames("units", "unit")]
    public class BmdSmartViewUnitDatabase: DatabaseBase<BmdSmartViewUnit>
    {
        public static BmdSmartViewUnitDatabase Instance { get; } = new BmdSmartViewUnitDatabase();
        public const string DBNAME = "umds_bmdsmartview_units";
    }

}
