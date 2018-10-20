using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Playouts
{
    [DatabaseName("playouts")]
    [PolymorphDatabase(typeof(PlayoutTypeNameConverter))]
    public class PlayoutDatabase : DatabaseBase<Playout>
    {

        public static PlayoutDatabase Instance { get; } = new PlayoutDatabase();

    }
}
