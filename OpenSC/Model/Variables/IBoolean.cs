using System.Drawing;

namespace OpenSC.Model.Variables
{

    public delegate void BooleanStateChanged(IBoolean boolean);

    public delegate void DescriptionChangedDelegate(IBoolean boolean, string newDescription);

    public interface IBoolean
    {

        string Name { get; }

        Color Color { get; }

        string Description { get; set; }

        event DescriptionChangedDelegate DescriptionChanged;
        event ParameterlessChangeNotifierDelegate DescriptionChangedPCN;

        bool CurrentState { get; }

        event BooleanStateChanged StateChanged;
        event ParameterlessChangeNotifierDelegate StateChangedPCN;

    }

}
