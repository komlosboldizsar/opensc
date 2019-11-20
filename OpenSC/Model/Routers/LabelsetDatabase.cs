using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    [DatabaseName(LabelsetDatabase.DBNAME)]
    [XmlTagNames("labelsets", "labelset")]
    public class LabelsetDatabase : DatabaseBase<Labelset>
    {

        public static LabelsetDatabase Instance { get; } = new LabelsetDatabase();

        public const string DBNAME = "labelsets";

    }

}
