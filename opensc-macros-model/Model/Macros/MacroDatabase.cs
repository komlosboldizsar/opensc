using OpenSC.Model.Persistence;

namespace OpenSC.Model.Macros
{

    [DatabaseName(MacroDatabase.DBNAME)]
    [XmlTagNames("macros", "macro")]
    public class MacroDatabase : DatabaseBase<Macro>
    {

        public static MacroDatabase Instance { get; } = new MacroDatabase();

        public const string DBNAME = "macros";

    }

}
