using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Streams
{
    [DatabaseName("streams")]
    [PolymorphDatabase(typeof(StreamTypeNameConverter))]
    public class StreamDatabase : DatabaseBase<Stream>
    {

        public static StreamDatabase Instance { get; } = new StreamDatabase();

    }
}
