namespace OpenSC.Model.Variables
{

    public delegate void BooleanStateChanged(IBoolean boolean);

    public interface IBoolean
    {

        string Name { get; }

        bool CurrentState { get; }

        event BooleanStateChanged StateChanged;

    }

}
