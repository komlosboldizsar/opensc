namespace OpenSC.Model.Variables
{

    public class BooleanBase : IBoolean
    {

        public string Name { get => name; }

        public readonly string name;

        public BooleanBase(string name)
        {
            this.name = name;
        }

        private bool currentState;

        public bool CurrentState
        {
            get { return currentState; }
            protected set
            {
                if (value == currentState)
                    return;
                currentState = value;
                StateChanged?.Invoke(this);
            }
        }

        public event BooleanStateChanged StateChanged;

    }

}
