using OpenSC.Model.General;
using System.Drawing;

namespace OpenSC.Model.Variables
{

    public class BooleanBase : SystemObjectBase, IBoolean, INotifyPropertyChanged
    {

        public BooleanBase()
        { }

        public BooleanBase(string name, Color color, string description = "")
        {
            this.name = name;
            this.color = color;
            this.description = description;
        }

        #region Property: Name
        public event PropertyChangedTwoValuesDelegate<IBoolean, string> NameChanged;

        private string name;

        public string Name
        {
            get => name;
            set
            {
                if (!BooleanRegister.Instance.CanNameUsedForBoolean(this, value))
                    return;
                if (!setProperty(this, ref name, value, NameChanged))
                    return;
                if (Registered)
                    BooleanRegister.Instance.BooleanNameChanged(this);
            }
        }
        #endregion

        #region Property: Color
        public event PropertyChangedTwoValuesDelegate<IBoolean, Color> ColorChanged;

        private Color color;

        public Color Color
        {
            get => color;
            set => setProperty(this, ref color, value, ColorChanged);
        }
        #endregion

        #region Property: Description
        public event PropertyChangedTwoValuesDelegate<IBoolean, string> DescriptionChanged;

        private string description;

        public string Description
        {
            get => description;
            set => setProperty(this, ref description, value, DescriptionChanged);
        }
        #endregion

        #region Property: CurrentState
        public event PropertyChangedTwoValuesDelegate<IBoolean, bool> StateChanged;

        private bool currentState;

        public bool CurrentState
        {
            get => currentState;
            protected set => setProperty(this, ref currentState, value, StateChanged);
        }
        #endregion

        protected void register()
        {
            if (Registered)
                return;
            BooleanRegister.Instance.RegisterBoolean(this);
            Registered = true;
        }

        protected void unregister()
        {
            if (!Registered)
                return;
            BooleanRegister.Instance.UnregisterBoolean(this);
            Registered = false;
        }

        protected bool Registered { get; private set; }

    }

}
