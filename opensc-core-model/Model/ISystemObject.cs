using OpenSC.Model.General;

namespace OpenSC.Model
{
    public interface ISystemObject : INotifyPropertyChanged
    {
        string GlobalID { get; }
        event PropertyChangedTwoValuesDelegate<ISystemObject, string> GlobalIdChanged;
    }
}
