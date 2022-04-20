using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.MidiControllers
{

    [DatabaseName(MidiControllerDatabase.DBNAME)]
    [XmlTagNames("midi_controllers", "midi_controller")]
    public class MidiControllerDatabase : DatabaseBase<MidiController>
    {

        public static MidiControllerDatabase Instance { get; } = new MidiControllerDatabase();

        public const string DBNAME = "midi_controllers";

    }

}
