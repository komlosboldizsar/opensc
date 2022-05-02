using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{
    [DatabaseName(CustomBooleanDatabase.DBNAME)]
    [PolymorphDatabase(typeof(CustomBooleanTypeRegister))]
    [XmlTagNames("custom_booleans", "custom_boolean")]
    public class CustomBooleanDatabase : DatabaseBase<CustomBoolean>
    {
        public static CustomBooleanDatabase Instance { get; } = new CustomBooleanDatabase();
        public const string DBNAME = "custom_booleans";
    }
}
