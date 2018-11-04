using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers
{

    [DatabaseName(MixerDatabase.DBNAME)]
    [PolymorphDatabase(typeof(MixerTypeNameConverter))]
    [XmlTagNames("mixers", "mixer")]
    public class MixerDatabase : DatabaseBase<Mixer>
    {

        public static MixerDatabase Instance { get; } = new MixerDatabase();

        public const string DBNAME = "mixers";

    }

}
