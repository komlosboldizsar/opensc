using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Streams
{
    [DatabaseName(StreamDatabase.DBNAME)]
    [PolymorphDatabase(typeof(StreamTypeRegister))]
    [XmlTagNames("streams", "stream")]
    public class StreamDatabase : DatabaseBase<Stream>
    {

        public static StreamDatabase Instance { get; } = new StreamDatabase();

        public const string DBNAME = "streams";

    }
}
