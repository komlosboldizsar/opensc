using System.Drawing;

namespace OpenSC.Model.Variables
{

    public class BooleanBase : IBoolean
    {

        public string Name { get => name; }

        private readonly string name;

        public Color Color { get => color; }

        private readonly Color color;

        private string description;

        public string Description
        {
            get => description;
            set
            {
                if (value == description)
                    return;
                description = value;
                DescriptionChanged?.Invoke(this, description);
                DescriptionChangedPCN?.Invoke();
            }
        }

        public event DescriptionChangedDelegate DescriptionChanged;
        public event ParameterlessChangeNotifierDelegate DescriptionChangedPCN;

        public BooleanBase(string name, Color color, string description = "")
        {
            this.name = name;
            this.color = color;
            this.description = description;
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
