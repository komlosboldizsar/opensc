using System.Drawing;

namespace OpenSC.Model.Variables
{

    public class BooleanBase : IBoolean
    {

        public string Name { get => name; }

        private readonly string name;

        public Color Color { get => color; }

        private readonly Color color;

        public BooleanBase(string name, Color color)
        {
            this.name = name;
            this.color = color;
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
