using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.CrosspointBooleans
{
    [DatabaseName(CrosspointBooleanDatabase.DBNAME)]
    [XmlTagNames("crosspoint_booleans", "crosspoint_boolean")]
    public class CrosspointBooleanDatabase : DatabaseBase<CrosspointBoolean>
    {
        public static CrosspointBooleanDatabase Instance { get; } = new CrosspointBooleanDatabase();
        public const string DBNAME = "crosspoint_booleans";
    }
}
