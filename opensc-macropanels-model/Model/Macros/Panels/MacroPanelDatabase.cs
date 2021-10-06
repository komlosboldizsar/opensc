using OpenSC.Model.Persistence;

namespace OpenSC.Model.Macros
{

    [DatabaseName(MacroPanelDatabase.DBNAME)]
    [XmlTagNames("macropanels", "macropanel")]
    public class MacroPanelDatabase : DatabaseBase<MacroPanel>
    {

        public static MacroPanelDatabase Instance { get; } = new MacroPanelDatabase();

        public const string DBNAME = "macropanels";

    }

}
