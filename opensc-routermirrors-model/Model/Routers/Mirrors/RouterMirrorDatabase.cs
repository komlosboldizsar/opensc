using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Mirrors
{
    [DatabaseName(RouterMirrorDatabase.DBNAME)]
    [XmlTagNames("router_mirrors", "router_mirror")]
    public class RouterMirrorDatabase : DatabaseBase<RouterMirror>
    {
        public static RouterMirrorDatabase Instance { get; } = new RouterMirrorDatabase();
        public const string DBNAME = "router_mirrors";
    }
}
