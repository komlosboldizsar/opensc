using System.Drawing;

namespace OpenSC.Model.Variables
{

    public class BooleanBase : IBoolean
    {

        private string name;

        public string Name
        {
            get => name;
            set
            {
                if (!BooleanRegister.Instance.CanNameUsedForBoolean(this, value))
                    return;
                if (value == name)
                    return;
                name = value;
                NameChanged?.Invoke(this, name);
                NameChangedPCN?.Invoke();
                BooleanRegister.Instance.BooleanNameChanged(this);
            }
        }

        public event BooleanNameChangedDelegate NameChanged;
        public event ParameterlessChangeNotifierDelegate NameChangedPCN;

        private readonly Color color;

        public Color Color { get => color; }

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

        public event BooleanDescriptionChangedDelegate DescriptionChanged;
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
                StateChanged?.Invoke(this, currentState);
                StateChangedPCN?.Invoke();
            }
        }

        public event BooleanStateChangedDelegate StateChanged;
        public event ParameterlessChangeNotifierDelegate StateChangedPCN;

    }

}
