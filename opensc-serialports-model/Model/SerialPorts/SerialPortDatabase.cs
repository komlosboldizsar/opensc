using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.SerialPorts
{

    [DatabaseName(SerialPortDatabase.DBNAME)]
    [XmlTagNames("serial_ports", "serial_port")]
    public class SerialPortDatabase: DatabaseBase<SerialPort>
    {

        public static SerialPortDatabase Instance { get; } = new SerialPortDatabase();

        public const string DBNAME = "serial_ports";

    }

}
