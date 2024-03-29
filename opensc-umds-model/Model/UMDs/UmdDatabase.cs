﻿using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenSC.Model.UMDs
{

    [DatabaseName(UmdDatabase.DBNAME)]
    [PolymorphDatabase(typeof(UmdTypeRegister))]
    [XmlTagNames("umds", "umd")]
    public class UmdDatabase: DatabaseBase<Umd>
    {
        public static UmdDatabase Instance { get; } = new UmdDatabase();
        public const string DBNAME = "umds";
    }

}
