namespace OpenSC.Model.Variables
{

    [DatabaseName("dynamictexts")]
    public class DynamicTextDatabase : DatabaseBase<DynamicText>
    {

        public static DynamicTextDatabase Instance { get; } = new DynamicTextDatabase();

    }

}
