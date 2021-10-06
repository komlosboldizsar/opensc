using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{
    [DatabaseName(RouterDatabase.DBNAME)]
    [PolymorphDatabase(typeof(RouterTypeRegister))]
    [XmlTagNames("routers", "router")]
    public class RouterDatabase : DatabaseBase<Router>
    {
        public static RouterDatabase Instance { get; } = new RouterDatabase();
        public const string DBNAME = "routers";
    }
}
