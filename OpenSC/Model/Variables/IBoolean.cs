using System.Drawing;

namespace OpenSC.Model.Variables
{

    public delegate void BooleanStateChangedDelegate(IBoolean boolean, bool newState);
    public delegate void BooleanNameChangedDelegate(IBoolean boolean, string newName);
    public delegate void BooleanColorChangedDelegate(IBoolean boolean, Color newColor);
    public delegate void BooleanDescriptionChangedDelegate(IBoolean boolean, string newDescription);

    public interface IBoolean
    {

        string Name { get; set; }

        event BooleanNameChangedDelegate NameChanged;

        Color Color { get; set; }

        event BooleanColorChangedDelegate ColorChanged;

        string Description { get; set; }

        event BooleanDescriptionChangedDelegate DescriptionChanged;

        bool CurrentState { get; }

        event BooleanStateChangedDelegate StateChanged;

    }

}
