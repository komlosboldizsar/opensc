using OpenSC.Model.General;
using System.Drawing;

namespace OpenSC.Model.Variables
{

    public class BooleanBase : IBoolean, INotifyPropertyChanged
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
                PropertyChanged?.Invoke(nameof(Name));
                if (Registered)
                    BooleanRegister.Instance.BooleanNameChanged(this);
            }
        }

        public event BooleanNameChangedDelegate NameChanged;

        private Color color;

        public Color Color
        {
            get => color;
            set
            {
                if (value == color)
                    return;
                ColorChanged?.Invoke(this, color);
                PropertyChanged?.Invoke(nameof(Name));
            }
        }

        public event BooleanColorChangedDelegate ColorChanged;

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
                PropertyChanged?.Invoke(nameof(Description));
            }
        }

        public event BooleanDescriptionChangedDelegate DescriptionChanged;

        public BooleanBase()
        { }

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
                PropertyChanged?.Invoke(nameof(CurrentState));
            }
        }

        public event BooleanStateChangedDelegate StateChanged;

        protected void register()
        {
            if (!Registered)
            {
                BooleanRegister.Instance.RegisterBoolean(this);
                Registered = true;
            }
        }

        protected void unregister()
        {
            if (Registered)
            {
                BooleanRegister.Instance.UnregisterBoolean(this);
                Registered = false;
            }
        }

        protected bool Registered { get; private set; }

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedDelegate PropertyChanged;
        #endregion

    }

}
