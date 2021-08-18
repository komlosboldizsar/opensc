using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.CrosspointStores
{
    [DatabaseName(CrosspointStoreDatabase.DBNAME)]
    [XmlTagNames("crosspoint_stores", "crosspoint_store")]
    public class CrosspointStoreDatabase : DatabaseBase<CrosspointStore>
    {
        public static CrosspointStoreDatabase Instance { get; } = new CrosspointStoreDatabase();
        public const string DBNAME = "crosspoint_stores";
    }
}
