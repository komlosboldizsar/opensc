using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenSC.Model.UMDs.Tsl50
{

    [DatabaseName(Tsl50ScreenDatabase.DBNAME)]
    [XmlTagNames("screens", "screen")]
    public class Tsl50ScreenDatabase: DatabaseBase<Tsl50Screen>
    {
        public static Tsl50ScreenDatabase Instance { get; } = new Tsl50ScreenDatabase();
        public const string DBNAME = "tsl50_screens";
    }

}
