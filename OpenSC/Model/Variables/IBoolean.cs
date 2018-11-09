using System.Drawing;

namespace OpenSC.Model.Variables
{

    public delegate void BooleanStateChangedDelegate(IBoolean boolean, bool newState);

    public delegate void BooleanNameChangedDelegate(IBoolean boolean, string newName);
    public delegate void BooleanDescriptionChangedDelegate(IBoolean boolean, string newDescription);

    public interface IBoolean
    {

        string Name { get; }

        event BooleanNameChangedDelegate NameChanged;

        Color Color { get; }

        string Description { get; set; }

        event BooleanDescriptionChangedDelegate DescriptionChanged;

        bool CurrentState { get; }

        event BooleanStateChangedDelegate StateChanged;

    }

}
