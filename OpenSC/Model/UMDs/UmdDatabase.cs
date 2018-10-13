using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenSC.Model.UMDs
{

    [DatabaseName("umds")]
    [PolymorphDatabase(typeof(UmdTypeNameConverter))]
    class UmdDatabase: DatabaseBase<UMD>
    {

        public static UmdDatabase Instance { get; } = new UmdDatabase();

    }

}
