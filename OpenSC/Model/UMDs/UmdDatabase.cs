using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenSC.Model.UMDs
{

    [DatabaseName("umds")]
    class UmdDatabase: DatabaseBase<UMD>
    {

        public static UmdDatabase Instance { get; } = new UmdDatabase();

    }

}
