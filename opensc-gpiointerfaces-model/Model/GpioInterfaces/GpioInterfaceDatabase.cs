using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.GpioInterfaces
{
    [DatabaseName(GpioInterfaceDatabase.DBNAME)]
    [PolymorphDatabase(typeof(GpioInterfaceTypeRegister))]
    [XmlTagNames("gpio_interfaces", "gpio_interface")]
    public class GpioInterfaceDatabase : DatabaseBase<GpioInterface>
    {
        public static GpioInterfaceDatabase Instance { get; } = new GpioInterfaceDatabase();
        public const string DBNAME = "gpio_interfaces";
    }
}
