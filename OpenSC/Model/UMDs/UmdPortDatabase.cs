using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenSC.Model.UMDs
{

    [DatabaseName("umd_ports")]
    [PolymorphDatabase(typeof(UmdPortTypeNameConverter))]
    class UmdPortDatabase: DatabaseBase<UmdPort>
    {

        public static UmdPortDatabase Instance { get; } = new UmdPortDatabase();

    }

}
