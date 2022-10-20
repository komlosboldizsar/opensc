using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.X32Faders
{
    [DatabaseName(X32FaderDatabase.DBNAME)]
    [XmlTagNames("savlos", "salvo")]
    public class X32FaderDatabase : DatabaseBase<X32Fader>
    {
        public static X32FaderDatabase Instance { get; } = new X32FaderDatabase();
        public const string DBNAME = "x32faders";
    }
}
