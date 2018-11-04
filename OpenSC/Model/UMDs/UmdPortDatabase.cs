using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenSC.Model.UMDs
{

    [DatabaseName(UmdPortDatabase.DBNAME)]
    [PolymorphDatabase(typeof(UmdPortTypeNameConverter))]
    [XmlTagNames("ports", "port")]
    class UmdPortDatabase: DatabaseBase<UmdPort>
    {

        public static UmdPortDatabase Instance { get; } = new UmdPortDatabase();

        public const string DBNAME = "umd_ports";

    }

}
