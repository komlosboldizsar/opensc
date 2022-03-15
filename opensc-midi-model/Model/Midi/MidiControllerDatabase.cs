using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Midi
{

    [DatabaseName(MidiControllerDatabase.DBNAME)]
    [XmlTagNames("midicontrollers", "midicontroller")]
    public class MidiControllerDatabase : DatabaseBase<MidiController>
    {

        public static MidiControllerDatabase Instance { get; } = new MidiControllerDatabase();

        public const string DBNAME = "midicontrollers";

    }

}
