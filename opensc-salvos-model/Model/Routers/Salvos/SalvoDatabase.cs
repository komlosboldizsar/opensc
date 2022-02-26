using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Salvos
{
    [DatabaseName(SalvoDatabase.DBNAME)]
    [XmlTagNames("savlos", "salvo")]
    public class SalvoDatabase : DatabaseBase<Salvo>
    {
        public static SalvoDatabase Instance { get; } = new SalvoDatabase();
        public const string DBNAME = "salvos";
    }
}
