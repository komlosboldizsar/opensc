using OpenSC.Model.Persistence;

namespace OpenSC.Model.Variables
{

    [DatabaseName("dynamictexts")]
    [XmlTagNames("dynamictexts", "dynamictext")]
    public class DynamicTextDatabase : DatabaseBase<DynamicText>
    {

        public static DynamicTextDatabase Instance { get; } = new DynamicTextDatabase();

    }

}
