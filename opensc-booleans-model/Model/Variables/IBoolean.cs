using OpenSC.Model.General;
using System.Drawing;

namespace OpenSC.Model.Variables
{

    public delegate void BooleanStateChangedDelegate(IBoolean boolean, bool newState);
    public delegate void BooleanNameChangedDelegate(IBoolean boolean, string newName);
    public delegate void BooleanColorChangedDelegate(IBoolean boolean, Color newColor);
    public delegate void BooleanDescriptionChangedDelegate(IBoolean boolean, string newDescription);

    public interface IBoolean : INotifyPropertyChanged
    {

        string Identifier { get; set; }
        event PropertyChangedTwoValuesDelegate<IBoolean, string> IdentifierChanged;

        Color Color { get; set; }
        event PropertyChangedTwoValuesDelegate<IBoolean, Color> ColorChanged;

        string Description { get; set; }
        event PropertyChangedTwoValuesDelegate<IBoolean, string> DescriptionChanged;

        bool CurrentState { get; }
        event PropertyChangedTwoValuesDelegate<IBoolean, bool> StateChanged;

        string ToString() => $"[{Identifier}] {Description}";

    }

}
