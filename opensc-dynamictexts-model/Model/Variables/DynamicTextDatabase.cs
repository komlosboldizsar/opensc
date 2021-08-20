using OpenSC.Model.Persistence;

namespace OpenSC.Model.Variables
{

    [DatabaseName(DynamicTextDatabase.DBNAME)]
    [XmlTagNames("dynamictexts", "dynamictext")]
    public class DynamicTextDatabase : DatabaseBase<DynamicText>
    {

        public static DynamicTextDatabase Instance { get; } = new DynamicTextDatabase();

        public const string DBNAME = "dynamictexts";

    }

}
